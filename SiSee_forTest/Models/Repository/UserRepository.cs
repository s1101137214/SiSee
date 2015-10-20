using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiSee_v1.Models.Repository
{
    public class UserRepository
    {
        //Spot
        private sisdbEntities1 db = new sisdbEntities1();

        #region Create

        public void CreateUser()
        {

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