using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
namespace PetCenter.DataAccess
{

    public class DomainObjectFactoryBase<TDomainObject>
    {
        #region Fields
        public delegate TDomainObject MappingHandler(IDataReader reader);
        private MappingHandler mapping;
        #endregion

        #region Constructors
        public DomainObjectFactoryBase(MappingHandler mappingHandler)
        {
            mapping = mappingHandler;
        }
        #endregion

        #region Methods
        public TDomainObject Construct(IDataReader reader)
        {
            return mapping(reader);
        }
        #endregion
    }       

}
