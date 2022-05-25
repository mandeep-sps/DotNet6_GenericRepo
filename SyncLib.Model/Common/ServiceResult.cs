namespace SyncLib.Model.Common
{
    /// <summary>
    /// Service Result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceResult<T>
    {
        public T? Data { get; set; }
        public Exception? Exception { get; set; }
        public string Message { get; set; }
        public bool Success => Exception == null && !HasValidationError;
        public bool HasValidationError => Data == null;

        /// <summary>
        /// Service Result
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <param name="hasValidationError"></param>
        public ServiceResult(T data, string message)
        {
            Data = data;
            Message = message;
            Exception = null;
        }

        /// <summary>
        /// Service Result
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        public ServiceResult(Exception exception, string message)
        {
            Exception = exception;
            Message = message;
            Data = default;
        }

    }
}
