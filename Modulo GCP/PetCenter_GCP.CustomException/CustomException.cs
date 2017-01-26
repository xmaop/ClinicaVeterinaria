using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCenter_GCP.CustomException
{
    #region Exception interface

    public interface ICustomException
    {
        string ErrorCode { get; }
        string ErrDescription { get; set; }
        Exception InnerExceptionObj { get; set; }
    }
    #endregion

    #region Layer Class
    public class Layer
    {
        public const string Entity = "Entity";
        public const string DataAccess = "DataAccess";
        public const string BizLogic = "BizLogic";
        public const string Service = "Service";
        public const string Web = "Web";

    }
    #endregion
            
    #region Module Class
    public class Module
    {
        public const string AddRecord = "AgregarRegistro";
        public const string DisplayRecord = "MostrarRegistro";
        public const string DeleteRecord = "EliminarRegistro";
        public const string ModifyRecord = "ModificarRegistro";
        public const string FillRecord = "LlenarRegistro";
        public const string ValidateRecord = "ValidarRegistro";
    }
    #endregion

    #region CustomExceptionBase Class
    public abstract class CustomExceptionBase : Exception, ICustomException
    {
        private int _actualNumber;
        private string _layer;
        private string _module;
        private string _description;
        private Exception _innerException;
        protected string _errorCode;

        public CustomExceptionBase() { }

        public CustomExceptionBase(string layer, string module,
            int actualNumber, string description)
        {
            this._layer = layer;
            this._module = module;
            this._actualNumber = actualNumber;
            this._description = description;

        }
        public CustomExceptionBase(string layer, string module, int actualNumber,
            string description, Exception innerException)
        {

            this._layer = layer;
            this._module = module;
            this._actualNumber = actualNumber;
            this._description = description;
            this._innerException = innerException;

        }
        public string LayerType
        {
            get { return _layer; }
            set { _layer = value; }
        }

        public string ModuleType
        {
            get { return _module; }
            set { _module = value; }
        }

        public string ErrorCode
        {
            get
            {
                SetErrorCode();
                return _errorCode;
            }
        }

        public string ErrDescription
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        public Exception InnerExceptionObj
        {
            get
            {
                return _innerException;
            }
            set
            {
                _innerException = value;
            }
        }
        public virtual string SetErrorCode()
        {
            string actualNumber = String.Concat("000", _actualNumber.ToString());
            actualNumber = actualNumber.Substring(actualNumber.Length - 3, 3);
            _errorCode = String.Format("{0}{1}{2}", _layer, _module, actualNumber);
            return _errorCode;

        }

    }
    #endregion

    #region CustomDataValidationException

    public sealed class CustomDataValidationException : CustomExceptionBase
    {
        public CustomDataValidationException()
        { }

        public CustomDataValidationException(string layer, string module, int actualNumber, string description) :
            base(layer, module, actualNumber, description)
        { }

        public CustomDataValidationException(string layer, string module, int actualNumber, string description, Exception innerException) :
            base(layer, module, actualNumber, description, innerException)
        { }
    }

    #endregion

    #region CustomSqlException Class

    public sealed class CustomSqlException : CustomExceptionBase
    {
        public CustomSqlException() { }

        public CustomSqlException(string layer, string module, int actualNumber, string description) :
            base(layer, module, actualNumber, description)
        { }

        public CustomSqlException(string layer, string module, int actualNumber, string description, Exception innerException) :
            base(layer, module, actualNumber, description, innerException)
        { }
    }
    #endregion
}
