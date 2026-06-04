using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuQuestions.Application.Common.Exceptions
{
    public class BusinessRuleException : Exception
    {
        public BusinessRuleException(string message) : base(message) { }
    }

}
