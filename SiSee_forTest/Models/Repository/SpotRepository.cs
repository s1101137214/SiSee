using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiSee_v1.Models.ViewModels;
using System.Data.SqlClient;

namespace SiSee_v1.Models.Repository
{
    public class SpotRepository
    {
        //
        private sisdbEntities1 db = new sisdbEntities1();

        #region Create

        public void CreateCommand(CommentRecord CommentRecord)
        {
            db.Database.SqlQuery<CommentRecord>(
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
        #endregion

        #region Select

        //首頁預設取得全部Spot
        public IEnumerable<Spot> GetAll()
        {
            //IEnumerable<Spot> spot = db.Database.SqlQuery<Spot>(
            //    "Select TOP 10 * FROM SPOT"
            //    );
            //   return db.Spot.take<Spot>(10);

            return db.Spot;
        }

        public IEnumerable<Spot> test()
        {
            //IEnumerable<Spot> t = db.Database.SqlQuery<Spot>(
            //    "Select TOP 10 * FROM SPOT WHERE 1=1"
            //    );

            return db.Spot;
        }

        public List<Spot> GetByName(String searchName)
        {
            List<Spot> spot = db.Spot.Where(s => s.spot_name.Contains(searchName)).ToList();

            return spot;
        }

        public List<Spot> GetByAreaName(String areaName)
        {
            return null;
        }

        #endregion
    }
}