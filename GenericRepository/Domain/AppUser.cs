using GenericRepository.Domain.Abstract;
using Microsoft.AspNetCore.Identity;

namespace GenericRepository.Domain
{
    public class AppUser : IdentityUser<int>, IEntity
    {
    }
}