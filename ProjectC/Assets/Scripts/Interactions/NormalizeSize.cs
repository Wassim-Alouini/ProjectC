using UnityEngine;

public class NormalizeSize : MonoBehaviour
{
    [Range(0.1f, 1)]
    public float scaleFactor;

    void Start()
    {
        Normalize();
        //Debug.LogWarning("Normalizing");
    }

    void Normalize()
    {
        transform.localScale = transform.localScale * scaleFactor;
    }
}