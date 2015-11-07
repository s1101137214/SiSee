using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using SiSee_v1.Models.ViewModels;

namespace SiSee_v1.Models.Repository
{
    public class UserRepository : IRepository
    {
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

        public List<SpotComment> GetCommentRecordByUserID(int? userID)
        {
            var commentRecord = db.Database.SqlQuery<SpotComment>(
                     @"SELECT
	                            *
                            FROM
	                            [dbo].[CommentRecord]
                            JOIN Spot ON Spot.spot_ID = CommentRecord.spot_ID
                            WHERE
	                            user_ID = @user_ID",
                      new SqlParameter("@user_ID", userID)
                      );

            return commentRecord.ToList();
        }

        public List<FavoriteRecord> GetFavoriteRecordByUserID(int? userID)
        {
            var commentRecord = db.Database.SqlQuery<FavoriteRecord>(
                     @"SELECT
	                            *
                            FROM
	                            [dbo].[FavoriteRecord]
                            WHERE
	                            user_ID = @user_ID",
                      new SqlParameter("@user_ID", userID)
                      );

            return commentRecord.ToList();
        }

        #endregion

        #region Update

        public void UpdateUser(User user)
        {
            db.Database.ExecuteSqlCommand(
                @"UPDATE [dbo].[User]
                    SET 
                    [user_name] = @user_name,
                    [user_tel] =@user_tel,
                    [user_email] =@user_email,
                    [user_birth] = @user_birth
                    WHERE
	                    [user_ID] = @user_ID
                    ",
                     new SqlParameter("@user_name", user.user_name),
                     new SqlParameter("@user_email", String.IsNullOrEmpty(user.user_email) ? "" : user.user_email),
                     new SqlParameter("@user_tel", String.IsNullOrEmpty(user.user_tel) ? "" : user.user_tel),
                     new SqlParameter("@user_birth", user.user_birth),
                     new SqlParameter("@user_ID", user.user_ID)
                     );

            db.SaveChanges();
        }

        public void UpdateUserCommendRecord(CommentRecord commentRecord)
        {
            db.Database.ExecuteSqlCommand(
                @"UPDATE [dbo].[CommentRecord]
                    SET 
                    [comment_date] = @comment_date,
                    [comment_grade] =@comment_grade,
                    [comment_context] = @comment_context
                    WHERE
	                    [commentrecord_ID] = @commentrecord_ID 
                    ",
                     new SqlParameter("@user_name", commentRecord.commentrecord_ID),
                     new SqlParameter("@user_tel", commentRecord.comment_date),
                     new SqlParameter("@comment_grade", commentRecord.comment_grade),
                     new SqlParameter("@comment_context", commentRecord.comment_context)
                     );

            db.SaveChanges();
        }

        public void DeleteFavoriteRecordByUserIDAndSpotID(int spotID, string userID)
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

        public void DeleteCommendRecordByUserIDAndSpotID(int spotID, string userID)
        {
            db.Database.ExecuteSqlCommand(
            @"DELETE 
                    FROM
	                    [dbo].[CommendRecord]
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