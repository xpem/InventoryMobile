using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BLLResponse
    {
        public bool Success { get; set; }

        public Object? Content { get; set; }

        public ErrorTypes? Error { get; set; }

    }

    public enum ErrorTypes
    {
        TokenExpired = 0, Unknown = 1, ServerUnavaliable = 2,WrongEmailOrPassword = 3
    }
}
