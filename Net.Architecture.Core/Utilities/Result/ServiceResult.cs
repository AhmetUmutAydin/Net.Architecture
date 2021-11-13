using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Net.Architecture.Core.Utilities.Result
{
    public class ServiceResult<T> : ServiceResult, IServiceResult<T>
    {
        public ServiceResult(bool result = true) : base(result) { }
        public ServiceResult(string message, bool result = false) : base(message, result) { }
        public ServiceResult(T data) : this(true)
        {
            Data = data;
        }

        public ServiceResult(T data, bool result = true) : base(result)
        {
            Data = data;
        }
        public ServiceResult(T data, string message, bool result = true) : base(message, result)
        {
            Data = data;
        }
        public T Data { get; }
    }

    public class ServiceResult : IServiceResult
    {
        public ServiceResult(bool result = true)
        {
            Result = result;
        }
        public ServiceResult(string message, bool result = false) : this(result)
        {
            Message = message;
        }

        public bool Result { get; }
        public string Message { get; }
        public int StatusCode { get; set; }

        public IServiceResult NotFound()
        {
            StatusCode = StatusCodes.Status404NotFound;
            return this;
        }
        public IServiceResult BadRequest()
        {
            StatusCode = StatusCodes.Status400BadRequest;
            return this;
        }
        public IServiceResult InternalServerError()
        {
            StatusCode = StatusCodes.Status500InternalServerError;
            return this;
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
