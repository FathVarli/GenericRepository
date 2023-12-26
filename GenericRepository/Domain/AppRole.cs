using GenericRepository.Domain.Abstract;
using Microsoft.AspNetCore.Identity;

namespace GenericRepository.Domain
{
    public class AppRole : IdentityRole<int>, IEntity
    {
    }
}