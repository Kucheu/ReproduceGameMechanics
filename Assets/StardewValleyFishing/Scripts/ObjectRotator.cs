using UnityEngine;

namespace Kucheu.StardewValleyFishing
{
    public class ObjectRotator
    {
        private Transform objectToRotate;

        public ObjectRotator(Transform objectToRotate)
        {
            this.objectToRotate = objectToRotate;
        }

        public void Rotate(Vector3 direction, float speed)
        {
            if (objectToRotate == null)
            {
                return;
            }

            objectToRotate.Rotate(direction, speed / Time.deltaTime);
        }
    }
}