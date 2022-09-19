using NClicker.Models;
using System.Collections.Generic;

namespace NClicker.Storage
{
    public interface IPresetRepository
    {
        int GetPresetsCount();
        void Upsert(RunConfiguration configuration);

        void Remove(string name);

        bool Contains(string presetName);
        IEnumerable<RunConfiguration> GetAllPresets();

        /// <summary>
        /// Little bit slower as this is being orderd by creation date, not that it matters though.
        /// </summary>
        /// <returns></returns>
        IEnumerable<RunConfiguration> GetAllPresetsOrdered();
    }
}