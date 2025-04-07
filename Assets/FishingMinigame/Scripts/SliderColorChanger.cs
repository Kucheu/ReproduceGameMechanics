using UnityEngine;
using UnityEngine.UI;

namespace Kucheu.FishingMinigame
{
    public class SliderColorChanger : MonoBehaviour
    {
        [SerializeField]
        private Slider slider;
        [SerializeField]
        private Gradient progressBarColorGradient;
        [SerializeField]
        private Image progressBarImage;

        private void OnEnable()
        {
            slider.onValueChanged.AddListener(ChangeColor);
            ChangeColor(slider.value);
        }

        private void OnDisable()
        {
            slider.onValueChanged.RemoveListener(ChangeColor);
        }

        private void ChangeColor(float value)
        {
            progressBarImage.color = progressBarColorGradient.Evaluate(value);
        }
    }
}