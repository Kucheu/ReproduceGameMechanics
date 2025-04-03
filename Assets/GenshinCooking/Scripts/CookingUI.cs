using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine.UI;

namespace Kucheu.GenshinCooking
{
    public class CookingUI : MonoBehaviour
    {
        [Serializable]
        private struct ImageToResult
        {
            public CookingResult result;
            public Sprite resultSprite;
            public string resultText;
        }

        [SerializeField]
        private GameObject startUI;
        [SerializeField]
        private GameObject cookingUI;
        [SerializeField]
        private GameObject resultUI;

        [Header("Start UI")]
        [SerializeField]
        private TMP_Dropdown difficultyDropdown;
        [SerializeField]
        private CookingController cookingController;

        [Header("Result UI")]
        [SerializeField]
        private List<ImageToResult> resultData;
        [SerializeField]
        private TextMeshProUGUI resultText;
        [SerializeField]
        private Image resultImage;

        [Header("Cooking UI")]
        [SerializeField]
        private Image goodFieldImage;
        [SerializeField]
        private Image perfectFieldImage;
        [SerializeField]
        private Transform cookingIndicatorTransform;

        private void Start()
        {
            difficultyDropdown.ClearOptions();
            List<string> difficultyList = new();
            foreach (var difficulty in Enum.GetValues(typeof(CookingDifficulty)))
            {
                difficultyList.Add(difficulty.ToString());
            }
            difficultyDropdown.AddOptions(difficultyList);
        }

        private void Update()
        {
            if (!cookingUI.activeInHierarchy)
            {
                return;
            }

            cookingIndicatorTransform.localRotation = Quaternion.Euler(0f, 0f, -(180f * cookingController.CurrentPointerPositionPercent));
        }

        public void OnCookingStart()
        {
            cookingController.Setup((CookingDifficulty)difficultyDropdown.value);
            SetupCookingUI();
            startUI.SetActive(false);
            cookingUI.SetActive(true);
            cookingController.CookingEnd += OnFishingEnd;
        }

        private void OnFishingEnd(CookingResult result)
        {
            cookingUI.SetActive(false);
            resultUI.SetActive(true);
            var resultData = this.resultData.Find(x => x.result == result);
            resultImage.sprite = resultData.resultSprite;
            resultText.text = resultData.resultText;
            cookingController.CookingEnd -= OnFishingEnd;
            StartCoroutine(ResultWindowCoroutine());
        }

        private IEnumerator ResultWindowCoroutine()
        {
            yield return new WaitForSeconds(1f);
            resultUI.SetActive(false);
            startUI.SetActive(true);
        }

        private void SetupCookingUI()
        {
            perfectFieldImage.fillAmount = 0.5f * cookingController.PerfectPercent;
            goodFieldImage.fillAmount = 0.5f * cookingController.GoodPercent;
            perfectFieldImage.transform.localRotation = Quaternion.Euler(0f, 0f, -(180f * cookingController.StartPerfectField));
            goodFieldImage.transform.localRotation = Quaternion.Euler(0f, 0f, -(180f * cookingController.StartGoodField));
            cookingIndicatorTransform.localRotation = Quaternion.Euler(0f, 0f, -(180f * cookingController.CurrentPointerPositionPercent));
        }
    }
}

