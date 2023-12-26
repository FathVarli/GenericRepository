using GenericRepository.Dto.Abstract;

namespace GenericRepository.Dto.User.Request
{
    public class UserFilterRequestDto : IDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            var queryString =
                $"?UserName={UserName}&Email={Email}";
            return queryString;
        }
    }
}