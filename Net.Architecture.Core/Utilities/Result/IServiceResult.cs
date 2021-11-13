namespace Net.Architecture.Core.Utilities.Result
{
    public interface IServiceResult
    {
        string Message { get; }
        int StatusCode { get; set; }
        bool Result { get; }//#ToDo json ignore
        IServiceResult NotFound();
        IServiceResult BadRequest();
        IServiceResult InternalServerError();
    }

    public interface IServiceResult<out T> : IServiceResult
    {
        T Data { get; }
    }
}
