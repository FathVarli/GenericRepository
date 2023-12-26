using GenericRepository.Dto.Abstract;

namespace GenericRepository.Dto.User.Request
{
    public class UserGetRequestDto : IDto
    {
        public int? Id { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            var queryString =
                $"?Id={Id}&Email={Email}";
            return queryString;
        }
    }
}