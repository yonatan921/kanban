using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class Response<T>
    {

        public String ErrorMessage { get; set; }
        public T ReturnValue { get; set; }

        public Response()
        {
        }

        public Response(String ErrorMessage, T ReturnValue)
        {
            this.ErrorMessage = ErrorMessage;
            this.ReturnValue = ReturnValue;
        }
    }
}