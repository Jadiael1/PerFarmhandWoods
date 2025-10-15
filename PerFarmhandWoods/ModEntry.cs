using StardewModdingAPI;
using PerFarmhandWoods.Events.SaveLoaded;
using PerFarmhandWoods.Events.PeerConnected;
using PerFarmhandWoods.Events.LoadStageChanged;
using PerFarmhandWoods.Events.Saving;
using PerFarmhandWoods.Events.DayStarted;
using PerFarmhandWoods.Events.ReturnedToTitle;
using PerFarmhandWoods.TileActions;
using PerFarmhandWoods.Events.AssetRequested;

namespace PerFarmhandWoods
{
    internal sealed class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            _ = new AssetRequested(helper, this.Monitor);
            _ = new EnterWoodsAction(this.Monitor);
            _ = new SaveLoaded(helper, this.Monitor);
            _ = new PeerConnected(helper, this.Monitor);
            _ = new LoadStageChanged(helper, this.Monitor);
            _ = new Saving(helper);
            _ = new DayStarted(helper, this.Monitor);
            _ = new ReturnedToTitle(helper, this.Monitor);
        }
    }
}
