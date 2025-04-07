using System;
using UnityEngine;

namespace Kucheu.FishingMinigame
{
    public abstract class FishBehaviour
    {
        internal float fishPosition = 30f;
        internal float fishDiffuculty;
        internal float fishTargetPosition = 30f;
        internal bool hasTarget;

        public FishBehaviour(FishData fishData)
        {
            fishDiffuculty = fishData.fishDifficulty;
        }

        internal virtual bool TrySelectNewTargetPosition(float chance)
        {
            float random = UnityEngine.Random.Range(0f, 1f);
            if (random < chance)
            {
                float percent = UnityEngine.Random.Range(fishDiffuculty - 20f, fishDiffuculty - 5f) / 100f;
                percent = Mathf.Clamp(percent, 0f, 1f);
                float direction = UnityEngine.Random.Range(-fishPosition, 568 - fishPosition);
                if(direction == 0f)
                {
                    direction = 1f;
                }
                direction /= Math.Abs(direction);
                fishTargetPosition = fishPosition + (568 * percent * direction);
                fishTargetPosition = Mathf.Clamp(fishTargetPosition, 0f, 568f - 32f);
                hasTarget = true;
                return true;
            }
            return false;
        }

        internal virtual float GetNewTargetChance()
        {
            return fishDiffuculty / 3000f;
        }

        public virtual float Move()
        {
            TrySelectNewTargetPosition(GetNewTargetChance());
            if(hasTarget && Math.Abs(fishPosition - fishTargetPosition) < 2f)
            {
                hasTarget = false;
            }

            if (hasTarget)
            {
                var currentAcceleration = (fishTargetPosition - fishPosition) / (150 - fishDiffuculty);
                fishPosition += currentAcceleration;
                fishPosition = Math.Clamp(fishPosition, 0f, 568f - 32f);
            }
            return fishPosition;
        }
    }

    public class MixedFishBehaviour : FishBehaviour
    {
        public MixedFishBehaviour(FishData fishData) : base(fishData)
        {
        }
    }

    public class SmoothFishBehaviour : FishBehaviour
    {
        internal override float GetNewTargetChance()
        {
            return base.GetNewTargetChance() * 15f;
        }

        internal override bool TrySelectNewTargetPosition(float chance)
        {
            if(hasTarget)
            {
                return false;
            }

            return base.TrySelectNewTargetPosition(chance);
        }

        public SmoothFishBehaviour(FishData fishData) : base(fishData)
        {
        }
    }

    public class ContinuousMovementFishBehaviour : FishBehaviour
    {
        public enum FishContinuousMovementDirection
        {
            up = 0,
            down = 1
        }

        private float additionalSpeed = 1f;


        public ContinuousMovementFishBehaviour(FishData fishData, FishContinuousMovementDirection movementDirection) : base(fishData)
        {
            additionalSpeed *= movementDirection == FishContinuousMovementDirection.up ? 1 : -1;
        }

        public override float Move()
        {
            var baseMove = base.Move();
            fishPosition = Math.Clamp(baseMove + additionalSpeed, 0f, 568f - 32f);
            return fishPosition;
        }
    }

    public class DartFishBehaviour : FishBehaviour
    {
        public DartFishBehaviour(FishData fishData) : base(fishData)
        {
        }

        internal override float GetNewTargetChance()
        {
            return base.GetNewTargetChance() * 2;
        }

        internal override bool TrySelectNewTargetPosition(float chance)
        {
            float random = UnityEngine.Random.Range(0f, 1f);
            if (random < chance)
            {
                float positionChange = UnityEngine.Random.Range(30f, (fishDiffuculty * 2) + 60f);
                float direction = UnityEngine.Random.Range(0,2) == 0 ? -1 : 1;
                fishTargetPosition = fishPosition + (positionChange * direction);
                fishTargetPosition = Mathf.Clamp(fishTargetPosition, 0f, 568f - 32f);
                hasTarget = true;
                return true;
            }
            return false;
        }
    }
}