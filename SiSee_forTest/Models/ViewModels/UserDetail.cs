using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiSee_v1.Models.ViewModels
{
    public class UserDetail
    {
        public User User { get; set; }

        public IEnumerable<CommentRecord> CommentRecords { get; set; }

        public IEnumerable<FavoriteRecord> FavoriteRecord { get; set; }

        public IEnumerable<SearchRecord> SearchRecord { get; set; }
    }
}