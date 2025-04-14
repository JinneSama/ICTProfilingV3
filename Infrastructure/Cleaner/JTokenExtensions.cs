
using Newtonsoft.Json.Linq;

namespace Infrastructure.Cleaner
{
    public static class JTokenExtensions
    {
        public static bool IsNullOrEmpty(this JToken token)
        {
            return token == null ||
                   token.Type == JTokenType.Null ||
                   (token.Type == JTokenType.String && string.IsNullOrWhiteSpace(token.ToString())) ||
                   (token.Type == JTokenType.Array && !token.HasValues) ||
                   (token.Type == JTokenType.Object && !token.HasValues);
        }
    }
}
