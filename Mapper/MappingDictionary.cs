using System.Linq.Expressions;

namespace Mapper
{
    public class MappingDictionary
    {
        public string SourceName { get; set; }
        public string DestinationName { get; set; }
        public LambdaExpression SourceExpression { get; set; }
        public LambdaExpression DestinationExpression { get; set; }
    }
}
