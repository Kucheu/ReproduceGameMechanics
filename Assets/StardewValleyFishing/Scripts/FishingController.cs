using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Kucheu.StardewValleyFishing
{
    public class FishingController : MonoBehaviour
    {
        public const float fishSpriteHeight = 32f;

        public event Action<bool> FishingEnd;

        [SerializeField]
        private InputActionReference keyInput;

        private FishBehaviour fishBehaviour;
        private FishingBarBehaviour fishingBar;
        private float fishPosition;
        private float barSize;
        private float barPosition;
        private bool isActive;
        private float catchingProgress;
        private bool isFishInBar;

        public float FishPosition => fishPosition;
        public float BarSize => barSize;
        public float BarPosition => barPosition;
        public float CatchingProgress => catchingProgress;
        public bool IsFishInBar => isFishInBar;

        public void Setup(FishingRodData rodData, FishData fishData)
        {
            fishBehaviour = CreateFishBehaviour(fishData);
            fishingBar = new FishingBarBehaviour(rodData);
            barSize = fishingBar.GetBarSize();
            catchingProgress = 0.3f;
            isActive = true;
            Time.fixedDeltaTime = 0.01f;
        }

        private void FixedUpdate()
        {
            if (!isActive)
            {
                return;
            }
            fishPosition = fishBehaviour.Move();
            barPosition = fishingBar.CalculateBarPosition(keyInput.action.IsPressed());
            isFishInBar = fishPosition >= barPosition && fishPosition + fishSpriteHeight <= barPosition + barSize;
            catchingProgress += (isFishInBar ? 0.002f : -0.002f);

            if (catchingProgress >= 1f || catchingProgress <= 0f)
            {
                bool isCatch = catchingProgress >= 1f;
                FishingEnd?.Invoke(isCatch);
                isActive = false;
            }
        }

        private FishBehaviour CreateFishBehaviour(FishData fishData)
        {
            return fishData.fishType switch
            {
                FishType.Mixed => new MixedFishBehaviour(fishData),
                FishType.Smooth => new SmoothFishBehaviour(fishData),
                FishType.Sinker => new ContinuousMovementFishBehaviour(fishData, ContinuousMovementFishBehaviour.FishContinuousMovementDirection.down),
                FishType.Floater => new ContinuousMovementFishBehaviour(fishData, ContinuousMovementFishBehaviour.FishContinuousMovementDirection.up),
                FishType.Dart => new DartFishBehaviour(fishData),
                _ => new MixedFishBehaviour(fishData)
            };
        }
    }
}