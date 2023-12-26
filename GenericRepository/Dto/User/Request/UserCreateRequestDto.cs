using GenericRepository.Dto.Abstract;

namespace GenericRepository.Dto.User.Request
{
    public class UserCreateRequestDto : IDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}