using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SiSee_v1.Models.Repository
{
    public class UserRepository
    {
        //Spot
        private sisdbEntities1 db = new sisdbEntities1();

        #region Create

        public void CreateUser(User user)
        {
            db.Database.SqlQuery<User>(
         @"INSERT INTO CommentRecord (
	            user_FBID,
	            user_name,
	            user_email
            )
            VALUES
	            (
		            @user_FBID ,@user_name ,@user_email 
	            );
            ",
            new SqlParameter("spot_ID", user.user_FBID),
            new SqlParameter("user_ID", user.user_name),
            new SqlParameter("comment_grade", user.user_email)
        );
            db.SaveChanges();
        }

        #endregion

        #region Select

        public IEnumerable<Spot> GetAll()
        {
            return db.Spot;
        }

        #endregion
    }
}