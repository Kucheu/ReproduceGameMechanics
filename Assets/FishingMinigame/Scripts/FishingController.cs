using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Kucheu.FishingMinigame
{
    public class FishingController : MonoBehaviour
    {
        public event Action<bool> FishingEnd;

        [SerializeField]
        private InputActionReference keyInput;

        private IFish fishBehaviour;
        private FishingBarBehaviour fishingBar;
        private float fishPosition;
        private float barSize;
        private float barPosition;
        private bool isActive;
        private float catchingProgress;
        private bool isFishInBar;
        private FishingRodData fishingRodData;


        public float FishPosition => fishPosition;
        public float BarSize => barSize;
        public float BarPosition => barPosition;
        public float CatchingProgress => catchingProgress;
        public bool IsFishInBar => isFishInBar;

        public void Setup(FishingRodData rodData, FishData fishData)
        {
            fishBehaviour = SetFishBehaviourStrategy(fishData);
            fishingBar = new FishingBarBehaviour(rodData);
            fishingRodData = rodData;
            barSize = fishingBar.GetBarSize();
            catchingProgress = 0.3f;
            isActive = true;
        }

        private void FixedUpdate()
        {
            if (!isActive)
            {
                return;
            }
            fishBehaviour.Move(ref fishPosition);
            fishingBar.Move(ref barPosition, keyInput.action.IsPressed(), fishPosition, isFishInBar);
            isFishInBar = fishPosition >= barPosition && fishPosition + 32f <= barPosition + barSize;
            catchingProgress += GetCatchingProgress();

            if (catchingProgress >= 1f || catchingProgress <= 0f)
            {
                bool isCatch = catchingProgress >= 1f;
                FishingEnd?.Invoke(isCatch);
                isActive = false;
            }
        }

        private float GetCatchingProgress()
        {
            float catchingProgressChange;
            if(isFishInBar)
            {
                catchingProgressChange = 0.001f;
            }
            else
            {
                catchingProgressChange = -0.001f;
                catchingProgressChange *= fishingRodData.firstTackle == TackleType.TrapBobber ? 0.7f : 1f;
                catchingProgressChange *= fishingRodData.secondTackle == TackleType.TrapBobber ? 0.7f : 1f;
            }
            return catchingProgressChange;
        }

        private IFish SetFishBehaviourStrategy(FishData fishData)
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