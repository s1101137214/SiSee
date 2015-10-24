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
            db.Database.ExecuteSqlCommand(
         @"INSERT INTO [dbo].[USER] (
	            [user_FBID],
	            [user_name],
	            [user_email],
                [user_birth]
            )
            VALUES
	            (
		            @user_FBID ,@user_name ,@user_email, @user_birth
	            );
            ",
            new SqlParameter("@user_FBID", user.user_FBID),
            new SqlParameter("@user_name", user.user_name),
            new SqlParameter("@user_email", user.user_email),
            new SqlParameter("@user_birth", System.DateTime.Now)
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