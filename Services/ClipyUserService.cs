using Keshav_Dev.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Keshav_Dev.Services
{
    public class ClipyUserService
    {
        private readonly IMongoCollection<ClipyUserFields> _ClipyCollection;

        public ClipyUserService(
            IOptions<ClipyUserDatabaseSettings> clipyUserDatabaseSettings)
        {

            var settings = MongoClientSettings.FromConnectionString(
                clipyUserDatabaseSettings.Value.ConnectionString);

            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            var client = new MongoClient(settings);

            var database = client.GetDatabase(
                clipyUserDatabaseSettings.Value.DatabaseName);

            _ClipyCollection = database.GetCollection<ClipyUserFields>(
                clipyUserDatabaseSettings.Value.CollectionName);
        }

        public async Task<List<ClipyUserFields>> GetAsync() =>
            await _ClipyCollection.Find(_ => true).ToListAsync();

        public async Task<ClipyUserFields?> GetAsync(string id) =>
            await _ClipyCollection.Find(x => x.Email == id).FirstOrDefaultAsync();

        public async Task CreateAsync(ClipyUserFields newUser) =>
            await _ClipyCollection.InsertOneAsync(newUser);

        public async Task UpdateAsync(string id, ClipyUserFields updatedClipyClipboard) =>
            await _ClipyCollection.ReplaceOneAsync(x => x.Email == id, updatedClipyClipboard);

        public async Task RemoveAsync(string id) =>
            await _ClipyCollection.DeleteOneAsync(x => x.Email == id);
    }
}
