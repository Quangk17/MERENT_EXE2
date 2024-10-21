using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceRespones
{
    public class ServiceResponse<T>
    {
        public T ?Data { get; set; }
        public bool Success { get; set; } = false;
        public string? Message { get; set; } = null;
        public string? Error { get; set; } = null;
        public List<string>? ErrorMessages { get; set; } = null;

        public static ServiceResponse<T> Succeed(T? data, string message)
        {
            return new ServiceResponse<T> { Success = true, Data = data, Message = message };
        }

        public static ServiceResponse<T> isError(T? data, string Message)
        {
            return new ServiceResponse<T> { Success = false, Data = data, Message = Message };
        }

        public static ServiceResponse<object> Fail(Exception ex)
        {
            return new ServiceResponse<object>
            {
                Success = false,
                Data = null,
                Message = ex.Message
            };
        }
    }
}
