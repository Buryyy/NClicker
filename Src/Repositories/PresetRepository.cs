using LiteDB;
using NClicker.Models;
using NClicker.Storage;
using System.Collections.Generic;

namespace NClicker.Repositories
{
    public class PresetRepository : IPresetRepository
    {
        private const string CollectionName = "Presets";
        private readonly ILiteDatabase _repository;

        public PresetRepository()
        {
            _repository = new LiteDatabase(Constants.LiteDbConnectionString, BsonMapper.Global);
        }

        public void Upsert(RunConfiguration configuration)
        {
            _repository.GetCollection<RunConfiguration>(CollectionName).Upsert(new BsonValue(configuration.Name), configuration);
        }

        public bool Contains(string presetName) => _repository.GetCollection<RunConfiguration>(CollectionName).FindById(new BsonValue(presetName)) != null;

        public void Remove(string name) => _repository.GetCollection<RunConfiguration>(CollectionName).Delete(name);

        public IEnumerable<RunConfiguration> GetAllPresets() => _repository.GetCollection<RunConfiguration>(CollectionName).FindAll();

        public IEnumerable<RunConfiguration> GetAllPresetsOrdered() =>
            _repository.GetCollection<RunConfiguration>(CollectionName).Find(Query.All("CreationDate", Query.Ascending));

        public int GetPresetsCount() => _repository.GetCollection<RunConfiguration>(new BsonValue(CollectionName)).Count();
    }
}