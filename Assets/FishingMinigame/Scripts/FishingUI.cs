using UnityEngine;
using UnityEngine.UI;

namespace Kucheu.FishingMinigame
{
    public class FishingUI : MonoBehaviour
    {
        [SerializeField]
        private Transform fishTransform;
        [SerializeReference]
        private Transform shakeFishTransform;
        [SerializeField]
        private Transform bar;
        [SerializeField]
        private RectTransform barRectTransform;
        [SerializeField]
        private Slider progressBar;
        [SerializeField]
        private Transform reelTransform;
        [SerializeField]
        private float reelRotationSpeed;

        [SerializeField]
        private FishingController fishingController;
        [SerializeField]
        private ShakeController shakeController;

        private bool isFishInBar;
        private ObjectRotator objectRotator;


        public void Setup()
        {
            barRectTransform.sizeDelta = new Vector2(barRectTransform.sizeDelta.x, fishingController.BarSize);
            objectRotator = new ObjectRotator(reelTransform);
        }

        private void FixedUpdate()
        {
            fishTransform.localPosition = new Vector3(0f, fishingController.FishPosition, 0f);
            bar.localPosition = new Vector3(0f, fishingController.BarPosition, 0f);
            progressBar.value = fishingController.CatchingProgress;

            if (fishingController.IsFishInBar && !isFishInBar)
            {
                isFishInBar = true;
                shakeController.StartShake(Vector3.left, shakeFishTransform);
            }
            else if (!fishingController.IsFishInBar && isFishInBar)
            {
                isFishInBar = false;
                shakeController.StopShake();
            }

            if (isFishInBar)
            {
                objectRotator.Rotate(Vector3.back, reelRotationSpeed);
            }
            else
            {
                objectRotator.Rotate(Vector3.forward, reelRotationSpeed/8);
            }
        }
    }
}