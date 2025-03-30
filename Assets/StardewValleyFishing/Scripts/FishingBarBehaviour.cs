using System;

namespace Kucheu.StardewValleyFishing
{
    public class FishingBarBehaviour
    {
        private FishingRodData rodData;
        float barPosition = 30f;
        float barSize;
        float barSpeed;

        public FishingBarBehaviour(FishingRodData rodData)
        {
            this.rodData = rodData;
            barSize = GetBarSize();
        }

        public float CalculateBarPosition(bool isKeyPressed)
        {
            float gravity = isKeyPressed ? 0.1f : -0.1f;
            if (isKeyPressed && barPosition == 0f)
            {
                barSpeed = 0f;
            }
            barSpeed += gravity;
            barPosition += barSpeed;
            if (barPosition >= 568f - barSize)
            {
                barSpeed = 0f;
            }
            if (barPosition <= 0)
            {
                barSpeed = -barSpeed * 2 / 3;
            }
            barPosition = Math.Clamp(barPosition, 0f, 568f - barSize);
            return barPosition;
        }

        public float GetBarSize()
        {
            return 96 + (rodData.fishingLevel * 8) + (rodData.firstTackle == TackleType.CorkBobber ? 24 : 0) + (rodData.secondTackle == TackleType.CorkBobber ? 24 : 0) + (rodData.useDeluxeBait ? 12 : 0);
        }
    }
}