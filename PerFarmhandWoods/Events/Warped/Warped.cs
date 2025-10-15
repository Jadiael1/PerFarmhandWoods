using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace PerFarmhandWoods.Events.Warped
{
    public class Warped
    {
        private readonly IModHelper _helper;
        private readonly IMonitor _monitor;
        private readonly ITranslationHelper _translate;
        private Point? _pendingWarpTile;
        private const string _baseWoodsName = Helpers.Constants.BaseWoodsName;
        private bool _warpInProgress;

        public Warped(IModHelper helper, IMonitor monitor)
        {
            _helper = helper;
            _monitor = monitor;
            _translate = helper.Translation;
            helper.Events.Player.Warped += this.OnWarped;
        }

        private void WarpToPersonalWoods(string target)
        {
            if (_warpInProgress) return;
            _warpInProgress = true;
            try
            {
                var tile = _pendingWarpTile ?? new Point(57, 15);
                Game1.warpFarmer(target, tile.X, tile.Y, Game1.player.FacingDirection);
                _pendingWarpTile = null;
            }
            catch (Exception ex)
            {
                _monitor.Log($"[PFW]: Falha ao redirecionar para {target}: {ex}", LogLevel.Error);
            }
            finally
            {
                _warpInProgress = false;
            }
        }

        private void OnWarped(object? sender, WarpedEventArgs e)
        {
            if (!e.IsLocalPlayer)
                return;

            if (Context.IsMainPlayer)
                return;

            if (!string.Equals(e.NewLocation?.Name, "Woods", StringComparison.Ordinal))
                return;

            if (string.Equals(e.OldLocation?.Name, "Woods", StringComparison.Ordinal))
                return;

            string target = $"{_baseWoodsName}{Game1.player.UniqueMultiplayerID}";

            _pendingWarpTile = Game1.player.TilePoint;

            WarpToPersonalWoods(target);
        }
    }
}