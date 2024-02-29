using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SamsysDemo.Infrastructure.Helpers
{
    public class MessagingHelper
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";

        public string ErrorType { get; set; } = null;

        public void SetMessage(string message)
        {
            Message = message;
        }       
    }

    public class MessagingHelper<T> : MessagingHelper
    {
        public T Obj { get; set; }
    }   
}