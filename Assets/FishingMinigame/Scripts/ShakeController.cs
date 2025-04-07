using UnityEngine;

namespace Kucheu.FishingMinigame
{
    public class ShakeController: MonoBehaviour
    {
        [SerializeField]
        float amplitute;
        [SerializeField]
        float frequency;

        private Vector3 direction;
        private Transform transformToShake;

        private void FixedUpdate()
        {
            if(transformToShake != null)
            {
                transformToShake.localPosition = Mathf.Sin(Time.time * frequency) * direction * amplitute;
            }
        }

        public void StartShake(Vector3 direction, Transform transformToShake)
        {
            this.direction = direction;
            this.transformToShake = transformToShake;
        }

        public void StopShake()
        {
            transformToShake.localPosition = Vector3.zero;
            transformToShake = null;
        }
    }
}