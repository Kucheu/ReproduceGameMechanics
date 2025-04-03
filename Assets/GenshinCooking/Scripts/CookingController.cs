using UnityEngine;
using System.Collections.Generic;
using System;

namespace Kucheu.GenshinCooking
{
    public class CookingController : MonoBehaviour
    {
        public event Action<CookingResult> CookingEnd;

        [SerializeField]
        private List<CookingDifficultyData> cookingDifficultyDatas;
        [SerializeField]
        private float pointerSpeed;


        private bool isActive;
        private float goodPercent;
        private float perfectPercent;
        private float startGoodField;
        private float startPerfectField;
        private float currentPointerPositionPercent;

        public float CurrentPointerPositionPercent => currentPointerPositionPercent;
        public float StartGoodField => startGoodField;
        public float StartPerfectField => startPerfectField;
        public float GoodPercent => goodPercent;
        public float PerfectPercent => perfectPercent;

        public void Setup(CookingDifficulty cookingDifficulty)
        {
            var cookingData = cookingDifficultyDatas.Find(x => x.Difficulty == cookingDifficulty).GetPercents();
            goodPercent = cookingData.goodPercent;
            perfectPercent = cookingData.perfectPercent;
            startGoodField = UnityEngine.Random.Range(0f, 1f - goodPercent);
            startPerfectField = startGoodField + ((goodPercent - perfectPercent) / 2);
            currentPointerPositionPercent = 0f;
            isActive = true;
        }

        private void Update()
        {
            if(!isActive)
            {
                return;
            }
            

            currentPointerPositionPercent += pointerSpeed * Time.deltaTime;
            if(currentPointerPositionPercent >= 1f)
            {
                OnClick();
            }
        }

        public void OnClick()
        {
            isActive = false;
            if( currentPointerPositionPercent >= startPerfectField && currentPointerPositionPercent <= startPerfectField + perfectPercent)
            {
                CookingEnd?.Invoke(CookingResult.perfect);
                return;
            }
            if (currentPointerPositionPercent >= startGoodField && currentPointerPositionPercent <= startGoodField + goodPercent)
            {
                CookingEnd?.Invoke(CookingResult.good);
                return;
            }
            CookingEnd?.Invoke(CookingResult.fail);
        }
    }
}