using GenericRepository.Dto.Abstract;

namespace GenericRepository.Dto.User.Request
{
    public class UserCreateRequestDto : IDto
    {
        public string Name { get; set; }
    }
}