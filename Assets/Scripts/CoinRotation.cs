using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0f, 2f, 0f, Space.World);
    }
}
