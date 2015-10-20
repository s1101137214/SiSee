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

        //幹結果這個寫這麼久不能用
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

        }


        #endregion

        #region Select


        //首頁預設取得全部Spot
        public IEnumerable<Spot> GetAll()
        {
            return db.Spot;
        }

        public IEnumerable<Spot> test()
        {
            IEnumerable<Spot> t = db.Database.SqlQuery<Spot>(
                "Select * FROM SPOT where 1=@p", new SqlParameter("p", "1")
                );

            return t;
        }

        #endregion
    }
}