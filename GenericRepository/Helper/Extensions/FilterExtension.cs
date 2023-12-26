using System;
using System.Linq.Expressions;
using GenericRepository.Domain.Abstract;
using GenericRepository.Dto.Abstract;

namespace GenericRepository.Helper.Extensions
{
    public static class FilterExtension
    {
        public static Expression<Func<TEntity, bool>> ToQuery<TEntity, TDto>(this TDto dto)
            where TEntity : class, IEntity, new()
            where TDto : class, IDto, new()
        {
            return GetFilledPropertiesFilter<TEntity, TDto>(dto);
        }

        private static Expression<Func<TEntity, bool>> GetFilledPropertiesFilter<TEntity, TDto>(TDto dto)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "entity");
            Expression filterExpression = Expression.Constant(true);

            foreach (var property in typeof(TDto).GetProperties())
            {
                var propertyValue = property.GetValue(dto);
                if (propertyValue == null) continue;
                var propertyExpression = Expression.Property(parameter, property.Name);
                var valueExpression = Expression.Constant(propertyValue);
                var equalityExpression = Expression.Equal(propertyExpression, valueExpression);
                filterExpression = Expression.AndAlso(filterExpression, equalityExpression);
            }

            return Expression.Lambda<Func<TEntity, bool>>(filterExpression, parameter);
        }
    }
}