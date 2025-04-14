using Models.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Cleaner
{
    public class LogCleaner : ILogCleaner
    {
        public async Task CleanLogs()
        {
            const int batchSize = 200;
            int pageIndex = 0;
            bool hasMore = true;

            IUnitOfWork unitOfWork = new UnitOfWork();

            while (hasMore)
            {
                var logs = unitOfWork.LogEntriesRepo
                    .GetAll()
                    .OrderBy(x => x.Id)
                    .Skip(pageIndex * batchSize)
                    .Take(batchSize)
                    .ToList();

                hasMore = logs.Any();

                foreach (var log in logs)
                {
                    IUnitOfWork uow = new UnitOfWork();
                    var cleandJsonOld = CleanJson(log.OldValues);
                    var cleandJsonNew = CleanJson(log.NewValues);

                    var logEntry = await uow.LogEntriesRepo.FindAsync(x => x.Id == log.Id);

                    logEntry.NewValues = cleandJsonNew;
                    logEntry.OldValues = cleandJsonOld;
                    await uow.SaveChangesAsync();
                }

                pageIndex++;
            }
        }

        private string CleanJson(string json)
        {
            if (string.IsNullOrWhiteSpace(json)) return json;

            var token = JsonConvert.DeserializeObject<JToken>(json);
            var cleanedToken = CleanJToken(token);

            return JsonConvert.SerializeObject(cleanedToken, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        private JToken CleanJToken(JToken token)
        {
            if (token.Type == JTokenType.Object)
            {
                var obj = (JObject)token;
                var cleaned = new JObject();

                foreach (var property in obj.Properties())
                {
                    var value = property.Value;
                    if (value.Type == JTokenType.Null) continue;
                    if (value.Type == JTokenType.String && string.IsNullOrWhiteSpace(value.ToString())) continue;

                    var cleanedValue = CleanJToken(value);
                    if (cleanedValue != null && !cleanedValue.IsNullOrEmpty())
                        cleaned[property.Name] = cleanedValue;
                }

                return cleaned;
            }
            else if (token.Type == JTokenType.Array)
            {
                var array = (JArray)token;
                var cleanedItems = array
                    .Select(CleanJToken)
                    .Where(x => x != null && !x.IsNullOrEmpty())
                    .ToList();

                return new JArray(cleanedItems);
            }
            else
            {
                return token;
            }
        }
    }
}
