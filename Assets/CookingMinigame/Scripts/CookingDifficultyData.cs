using UnityEngine;

namespace Kucheu.CookingMinigame
{
    [CreateAssetMenu(fileName = "CookingDifficultyData", menuName = "Kucheu/CookingDifficultyData")]
    public class CookingDifficultyData : ScriptableObject
    {
        [SerializeField]
        private CookingDifficulty difficulty;
        [SerializeField, Range(0f, 1f)]
        private float goodPercent;
        [SerializeField, Range(0f, 1f)]
        private float perfectPercent;

        public CookingDifficulty Difficulty => difficulty;

        public (float goodPercent, float perfectPercent) GetPercents()
        {
            return (goodPercent, perfectPercent);
        }

        private void OnValidate()
        {
            if(goodPercent < perfectPercent)
            {
                Debug.LogError("GoodPercent can't be greater than PerfectPercent");
            }
            if(goodPercent <= 0f)
            {
                Debug.LogError("GoodPercent can't be smaller or equal than 0");
            }
            if(perfectPercent <= 0f)
            {
                Debug.LogError("PerfectPercent can't be smaller or equal than 0");
            }
        }
    }
}