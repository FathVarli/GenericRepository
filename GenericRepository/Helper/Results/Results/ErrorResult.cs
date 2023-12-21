using System.Net;
using GenericRepository.Helper.Results.Base;

namespace GenericRepository.Helper.Results.Results
{
    public class ErrorResult : Result
    {
        public ErrorResult(string message) : base(StatusTypeEnum.Failed, (int)HttpStatusCode.BadRequest, message)
        {
        }

        public ErrorResult() : base(StatusTypeEnum.Failed, (int)HttpStatusCode.BadRequest)
        {
        }
    }
}