using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;

namespace SiSee_v1.Models.Repository
{
    public class SpotRepository
    {
        
        private sisdbEntities1 db = new sisdbEntities1();

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

        public IEnumerable<Spot> GetAll()
        {
            //IEnumerable<Spot> spot = db.Database.SqlQuery<Spot>(
            //    "Select TOP 10 * FROM SPOT"
            //    );
            //   return db.Spot.take<Spot>(10);

            return db.Spot;
        }

        public List<Spot> GetByName(String searchName)
        {
            List<Spot> spot = db.Spot.Where(s => s.spot_name.Contains(searchName)).ToList();

            return spot;
        }

        public List<Spot> GetByAreaName(String areaName)
        {
            //有bug
            List<Spot> spot = db.Spot.Where(s => s.Area.area_Name.Contains(areaName)).ToList();

            return spot;
        }

        public bool GetSpotFavoriteRecordIsSet(int spotID,string userID)
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
    }
}