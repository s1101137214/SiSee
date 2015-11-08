using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;

namespace SiSee_v1.Models.Repository
{
    public class SpotRepository : IRepository
    {
        #region Create

        public void CreateCommand(CommentRecord CommentRecord)
        {
            db.Database.ExecuteSqlCommand(
            @"INSERT INTO CommentRecord (
	            spot_ID,
	            user_ID,
	            comment_context,
	            comment_grade,
	            comment_date
            )
            VALUES
	            (
		            @spot_ID ,@user_ID ,@comment_context ,@comment_grade ,@comment_date
	            );
            ",
               new SqlParameter("spot_ID", CommentRecord.spot_ID),
               new SqlParameter("user_ID", CommentRecord.user_ID),
               new SqlParameter("comment_grade", CommentRecord.comment_grade),
               new SqlParameter("comment_context", CommentRecord.comment_context),
               new SqlParameter("comment_date", CommentRecord.comment_date)
           );

            db.SaveChanges();

        }

        public void CreateSearchReacord(SearchRecord searchRecord)
        {
            db.Database.ExecuteSqlCommand(
                @"INSERT INTO [dbo].[SearchRecord] (
	                    [spot_ID],
	                    [user_ID],
	                    [search_date]
                    )
                    VALUES
	                    (
		                    @spot_ID ,@user_ID ,@search_date
	                    );
                    ",
                     new SqlParameter("@spot_ID", searchRecord.spot_ID),
                     new SqlParameter("@user_ID", searchRecord.user_ID),
                     new SqlParameter("@search_date", searchRecord.search_date)
                     );

            db.SaveChanges();
        }

        public void CreateFavoriteReacord(FavoriteRecord favoriteRecord)
        {
            db.Database.ExecuteSqlCommand(
                @"INSERT INTO [dbo].[FavoriteRecord] (
	                    [spot_ID],
	                    [user_ID]
                    )
                    VALUES
	                    (
		                    @spot_ID ,@user_ID 
	                    );
                    ",
                     new SqlParameter("@spot_ID", favoriteRecord.spot_ID),
                     new SqlParameter("@user_ID", favoriteRecord.user_ID)
                     );

            db.SaveChanges();
        }

        #endregion

        #region Select

        public int? GetSpotFavoriteRecordCount(int spotID)
        {
            var spot = db.Database.SqlQuery<int>(
                     @"SELECT
	                        COUNT (1)
                        FROM
	                        FavoriteRecord R
                        WHERE
	                        R.spot_ID = @spot_ID",
                      new SqlParameter("@spot_ID", spotID)
                      );

            return spot.First();
        }

        public int? GetSpotCommentRecordsCount(int spotID)
        {
            var spot = db.Database.SqlQuery<int>(
                     @"SELECT
	                        COUNT (1)
                        FROM
	                        CommentRecord R
                        WHERE
	                        R.spot_ID = @spot_ID",
                      new SqlParameter("@spot_ID", spotID)
                      );

            return spot.First();
        }

        public List<Spot> GetAll()
        {
            //依熱門(搜尋次數)排列
            List<Spot> spot = db.Database.SqlQuery<Spot>(
                @"SELECT
	                        TOP 100 *
                        FROM
	                        (
		                        SELECT
			                        Spot.spot_ID,
			                        COUNT (SearchRecord.spot_ID) AS num
		                        FROM
			                        Spot
		                        Left JOIN SearchRecord ON SearchRecord.spot_ID = Spot.spot_ID
		                        GROUP BY
			                        Spot.spot_ID
	                        ) AS Spotcount
                        JOIN Spot ON Spot.spot_ID = Spotcount.spot_ID
                        ORDER BY
	                        num DESC"
                ).ToList();

            return spot;
        }

        public List<Spot> GetByName(String searchName)
        {
            List<Spot> spot = db.Spot.Where(s => s.spot_name.Contains(searchName)).ToList();

            foreach (Spot s in spot)
            {
                s.spot_score = this.GetSpotScore(s.spot_ID);
            }
            return spot;
        }

        public List<Spot> GetByAreaName(String areaName)
        {
            int area_ID;

            switch (areaName)
            {
                case "北部":
                    area_ID = 1;
                    break;
                case "中部":
                    area_ID = 2;
                    break;
                case "南部":
                    area_ID = 3;
                    break;
                case "東部":
                    area_ID = 4;
                    break;
                default:
                    return null;
            }

            List<Spot> spot = db.Spot.Where(s => s.area_ID == area_ID).ToList();

            return spot.ToList();
        }

        public bool GetSpotFavoriteRecordIsSet(int spotID, string userID)
        {
            IEnumerable<FavoriteRecord> favoriteRecord = db.Database.SqlQuery<FavoriteRecord>(
                        @"SELECT 
                        *
                        FROM
                        [dbo].[FavoriteRecord] AS F
                        WHERE
                        F.spot_ID = @spot_ID
                        AND F.user_ID = @user_ID",
                         new SqlParameter("@spot_ID", spotID),
                         new SqlParameter("@user_ID", int.Parse(userID))
                         );

            if (favoriteRecord.Count() > 0)
            {
                return true;
            }

            return false;
        }

        public string GetSpotScore(int spotID)
        {
            var score = db.CommentRecord.Where(c => c.spot_ID == spotID).Select(s => s.comment_grade);

            int sum = 0;

            foreach (string s in score)
            {
                sum = sum + int.Parse(s);
            }
            if (score.Count() > 0)
            {
                int avg = sum / score.Count();

                return avg.ToString();
            }

            return "0";

        }

        #endregion

        #region Delete

        public void DeleteFavoriteRecord(int spotID, string userID)
        {
            db.Database.ExecuteSqlCommand(
            @"DELETE 
                    FROM
	                    [dbo].[FavoriteRecord]
                    WHERE
	                    spot_ID = @spot_ID
                    AND user_ID = @user_ID",
                        new SqlParameter("@spot_ID", spotID),
                        new SqlParameter("@user_ID", int.Parse(userID))
                 );

            db.SaveChanges();
        }

        #endregion


        #region Update

        public void UpdateSpotScoreByAuto()
        {
            List<Spot> spot = db.Database.SqlQuery<Spot>(
                    @"SELECT
	                                        *
                                        FROM
	                                        (
		                                        SELECT
			                                        Spot.spot_ID,
			                                        COUNT (SearchRecord.spot_ID) AS num
		                                        FROM
			                                        Spot
		                                        Left JOIN SearchRecord ON SearchRecord.spot_ID = Spot.spot_ID
		                                        GROUP BY
			                                        Spot.spot_ID
	                                        ) AS Spotcount
                                        JOIN Spot ON Spot.spot_ID = Spotcount.spot_ID
                                        ORDER BY
	                                        num DESC"
                    ).ToList();


            foreach (Spot s in spot)
            {
                db.Database.ExecuteSqlCommand(
               @"UPDATE [dbo].[Spot]
                    SET 
                    [spot_score] = @spot_score
                    WHERE
	                    [spot_ID]  = @spot_ID
                    ",
                    new SqlParameter("@spot_ID", s.spot_ID),
                    new SqlParameter("@spot_score", String.IsNullOrEmpty(s.spot_score) ? "0" : s.spot_score)

                    );

                db.SaveChanges();

            }
           
        }

        #endregion
    }
}