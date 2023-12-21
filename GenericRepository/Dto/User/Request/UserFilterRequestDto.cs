using GenericRepository.Dto.Abstract;

namespace GenericRepository.Dto.User.Request
{
    public class UserFilterRequestDto : IDto
    {
        public string Name { get; set; }
        public bool Status { get; set; }
        
        public override string ToString()
        {
            var queryString =
                $"?Name={Name}&Status={Status}";
            return queryString;
        }
    }
}