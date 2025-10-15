using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace PerFarmhandWoods.Events.PeerConnected
{
    public class PeerConnected
    {
        private readonly IModHelper _helper;
        private readonly IMonitor _monitor;
        private readonly ITranslationHelper _translate;

        public PeerConnected(IModHelper helper, IMonitor monitor)
        {
            _helper = helper;
            _monitor = monitor;
            _translate = helper.Translation;
            helper.Events.Multiplayer.PeerConnected += this.OnPeerConnected;
        }

        private void OnPeerConnected(object? sender, PeerConnectedEventArgs e)
        {
            if (!Context.IsMainPlayer)
                return;
            
            PerFarmhandWoods.Helpers.WoodsInstance.Ensure(e.Peer.PlayerID, _monitor);
        }
    }
}