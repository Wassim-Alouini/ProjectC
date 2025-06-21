using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    public Transform CameraPosition;

    void Update()
    {
        transform.position = CameraPosition.position;
    }
}
