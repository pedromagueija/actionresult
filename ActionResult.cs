namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class ActionResult<TRet>
    {
        private readonly List<ActionError> errorList = new List<ActionError>();

        public IEnumerable<ActionError> Errors
        {
            get
            {
                return errorList;
            }
        }

        public TRet ReturnValue { get; private set; }
        public bool Success { get; private set; } = true;

        public ActionResult<TRet> Fail(ActionError error)
        {
            if (error == null)
            {
                throw new ArgumentNullException(nameof(error));
            }

            errorList.Add(error);
            Success = false;

            return this;
        }

        public ActionResult<TRet> Fail(IEnumerable<ActionError> errors)
        {
            if (errors == null)
            {
                throw new ArgumentNullException(nameof(errors));
            }

            // silently ignore any null errors on the given error list
            var actualErrors = errors.Where(e => e != null);
            
            errorList.AddRange(actualErrors);

            return this;
        }

        public ActionResult<TRet> Succeed(TRet returnValue)
        {
            ReturnValue = returnValue;
            Success = true;
            return this;
        }        
    }

    internal class ActionError
    {
        public string Code { get; }
        public string Description { get; }

        public ActionError(string code, string description)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentException(@"Cannot be null or empty", nameof(code));
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentException(@"Cannot be null or empty", nameof(description));
            }

            Code = code;
            Description = description;
        }
    }
}