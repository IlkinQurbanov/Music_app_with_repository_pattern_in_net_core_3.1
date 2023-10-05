using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyMusic.Core.Models;
using MyMusic.Data.MongoDB.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMusic.Data.MongoDB.Setting
{
    public class DatabaseSettings : IDatabaseSettings
    {
        private readonly IMongoDatabase _db;

        public DatabaseSettings(IOptions<Settings> options, IMongoClient client)
        {
            _db = client.GetDatabase(options.Value.Database);
        }
        public IMongoCollection<Composer> Composers => _db.GetCollection<Composer>("Composers");
    }
}
