using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace ICTProfilingV3.Mapper
{
    public class MappingConfiguration
    {
        private List<MappingDictionary> _mappingDictionary = new List<MappingDictionary>();

        public void AddConfiguration<Source, Destination>(Action<MappingProfile<Source, Destination>> action)
        {
            var mappingProfile = new MappingProfile<Source, Destination>();
            action.Invoke(mappingProfile);
            _mappingDictionary.AddRange(mappingProfile.MappingDictionary);
        }

        public Destination MapTo<Source, Destination>(Source source)
        {
            var sourceName = typeof(Source).Name;
            var destinationName = typeof(Destination).Name;

            var mappingDics = _mappingDictionary
                .Where(x => x.SourceName == sourceName && x.DestinationName == destinationName)
                .ToList();

            if (!mappingDics.Any())
                throw new InvalidOperationException("Mapping not found");

            var destination = Activator.CreateInstance<Destination>();

            foreach (var mappingDic in mappingDics)
            {
                var sourceExpression = mappingDic.SourceExpression as Expression<Func<Source, object>>;
                var destinationExpression = mappingDic.DestinationExpression as Expression<Func<Destination, object>>;

                var value = source.SafeGet(sourceExpression.Compile());
                var property = destinationExpression.Body.GetMemberExpression().Member as PropertyInfo;

                property.SetValue(destination, value);
            }

            return destination;
        }

        public async Task<Destination> MapToAsync<Source, Destination>(Source source)
        {
            var sourceName = typeof(Source).Name;
            var destinationName = typeof(Destination).Name;

            var mappingDics = _mappingDictionary
                .Where(x => x.SourceName == sourceName && x.DestinationName == destinationName)
                .ToList();

            if (!mappingDics.Any())
                throw new InvalidOperationException("Mapping not found");

            var destination = Activator.CreateInstance<Destination>();

            var tasks = mappingDics.Select(mappingDic =>
            {
                return Task.Run(() =>
                {
                    var sourceExpression = mappingDic.SourceExpression as Expression<Func<Source, object>>;
                    var destinationExpression = mappingDic.DestinationExpression as Expression<Func<Destination, object>>;

                    var value = source.SafeGet(sourceExpression.Compile());
                    var property = destinationExpression.Body.GetMemberExpression().Member as PropertyInfo;

                    property.SetValue(destination, value);
                });
            });

            await Task.WhenAll(tasks);
            return destination;
        }
    }
    public class MappingProfile<Source, Destination>
    {
        public List<MappingDictionary> MappingDictionary { get; set; } = new List<MappingDictionary>();

        public void CreateMap(Expression<Func<Source, object>> source, Expression<Func<Destination, object>> destination)
        {
            MappingDictionary.Add(new MappingDictionary
            {
                SourceExpression = source,
                DestinationExpression = destination,
                SourceName = typeof(Source).Name,
                DestinationName = typeof(Destination).Name,
            });
        }
    }

    public static class ObjectExtensions
    {
        public static object SafeGet<T>(this T obj, Func<T, object> func)
        {
            return obj == null ? null : func(obj);
        }
    }

    public static class ExpressionExtensions
    {
        public static MemberExpression GetMemberExpression(this Expression expression)
        {
            if (expression is MemberExpression memberExpression)
            {
                return memberExpression;
            }

            if (expression is UnaryExpression unaryExpression)
            {
                // Unwrap the UnaryExpression (e.g., Convert) to get the member expression
                if (unaryExpression.Operand is MemberExpression operandMemberExpression)
                {
                    return operandMemberExpression;
                }
            }

            throw new InvalidOperationException("Expression is not a MemberExpression");
        }
    }
}
