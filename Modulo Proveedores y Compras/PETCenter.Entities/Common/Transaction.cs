using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace PETCenter.Entities.Common
{
    public class Transaction
    {
        [DataMember]
        public TypeTransaction type { get; set; }
        [DataMember]
        public string message { get; set; }
    }

    public enum TypeTransaction
    {
        OK, ERR
    }   
}
