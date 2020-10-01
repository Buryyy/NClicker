using NClicker.Models;
using System.Collections.Generic;

namespace NClicker.Storage
{
    public interface IPresetStorage
    {
        int TotalPresets { get; }

        IEnumerable<RunConfiguration> Presets { get; }

        void Add(RunConfiguration configuration);

        void Update(RunConfiguration configuration);

        void Remove(string name);

        bool Contains(string key);
    }
}