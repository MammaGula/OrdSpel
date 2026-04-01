namespace OrdSpel.Shared
{
    public class ServiceResult<T>
    {
        public T? Data { get; set; }
        public string? Error { get; set; }
        public bool Success => Error == null;

        public static ServiceResult<T> Ok(T data) => new() { Data = data };
        public static ServiceResult<T> Fail(string error) => new() { Error = error };
    }
}
