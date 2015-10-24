//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SiSee_v1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class User
    {
        public User()
        {
            this.FavoriteRecord = new HashSet<FavoriteRecord>();
            this.CommentRecord = new HashSet<CommentRecord>();
            this.SearchRecord = new HashSet<SearchRecord>();
        }

        [Display(Name = "ID")]
        public int user_ID { get; set; }
        [Display(Name = "FBID")]
        public string user_FBID { get; set; }
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "請輸入正確的電子信箱")]
        public string user_email { get; set; }
        [Display(Name = "電話")]
        public string user_tel { get; set; }
        [Display(Name = "生日")]
         [DataType(DataType.Date, ErrorMessage = "請輸入正確的日期")]
        public Nullable<System.DateTime> user_birth { get; set; }
        [Display(Name = "姓名")]
        public string user_name { get; set; }
        public string user_other { get; set; }

        public virtual ICollection<FavoriteRecord> FavoriteRecord { get; set; }
        public virtual ICollection<CommentRecord> CommentRecord { get; set; }
        public virtual ICollection<SearchRecord> SearchRecord { get; set; }
    }
}
