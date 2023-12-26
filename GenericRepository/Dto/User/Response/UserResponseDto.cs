using GenericRepository.Dto.Abstract;

namespace GenericRepository.Dto.User.Response
{
    public class UserResponseDto : IDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
    }
}