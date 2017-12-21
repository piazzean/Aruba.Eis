using System;

namespace Aruba.Eis.Exceptions
{
    public class EisException : Exception
    {
        public static readonly EisException RepositoryError = new EisException("0001", "Internal Repository Error");
        public static readonly EisException RecordNotFound = new EisException("0002", "Record Not Found");
        public static readonly EisException RecordAlreadyExists = new EisException("0003", "Record Already Exists");

        public string Code { get; set; }

        public EisException(string message)
            : base(message)
        {
        }

        public EisException(string code, string message)
            : base(message)
        {
            this.Code = code;
        }

        public EisException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public EisException(string code, string message, Exception inner)
            : base(message, inner)
        {
            this.Code = code;
        }

        public EisException(EisException inner, string parameter)
            : base(String.Format(inner.Message, parameter))
        {
            this.Code = inner.Code;
        }
    }
}