using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Architecture
{
    public class Result<T>
    {
        public T Data { get; private set; }
        public string Exception { get; private set; }

        public bool IsSuccess()
        {
            return Data != null;
        }

        public static Result<T> Error(string message)
        {
            Result<T> result = new Result<T>();
            result.Exception = message;
            return result;
        }

        public static Result<T> Success(T data)
        {
            Result<T> result = new Result<T>();
            result.Data = data;
            return result;
        }
    }
}
