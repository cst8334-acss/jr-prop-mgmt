using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JenningsRE.ResultTransfer
{
    public abstract class ObjectResult
    {
        private String Value { set; get; }
        public String GetValue()
        {
            return Value;
        }
        public void SetValue(String value)
        {
            this.Value = value;
        }
    }

    public class OkResult : ObjectResult
    {
        public OkResult(String value)
        {
            SetValue(value);
        }
    }
    public class NotFoundResult : ObjectResult
    {
        public NotFoundResult(String value)
        {
            SetValue(value);
        }
    }
    public class InvalidResult : ObjectResult
    {
        public InvalidResult(String value)
        {
            SetValue(value);
        }
    }

    public class WrongResult : ObjectResult
    {
        public WrongResult(String value)
        {
            SetValue(value);
        }
    }
}
