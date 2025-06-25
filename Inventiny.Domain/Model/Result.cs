using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventiny.Domain.Model
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public bool IsError { get; set; }
         
        public EnumResponseType type { get; set; }
        public bool IsValidationError { get { return type == EnumResponseType.ValidationError; } }
        public bool IsSystemError { get { return type == EnumResponseType.SystemError; } }

        public string Message { get; set; }

        public T Data { get; set; }

        public static Result<T> Success (T data, string message = "Success")
        {
            return new Result<T>
            {
                IsSuccess = true,
                IsError = false,
                type = EnumResponseType.Success,
                Message = message,
                Data = data
            };
        }

        public static Result<T> ValidationError (string message)
        {
            return new Result<T>
            {
                IsSuccess = false,
                IsError = true,
                type = EnumResponseType.ValidationError,
                Message = message,
                Data = default(T)
            };
        }

        public static Result<T> SystemError(string message)
        {
            return new Result<T>
            {
                IsSuccess = false,
                IsError = true,
                type = EnumResponseType.SystemError,
                Message = message,
                Data = default(T)
            };
        }



       

    }

    public class PagedResult<T>
    {
        public List<T> Data { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }


    public enum EnumResponseType
    {
        None=0,
        Success,
        ValidationError,
        SystemError,

    }
}
