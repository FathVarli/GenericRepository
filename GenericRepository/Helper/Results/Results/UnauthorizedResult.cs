using System.Net;
using GenericRepository.Helper.Results.Base;

namespace GenericRepository.Helper.Results.Results
{
    public class UnauthorizedResult : Result
    {
        public UnauthorizedResult(string message) : base(StatusTypeEnum.Unauthorized, (int)HttpStatusCode.Unauthorized,
            message)
        {
        }

        public UnauthorizedResult() : base(StatusTypeEnum.Unauthorized, (int)HttpStatusCode.Unauthorized)
        {
        }
    }
}