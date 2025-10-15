namespace PerFarmhandWoods.Types
{
    [Serializable]
    public class PfwSaveData
    {
        public HashSet<long> OwnerUids { get; set; } = new();
    }
}