using System.Linq;
using Mapster;

namespace GenericRepository.Helper.Mapper.Mapster
{
    public class MapsterAdapter : IMapperAdapter
    {
        public TTarget Map<TTarget>(object itemToMapFrom) where TTarget : class
        {
            return itemToMapFrom.Adapt<TTarget>();
        }

        public TTarget Map<TSource, TTarget>(TSource itemToMapFrom) where TSource : class where TTarget : class
        {
            return itemToMapFrom.Adapt<TSource, TTarget>();
        }

        public TTarget Map<TSource, TTarget>(TSource itemToMapFrom, TTarget itemToMapTo)
            where TSource : class where TTarget : class
        {
            return itemToMapFrom.Adapt(itemToMapTo);
        }

        public IQueryable<TTarget> ProjectTo<TSource, TTarget>(IQueryable<TSource> source)
        {
            return source.ProjectToType<TTarget>();
        }
    }
}