using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;

namespace Kucheu.FishingMinigame
{
    public class MenuUi : MonoBehaviour
    {
        [SerializeField]
        private FishingController fishingController;
        [SerializeField]
        private FishingUI fishinUI;
        [SerializeField]
        private TMP_Dropdown firstTackleDropdown;
        [SerializeField]
        private TMP_Dropdown secondTackleDropdown;
        [SerializeField]
        private TMP_Dropdown fishTypeDropdown;
        [SerializeField]
        private Slider fishDifficultySlider;
        [SerializeField]
        private Slider fishingLevelSlider;
        [SerializeField]
        private Toggle baitToggle;

        [SerializeField]
        private GameObject fishingUIGameObject;
        [SerializeField]
        private GameObject mainUI;

        private void Awake()
        {
            SetFishTypeDropdown();
            SetTackleDropdown(firstTackleDropdown);
            SetTackleDropdown(secondTackleDropdown);
        }

        private void SetFishTypeDropdown()
        {
            fishTypeDropdown.ClearOptions();
            List<string> fishTypeOptions = new();
            foreach (var fishType in Enum.GetValues(typeof(FishType)))
            {
                fishTypeOptions.Add(fishType.ToString());
            }
            fishTypeDropdown.AddOptions(fishTypeOptions);
        }

        private void SetTackleDropdown(TMP_Dropdown dropDown)
        {
            dropDown.ClearOptions();
            List<string> tacklesOptions = new();
            foreach (var fishType in Enum.GetValues(typeof(TackleType)))
            {
                tacklesOptions.Add(fishType.ToString());
            }
            dropDown.AddOptions(tacklesOptions);
        }

        public void StartFishing()
        {
            fishingController.Setup(GetFishRodData(), GetFishData());
            fishinUI.Setup();
            fishingController.FishingEnd += OnFishingEnd;
            fishingUIGameObject.SetActive(true);
            mainUI.SetActive(false);
        }

        private void OnFishingEnd(bool isCatch)
        {
            fishingController.FishingEnd -= OnFishingEnd;
            fishingUIGameObject.SetActive(false);
            mainUI.SetActive(true);
        }

        private FishData GetFishData()
        {
            return new FishData(
                fishType: (FishType)fishTypeDropdown.value,
                fishDifficulty: fishDifficultySlider.value
                   );
        }

        private FishingRodData GetFishRodData()
        {
            return new FishingRodData(
                fishingLevel: (int)fishingLevelSlider.value,
                firstTackle: (TackleType)firstTackleDropdown.value,
                secondTackle: (TackleType)firstTackleDropdown.value,
                useDeluxeBait: baitToggle.isOn
                );
        }
    }
}