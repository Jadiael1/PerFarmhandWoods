using PerFarmhandWoods.Types;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace PerFarmhandWoods.Events.Saving
{
    public sealed class Saving
    {
        private readonly IModHelper _helper;
        private readonly string _saveKey = Helpers.Constants.SaveKey;
        private readonly string _baseWoodsName = Helpers.Constants.BaseWoodsName;

        public Saving(IModHelper helper)
        {
            _helper = helper;
            helper.Events.GameLoop.Saving += OnSaving;
        }

        public void OnSaving(object? sender, SavingEventArgs e)
        {
            if (!Context.IsMainPlayer)
                return;

            if (PerFarmhandWoods.Helpers.RuntimeFlags.DisableSaving)
                return;

            // rewrites the list from what is in the world (idempotent)
            PfwSaveData? data = new();
            foreach (GameLocation? loc in Game1.locations)
            {
                if (loc?.Name is string name && name.StartsWith(_baseWoodsName, StringComparison.Ordinal))
                {
                    if (long.TryParse(name.Substring(6), out long uid))
                        data.OwnerUids.Add(uid);
                }
            }
            _helper.Data.WriteSaveData(_saveKey, data);
        }
    }
}