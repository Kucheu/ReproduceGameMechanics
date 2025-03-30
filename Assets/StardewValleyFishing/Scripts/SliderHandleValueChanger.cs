using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Kucheu.StardewValleyFishing
{
    public class SliderHandleValueChanger : MonoBehaviour
    {
        [SerializeField]
        private Slider slider;
        [SerializeField]
        private TextMeshProUGUI handleText;

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
            handleText.text = value.ToString();
        }
    }
}
