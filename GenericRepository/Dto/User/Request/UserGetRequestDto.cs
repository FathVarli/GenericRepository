using GenericRepository.Dto.Abstract;

namespace GenericRepository.Dto.User.Request
{
    public class UserGetRequestDto : IDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public bool? Status { get; set; }
        
        public override string ToString()
        {
            var queryString =
                $"?Id={Id}Name={Name}&Status={Status}";
            return queryString;
        }
    }
}