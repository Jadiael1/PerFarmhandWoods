using PerFarmhandWoods.Helpers;
using StardewModdingAPI;
using StardewModdingAPI.Enums;
using StardewModdingAPI.Events;
using StardewValley;

namespace PerFarmhandWoods.Events.LoadStageChanged
{
    public class LoadStageChanged
    {
        private readonly IModHelper _helper;
        private readonly IMonitor _monitor;
        private readonly ITranslationHelper _translate;

        public LoadStageChanged(IModHelper helper, IMonitor monitor)
        {
            _helper = helper;
            _monitor = monitor;
            _translate = helper.Translation;
            helper.Events.Specialized.LoadStageChanged += OnLoadStageChanged;
        }

        private void OnLoadStageChanged(object? sender, LoadStageChangedEventArgs e)
        {
            if (!Context.IsMainPlayer)
                return;

            if (e.NewStage is not LoadStage.SaveAddedLocations)
                return;

            var saveData = Helpers.SaveData.ReadSaveData(_helper, _monitor, _translate);

            foreach (var ouid in saveData.OwnerUids)
            {
                WoodsInstance.Ensure(ouid, _monitor);
            }
        }
    }
}