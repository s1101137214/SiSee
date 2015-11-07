using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiSee_v1.Models.Repository
{
    public class IRepository
    {
        protected SiSeeDBEntities db = new SiSeeDBEntities();
    }
}