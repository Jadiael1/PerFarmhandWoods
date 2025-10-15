using PerFarmhandWoods.Types;
using StardewModdingAPI;

namespace PerFarmhandWoods.Helpers
{
    public static class SaveData
    {
        private static PfwSaveData? _cache;
        private static readonly string _saveKey = PerFarmhandWoods.Helpers.Constants.SaveKey;

        public static PfwSaveData ReadSaveData(IModHelper helper, IMonitor monitor, ITranslationHelper translate)
        {
            if (_cache is not null)
                return _cache;

            try
            {
                _cache = helper.Data.ReadSaveData<PfwSaveData>(_saveKey) ?? new PfwSaveData();
            }
            catch (Exception ex)
            {
                monitor.Log($"Error ReadSaveData {ex.Message}", LogLevel.Error);
                _cache = new PfwSaveData();
            }
            return _cache;
        }

        public static void ResetCache()
        {
            _cache = null;
        }

        public static void WriteSaveData(PfwSaveData data, IModHelper helper)
        {
            _cache = data;
            helper.Data.WriteSaveData(_saveKey, data);
        }
    }
}