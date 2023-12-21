using System.Net;
using GenericRepository.Helper.Results.Base;

namespace GenericRepository.Helper.Results.Results
{
    public class NotFoundResult : Result
    {
        public NotFoundResult(string message) : base(StatusTypeEnum.NotFound, (int)HttpStatusCode.NotFound, message)
        {
        }

        public NotFoundResult() : base(StatusTypeEnum.NotFound, (int)HttpStatusCode.OK)
        {
        }
    }
}