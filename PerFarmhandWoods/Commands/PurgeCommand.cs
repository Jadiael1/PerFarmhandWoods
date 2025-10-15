using StardewModdingAPI;
using StardewValley;

namespace PerFarmhandWoods.Commands
{
    internal sealed class PurgeCommand
    {
        private readonly IModHelper _helper;
        private readonly IMonitor _monitor;
        private const string Name = "pfw_purge";
        private const string Desc = "Delete all Woods_* locations from the current world and disable PFW saving for this session. Usage: pfw_purge";

        public PurgeCommand(IModHelper helper, IMonitor monitor)
        {
            _helper = helper;
            _monitor = monitor;
            _helper.ConsoleCommands.Add(Name, Desc, this.Run);
        }

        private void Run(string cmd, string[] args)
        {
            if (!Context.IsWorldReady)
            {
                _monitor.Log("Load a save first.", LogLevel.Warn);
                return;
            }
            if (!Context.IsMainPlayer)
            {
                _monitor.Log("Host only: run this on the host.", LogLevel.Warn);
                return;
            }

            var eligible = PerFarmhandWoods.Helpers.EligibleFarmers.Get(_helper);

            // Activate runtime flags
            Helpers.RuntimeFlags.DisableSaving = true;
            Helpers.RuntimeFlags.PurgeMode = true;

            // Safe warp: pull any player out of Woods_* before removing them
            if (eligible is not null)
            {
                foreach (long uid in eligible)
                {
                    Farmer? farmer = Game1.GetPlayer(uid);
                    if (farmer is null) continue;

                    if (farmer?.currentLocation?.Name is string locName && locName.StartsWith(Helpers.Constants.BaseWoodsName))
                    {
                        var src = farmer.TilePoint;
                        var w = new StardewValley.Warp(src.X, src.Y, "Forest", 2, 7, flipFarmer: false);
                        farmer.warpFarmer(w, farmer.FacingDirection);
                    }
                }
            }

            // Remove every Woods_* from the locations list
            int removed = 0;
            for (int i = Game1.locations.Count - 1; i >= 0; i--)
            {
                var loc = Game1.locations[i];
                if (loc?.Name is string name && name.StartsWith(Helpers.Constants.BaseWoodsName))
                {
                    Game1.locations.RemoveAt(i);
                    removed++;
                }
            }

            // Clear the mod's saved data
            Helpers.SaveData.WriteSaveData(new Types.PfwSaveData(), _helper);
            Helpers.SaveData.ResetCache();

            _monitor.Log($"PFW: purged {removed} Woods_* location(s); saving disabled for this session.", LogLevel.Info);
            _monitor.Log("Tip: keep playing or save+quit. Re-enable by restarting the game/session.", LogLevel.Trace);
        }
    }
}
