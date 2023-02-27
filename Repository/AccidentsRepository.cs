using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RestAPI.Database;
using RestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Repository
{
    public class AccidentsRepository : IAccidentsRepository
    {
        private readonly IMongoCollection<Accident> _accidentsCollection;
        public AccidentsRepository(IOptions<AccidentsStoreDatabaseSettings> accidentsStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
            accidentsStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                accidentsStoreDatabaseSettings.Value.DatabaseName);

            _accidentsCollection = mongoDatabase.GetCollection<Accident>(
                accidentsStoreDatabaseSettings.Value.AccidentsCollection);
        }

        public async Task<List<Accident>> GetAccident() =>
        await _accidentsCollection.Find(_ => true).ToListAsync();

        public async Task<Accident?> GetAccidentByID(string id) =>
            await _accidentsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task InsertAccident(Accident newAccident) =>
            await _accidentsCollection.InsertOneAsync(newAccident);

        public async Task UpdateAccident(string id, Accident updateAccident) =>
            await _accidentsCollection.ReplaceOneAsync(x => x.Id == id, updateAccident);

        public async Task DeleteAccident(string id) =>
            await _accidentsCollection.DeleteOneAsync(x => x.Id == id);

    }
}
