namespace Kucheu.StardewValleyFishing
{
    public struct FishData
    {
        public FishData(FishType fishType, float fishDifficulty)
        {
            this.fishType = fishType;
            this.fishDifficulty = fishDifficulty;
        }

        public FishType fishType;
        public float fishDifficulty;

    }
}