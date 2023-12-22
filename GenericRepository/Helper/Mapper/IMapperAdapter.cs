using System.Linq;

namespace GenericRepository.Helper.Mapper
{
    public interface IMapperAdapter
    {
        TTarget Map<TTarget>(object itemToMapFrom) where TTarget : class;

        TTarget Map<TSource, TTarget>(TSource itemToMapFrom) where TSource : class where TTarget : class;

        TTarget Map<TSource, TTarget>(TSource itemToMapFrom, TTarget itemToMapTo)
            where TSource : class where TTarget : class;

        IQueryable<TTarget> ProjectTo<TSource, TTarget>(IQueryable<TSource> source);
    }
}
