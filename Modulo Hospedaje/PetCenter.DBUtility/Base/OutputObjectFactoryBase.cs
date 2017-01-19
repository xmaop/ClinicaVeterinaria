using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;


namespace PetCenter.DataAccess
{
    public class OutputObjectFactoryBase<TOutputObject>
    {        
        #region Fields
        public delegate void SettingHandler(Database db, DbCommand command);
        private SettingHandler setting;
        #endregion

        #region Constructors
        public OutputObjectFactoryBase(SettingHandler settingHandler)
        {
            setting = settingHandler;
        }
        #endregion
        
        #region Methods
        public void SetValue(Database db, DbCommand command)
        {
            setting(db, command);
        }
        #endregion
    }

}
