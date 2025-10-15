using PerFarmhandWoods.Helpers;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace PerFarmhandWoods.Events.DayStarted
{
    public class DayStarted
    {
        private readonly IModHelper _helper;
        private readonly IMonitor _monitor;
        private readonly ITranslationHelper _translate;

        public DayStarted(IModHelper helper, IMonitor monitor)
        {
            _helper = helper;
            _monitor = monitor;
            _translate = helper.Translation;
            helper.Events.GameLoop.DayStarted += OnDayStarted;

        }
        public void OnDayStarted(object? sender, DayStartedEventArgs e)
        {
            // Load for Invited
            // if (!Context.IsMainPlayer && Game1.player is not null)
            // {
            //     long uid = Game1.player.UniqueMultiplayerID;
            //     WoodsInstance.Ensure(uid, _monitor);
            // }
        }
    }
}
