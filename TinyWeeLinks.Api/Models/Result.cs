using System;
namespace TinyWeeLinks.Api.Models
{
    public class Result<T> where T : class
    {
        public T Data { get; set; }
        public int Status { get; set; }
        public string ErrorMessage { get; set;}

        public Result(int status)
        {
            Status = status;
        }
    }
}