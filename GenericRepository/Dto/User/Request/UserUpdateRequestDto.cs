using GenericRepository.Dto.Abstract;

namespace GenericRepository.Dto.User.Request
{
    public class UserUpdateRequestDto : IDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}