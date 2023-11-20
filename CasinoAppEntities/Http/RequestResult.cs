using System.Collections.Generic;


namespace CasinoApp.Entities.Http
{
    public sealed class RequestResult<T>
    {
        #region Fields
        public bool IsSuccessful { get; set; }
        public bool IsError { get; set; }
        public List<string> ErrorsMessage { get; set; }
        public byte[] RawBytes { get; set; }
        public T Result { get; set; }

        #endregion

        #region Builder
        public RequestResult()
        {
        }
        public RequestResult(bool succcess, bool error, List<string> errors, T result)
        {
            IsSuccessful = succcess;
            IsError = error;
            ErrorsMessage = errors;
            Result = result ?? default;
        }
        #endregion

        public static RequestResult<T> CreateSuccess(T result)
        {
            return new RequestResult<T>(true, false, null, result);
        }

        public static RequestResult<T> CreateError(List<string> errors)
        {
            return new RequestResult<T>(false, true, errors, default);
        }

        public static RequestResult<T> CreateError(string error)
        {
            return new RequestResult<T>(false, true, new List<string>() { error }, default);
        }

        public static RequestResult<T> CreateNoSuccess(string error)
        {
            return new RequestResult<T>(false, false, new List<string>() { error }, default);
        }
        public static RequestResult<T> CreateNoSuccess(List<string> error)
        {
            return new RequestResult<T>(false, false, error, default);
        }
    }
}
