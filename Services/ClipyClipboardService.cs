using Keshav_Dev.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace KloudReach.Services
{
    public class ClipyClipboardService
    {
        private readonly IMongoCollection<ClipyClipboardFields> _ClipyCollection;

        public ClipyClipboardService(
            IOptions<ClipyClipboardDatabaseSettings> clipyClipboardDatabaseSettings)
        {

            var settings = MongoClientSettings.FromConnectionString(
                clipyClipboardDatabaseSettings.Value.ConnectionString);

            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            var client = new MongoClient(settings);

            var database = client.GetDatabase(
                clipyClipboardDatabaseSettings.Value.DatabaseName);

            _ClipyCollection = database.GetCollection<ClipyClipboardFields>(
                clipyClipboardDatabaseSettings.Value.CollectionName);
        }

        public async Task<List<ClipyClipboardFields>> GetAsync() =>
            await _ClipyCollection.Find(_ => true).ToListAsync();

        public async Task<ClipyClipboardFields?> GetAsync(string id) =>
            await _ClipyCollection.Find(x => x.IdShared == id).FirstOrDefaultAsync();

        public async Task CreateAsync(ClipyClipboardFields newBook) =>
            await _ClipyCollection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string id, ClipyClipboardFields updatedClipyClipboard) =>
            await _ClipyCollection.ReplaceOneAsync(x => x.IdShared == id, updatedClipyClipboard);

        public async Task RemoveAsync(string id) =>
            await _ClipyCollection.DeleteOneAsync(x => x.IdShared == id);
    }
}
