using GenericRepository.Dto.Abstract;

namespace GenericRepository.Dto.User.Response
{
    public class UserResponseDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}