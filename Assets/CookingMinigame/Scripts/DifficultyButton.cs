using UnityEngine;
using UnityEngine.UI;

namespace Kucheu.CookingMinigame
{
    public class DifficultyButton : MonoBehaviour
    {
        [SerializeField]
        private Image difficultyImage;
        [SerializeField]
        private Toggle toggle;

        private CookingDifficulty difficulty;

        public bool IsSelected => toggle.isOn;
        public CookingDifficulty Difficulty => difficulty;

        public void Setup(Color buttonColor, CookingDifficulty difficulty, ToggleGroup toggleGroup)
        {
            difficultyImage.color = buttonColor;
            this.difficulty = difficulty;
            toggle.group = toggleGroup;
        }

        public void SelectButton()
        {
            toggle.isOn = true;
        }
    }
}

