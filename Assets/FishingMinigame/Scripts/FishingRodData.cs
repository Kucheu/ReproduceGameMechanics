namespace Kucheu.FishingMinigame
{
    public struct FishingRodData
    {
        public FishingRodData(int fishingLevel, TackleType firstTackle, TackleType secondTackle, bool useDeluxeBait)
        {
            this.fishingLevel = fishingLevel;
            this.firstTackle = firstTackle;
            this.secondTackle = secondTackle;
            this.useDeluxeBait = useDeluxeBait;
        }

        public int fishingLevel;
        public TackleType firstTackle;
        public TackleType secondTackle;
        public bool useDeluxeBait;
    }
}