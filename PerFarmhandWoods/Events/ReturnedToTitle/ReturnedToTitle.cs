using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace PerFarmhandWoods.Events.ReturnedToTitle
{
    public class ReturnedToTitle
    {
        private readonly IModHelper _helper;
        private readonly IMonitor _monitor;
        private readonly ITranslationHelper _translate;

        public ReturnedToTitle(IModHelper helper, IMonitor monitor)
        {
            _helper = helper;
            _monitor = monitor;
            _translate = helper.Translation;
            helper.Events.GameLoop.ReturnedToTitle += OnReturnedToTitle;
        }

        private void OnReturnedToTitle(object? sender, ReturnedToTitleEventArgs e)
        {
            Helpers.SaveData.ResetCache();
        }
    }
}