using System;

namespace Kucheu.FishingMinigame
{
    public class FishingBarBehaviour
    {
        private FishingRodData rodData;
        float barSize;
        float barSpeed;
        int numberOfBarbedHooks;

        public FishingBarBehaviour(FishingRodData rodData)
        {
            this.rodData = rodData;
            barSize = GetBarSize();
            numberOfBarbedHooks = (rodData.firstTackle == TackleType.BarbedHook ? 1 : 0) + (rodData.secondTackle == TackleType.BarbedHook ? 1 : 0);
        }

        public void  Move(ref float barPosition, bool isKeyPressed, float fishPosition, bool isFishInBar)
        {
            float gravity = 0f;
            if (isKeyPressed && barPosition == 0f)
            {
                barSpeed = 0f;
            }
            if(isFishInBar && numberOfBarbedHooks > 0)
            {
                float centerOfBar = barPosition + (barSize / 2);
                float centerOfFish = fishPosition + 16f;
                for (int i = 0; i < numberOfBarbedHooks; i++)
                {
                    if (centerOfBar != centerOfFish)
                    {
                        float direction = (centerOfFish - centerOfBar) / Math.Abs((centerOfFish - centerOfBar));
                        gravity += (0.02f * direction);
                    }
                }
            }
            else
            {
                gravity = isKeyPressed ? 0.1f : -0.1f;
            }
            barSpeed += gravity;
            barPosition += barSpeed;

            if (barPosition >= 568f - barSize)
            {
                barSpeed = 0f;
            }
            if (barPosition <= 0)
            {
                barSpeed = rodData.firstTackle == TackleType.LeadBobber || rodData.secondTackle == TackleType.LeadBobber ? 0f : (-barSpeed * 3/4);
            }
            barPosition = Math.Clamp(barPosition, 0f, 568f - barSize);
        }

        public float GetBarSize()
        {
            return 96 + (rodData.fishingLevel * 8) + (rodData.firstTackle == TackleType.CorkBobber ? 24 : 0) + (rodData.secondTackle == TackleType.CorkBobber ? 24 : 0) + (rodData.useDeluxeBait ? 12 : 0);
        }
    }
}