using NClicker.Models;
using System.Collections.Generic;
using System.Linq;

namespace NClicker.Storage
{
    public class PresetStorage : IPresetStorage
    {
        public int TotalPresets => _storage.Count;

        private readonly LiteDatabaseMapper<string, RunConfiguration> _storage;

        public PresetStorage()
        {
            _storage = new LiteDatabaseMapper<string, RunConfiguration>("SharedPresetCollection");
        }

        public IEnumerable<RunConfiguration> Presets
        {
            get
            {
                return _storage.Count != 0
                    ? _storage.Values
                        .OrderByDescending(preset => preset.CreationDate).ToList()
                    : new List<RunConfiguration>();
            }
        }

        public void Add(RunConfiguration configuration)
        {
            _storage.Store(configuration.Name, configuration);
        }

        public void Update(RunConfiguration configuration)
        {
            Remove(configuration.Name);
            Add(configuration);
        }

        public void Remove(string name)
        {
            _storage.Remove(name);
        }

        public bool Contains(string key)
        {
            return _storage[key] != null;
        }
    }
}