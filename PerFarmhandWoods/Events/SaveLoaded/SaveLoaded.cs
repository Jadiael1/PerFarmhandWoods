using PerFarmhandWoods.Helpers;
using PerFarmhandWoods.Types;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace PerFarmhandWoods.Events.SaveLoaded
{
    public sealed class SaveLoaded
    {
        private readonly IModHelper _helper;
        private readonly IMonitor _monitor;
        private readonly ITranslationHelper _translate;
        // private static readonly string _saveKey = PerFarmhandWoods.Helpers.Constants.SaveKey;

        

        public SaveLoaded(IModHelper helper, IMonitor monitor)
        {
            _helper = helper;
            _monitor = monitor;
            _translate = helper.Translation;
            helper.Events.GameLoop.SaveLoaded += OnSaveLoaded;
        }

        private void OnSaveLoaded(object? sender, SaveLoadedEventArgs e)
        {
            // var asd = Game1.MasterPlayer.UniqueMultiplayerID;

            // Load for Invited
            // if (!Context.IsMainPlayer && Game1.player is not null)
            // {
            //     long uid = Game1.player.UniqueMultiplayerID;
            //     WoodsInstance.Ensure(uid, _monitor);
            // }

            if (!Context.IsMainPlayer)
                return;


            var eligible = PerFarmhandWoods.Helpers.EligibleFarmers.Get(_helper);
            if (eligible is null)
            {
                return;
            }
            foreach (long uid in eligible)
            {
                Farmer? farmer = Game1.GetPlayer(uid);
                if (farmer is null || farmer.IsMainPlayer)
                    continue;

                PerFarmhandWoods.Helpers.WoodsInstance.Ensure(uid, _monitor);
            }
        }
    }
}