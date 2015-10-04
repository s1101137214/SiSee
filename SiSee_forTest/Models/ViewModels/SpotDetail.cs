using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiSee_v1.Models.ViewModels
{
    public class SpotDetail
    {
        public Spot Spot { get; set; }

        public IEnumerable<CommentRecord> CommentRecords { get; set; }

    }
}