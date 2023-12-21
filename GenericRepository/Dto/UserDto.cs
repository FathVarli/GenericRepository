using GenericRepository.Dto.Abstract;

namespace GenericRepository.Dto
{
    public class UserDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}