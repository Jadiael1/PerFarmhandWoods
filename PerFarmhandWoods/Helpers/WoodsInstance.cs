using PerFarmhandWoods.Types;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;

namespace PerFarmhandWoods.Helpers
{
    public static class WoodsInstance
    {
        private static readonly string _baseWoodsName = Helpers.Constants.BaseWoodsName;

        public static void Ensure(long playerId, IMonitor monitor)
        {
            try
            {
                string name = $"{_baseWoodsName}{playerId}";
                if (Game1.getLocationFromName(name) is not null)
                {
                    return;
                }

                if (!Game1.locationData.TryGetValue("Woods", out var baseData) || baseData?.CreateOnLoad == null)
                {
                    monitor.Log($"[PFW]: Data/Locations não contém 'Woods' (CreateOnLoad).", LogLevel.Error);
                    return;
                }

                var loc = Game1.CreateGameLocation(name, baseData.CreateOnLoad);
                if (loc == null)
                {
                    monitor.Log($"[PFW]: Falha ao criar localização '{name}' a partir de '{_baseWoodsName}'.", LogLevel.Error);
                    return;
                }

                // Add + 2 to the x coordinate of the exit from woods to forest
                var isWarp = loc.map.Properties.TryGetValue("Warp", out var mapProp);
                if (isWarp && !string.IsNullOrWhiteSpace(mapProp))
                {
                    List<WarpLocations>? warps = ListHelper.ConvertStringForList(mapProp);
                    if (warps is not null && warps.Count > 0)
                    {
                        foreach (var warp in warps)
                        {
                            if (warp.TargetName == "Forest")
                            {
                                warp.TargetX += 2;
                            }
                        }
                        var warpsStr = ListHelper.ConvertListForString(warps);
                        if (warpsStr is not null && !string.IsNullOrWhiteSpace(warpsStr))
                        {
                            loc.map.Properties["Warp"] = warpsStr;
                        }
                    }
                }

                Game1.locations.Add(loc);
                monitor.Log($"[PFW]: Instância criada: {name}", LogLevel.Info);
            }
            catch (Exception ex)
            {
                monitor.Log($"[PFW]: Erro ao garantir instância de Woods: {ex}", LogLevel.Error);
            }
        }
    }
}