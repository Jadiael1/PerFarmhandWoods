namespace PerFarmhandWoods.Helpers
{
    internal static class RuntimeFlags
    {
        /// <summary>When true, OnSaving returns without writing anything.</summary>
        public static bool DisableSaving { get; set; }

        /// <summary>When true, the TouchAction to enter the personal Woods does nothing (avoids warp to a location that has been purged).</summary>
        public static bool PurgeMode { get; set; }
    }
}
