using UnityEngine;

public class FixedTimeSetter : MonoBehaviour
{
    [SerializeField]
    private float updatedsPerSecond;

    private void Awake()
    {
        Time.fixedDeltaTime = 1/ updatedsPerSecond;
    }
}
