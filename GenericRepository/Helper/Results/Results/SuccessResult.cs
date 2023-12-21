using System.Net;
using GenericRepository.Helper.Results.Base;

namespace GenericRepository.Helper.Results.Results
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message) : base(StatusTypeEnum.Success, (int)HttpStatusCode.OK, message)
        {
        }

        public SuccessResult() : base(StatusTypeEnum.Success, (int)HttpStatusCode.OK)
        {
        }
    }
}