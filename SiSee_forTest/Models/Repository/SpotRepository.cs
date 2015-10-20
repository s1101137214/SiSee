using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiSee_v1.Models.ViewModels;

namespace SiSee_v1.Models.Repository
{
    public class SpotRepository
    {
        //
        private sisdbEntities1 db = new sisdbEntities1();

        #region Create


        #endregion

        #region Select

        public IEnumerable<Spot> GetAll()
        {
            return db.Spot;
        }

        #endregion
    }
}