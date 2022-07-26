using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public static class JsonController<T>
    {
        public static string toJson(object obj)
        {
            var jsonOptions = new JsonSerializerOptions();
            jsonOptions.WriteIndented = true;
            jsonOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            return JsonSerializer.Serialize(obj, obj.GetType(), jsonOptions);
        }

        // public static string toJson(T t)
        // {
        //     var jsonOptions = new JsonSerializerOptions();
        //     jsonOptions.WriteIndented = true;
        //     jsonOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        //     return JsonSerializer.Serialize(t, t.GetType(), jsonOptions);
        // }

        public static Response<T> fromJson(string json)
        {
            // var jsonOptions = new JsonSerializerOptions();
            // jsonOptions.WriteIndented = true;
            // jsonOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            Response<T> res = JsonSerializer.Deserialize<Response<T>>(json);
            return res;
        }
    }
}