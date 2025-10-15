using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;

namespace PerFarmhandWoods.TileActions
{
    public sealed class EnterWoodsAction
    {
        private readonly IMonitor _monitor;
        private readonly string _baseWoodsName = Helpers.Constants.BaseWoodsName;

        private readonly string _enterWoodsActionKey = Helpers.Constants.EnterWoodsActionKey;

        public EnterWoodsAction(IMonitor monitor)
        {
            _monitor = monitor;
            GameLocation.RegisterTouchAction(_enterWoodsActionKey, this.OnEnterWoods);
        }

        private void OnEnterWoods(GameLocation location, string[] args, Farmer player, Vector2 tile)
        {
            // if purge active, do nothing (let vanilla edge warp act)
            if (PerFarmhandWoods.Helpers.RuntimeFlags.PurgeMode) return;
            
            if (args.Length != 3 || args[0] != _enterWoodsActionKey) return;
            if (!int.TryParse(args[1], out int x) || !int.TryParse(args[2], out int y)) return;
            if (Context.IsMainPlayer) return;

            string target = $"{_baseWoodsName}{player.UniqueMultiplayerID}";
            Game1.warpFarmer(target, x, y, player.FacingDirection);
        }
    }
}
