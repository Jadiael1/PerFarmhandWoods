using PerFarmhandWoods.Helpers;
using PerFarmhandWoods.Types;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using xTile;
using xTile.Layers;
using xTile.Tiles;

namespace PerFarmhandWoods.Events.AssetRequested
{
    public sealed class AssetRequested
    {
        private readonly IModHelper _helper;
        private readonly IMonitor _monitor;
        private readonly ITranslationHelper _translate;
        private readonly string _enterWoodsActionKey = Helpers.Constants.EnterWoodsActionKey;
        // private readonly string _baseWoodsName = Helpers.Constants.BaseWoodsName;

        public AssetRequested(IModHelper helper, IMonitor monitor)
        {
            _helper = helper;
            _monitor = monitor;
            _translate = helper.Translation;
            helper.Events.Content.AssetRequested += OnAssetRequested;
        }

        private void OnAssetRequested(object? sender, AssetRequestedEventArgs e)
        {
            ForestIntercept(sender, e);
            // WoodsIntercept(sender, e);
        }

        private void WoodsIntercept(object? sender, AssetRequestedEventArgs e)
        {
            if (!e.NameWithoutLocale.IsEquivalentTo("Maps/Woods"))
                return;

            e.Edit(asset =>
            {
                var editor = asset.AsMap();
                var map = editor.Data;
                var isWarp = map.Properties.TryGetValue("Warp", out var mapProp);

                if (!isWarp || string.IsNullOrWhiteSpace(mapProp))
                {
                    _monitor.Log($"If it does not have the warp property then we should not continue", LogLevel.Trace);
                    return;
                }
                List<WarpLocations>? warps = ListHelper.ConvertStringForList(mapProp);
                if (warps is null || warps.Count == 0)
                {
                    _monitor.Log($"If the conversion to list failed we should not continue", LogLevel.Trace);
                    return;
                }

                foreach (var warp in warps)
                {
                    if (warp.TargetName == "Forest")
                    {
                        warp.TargetX += 2;
                    }
                }

                var warpsStr = ListHelper.ConvertListForString(warps);
                if (warpsStr is null || string.IsNullOrWhiteSpace(warpsStr))
                {
                    return;
                }

                map.Properties["Warp"] = warpsStr;

            }, AssetEditPriority.Default);
        }

        private void ForestIntercept(object? sender, AssetRequestedEventArgs e)
        {
            if (!e.NameWithoutLocale.BaseName.StartsWith("Maps/Forest", StringComparison.OrdinalIgnoreCase))
                return;

            // ignore maps
            if (e.NameWithoutLocale.IsEquivalentTo("Maps/Forest_RaccoonHouse")) return;
            if (e.NameWithoutLocale.IsEquivalentTo("Maps/Forest_FishingDerby_Revert")) return;
            if (e.NameWithoutLocale.IsEquivalentTo("Maps/Forest_FishingDerbySign")) return;
            if (e.NameWithoutLocale.IsEquivalentTo("Maps/Forest-SewerClean")) return;
            if (e.NameWithoutLocale.IsEquivalentTo("Maps/Forest-FlowerFestival2")) return;
            if (e.NameWithoutLocale.IsEquivalentTo("Maps/Forest_FishingDerby")) return;
            if (e.NameWithoutLocale.IsEquivalentTo("Maps/Forest_RaccoonStump")) return;
            if (e.NameWithoutLocale.IsEquivalentTo("Maps/Forest_FishingDerbySign_Revert")) return;
            if (e.NameWithoutLocale.IsEquivalentTo("Maps/Forest-IceFestival")) return;
            if (e.NameWithoutLocale.IsEquivalentTo("Maps/Forest-FlowerFestival")) return;
            if (e.NameWithoutLocale.IsEquivalentTo("Maps/Forest-FlowerFestival")) return;

            e.Edit(asset =>
            {
                var editor = asset.AsMap();
                var map = editor.Data;
                Layer back = map.GetLayer("Back");

                var isWarp = map.Properties.TryGetValue("Warp", out var mapProp);
                if (!isWarp || string.IsNullOrWhiteSpace(mapProp))
                {
                    _monitor.Log($"If it does not have the warp property then we should not continue", LogLevel.Trace);
                    return;
                }

                List<WarpLocations>? warps = ListHelper.ConvertStringForList(mapProp);
                if (warps is null || warps.Count == 0)
                {
                    _monitor.Log($"If the conversion to list failed we should not continue", LogLevel.Trace);
                    return;
                }

                List<WarpLocations>? warpsWoods = warps.Where(w => w.TargetName == "Woods").ToList();
                if (warpsWoods is null || warpsWoods.Count == 0)
                {
                    _monitor.Log($"WarpsWoods not found", LogLevel.Trace);
                    return;
                }

                // Replace From Tile Properties
                for (int i = 0; i < warpsWoods.Count; i++)
                {
                    Tile? tile = FindAnyTile(map, warpsWoods[i].X, warpsWoods[i].Y);
                    if (tile is null)
                    {
                        _monitor.Log($"If tile is null we do nothing {warpsWoods[i].X}, {warpsWoods[i].Y}, {editor.Name}", LogLevel.Warn);
                        continue;
                    }
                    if (tile.Properties.ContainsKey("Action")) tile.Properties.Remove("Action");
                    if (tile.Properties.ContainsKey("TouchAction")) tile.Properties.Remove("TouchAction");

                    tile.Properties["TouchAction"] = $"{_enterWoodsActionKey} {warpsWoods[i].TargetX} {warpsWoods[i].TargetY}";
                }
            }, AssetEditPriority.Late);
        }

        private static Tile? FindAnyTile(Map map, int x, int y)
        {
            // try the most common layers first
            foreach (string name in new[] { "Back", "Buildings", "Front", "AlwaysFront" })
            {
                var layer = map.GetLayer(name);
                if (layer is null) continue;
                if (x >= layer.LayerWidth)
                {
                    x = layer.LayerWidth - 1;
                }
                if (x < 0)
                {
                    x = 0;
                }
                if (y >= layer.LayerHeight)
                {
                    y = layer.LayerHeight - 1;
                }
                if (y < 0)
                {
                    y = 0;
                }

                Tile? t = layer.Tiles[x, y];
                if (t != null) return t;
            }

            // fallback: any layer
            foreach (var layer in map.Layers)
            {
                if (layer is null) continue;
                if (x >= layer.LayerWidth)
                {
                    x = layer.LayerWidth - 1;
                }
                if (x < 0)
                {
                    x = 0;
                }
                if (y >= layer.LayerHeight)
                {
                    y = layer.LayerHeight - 1;
                }
                if (y < 0)
                {
                    y = 0;
                }
                Tile? t = layer.Tiles[x, y];
                if (t != null) return t;
            }
            return null;
        }

    }
}