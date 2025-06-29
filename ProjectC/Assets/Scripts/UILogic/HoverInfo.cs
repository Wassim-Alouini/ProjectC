using UnityEngine;

public class HoverInfo : MonoBehaviour
{
    public string DisplayName = "";
    public string Description= "";
    public string Action = "";

    private void Reset()
    {
        ValidateLookable();
    }

    private void OnValidate()
    {
        ValidateLookable();
    }

        private void ValidateLookable()
    {
        var lookable = GetComponent<ILookable>();
        if (lookable == null)
        {
            Debug.LogError($"LookableUIHandler requires a component on '{gameObject.name}' that implements ILookable. Removing self.");
            #if UNITY_EDITOR
                        UnityEditor.EditorApplication.delayCall += () =>
                        {
                            if (this != null) DestroyImmediate(this);
                        };
            #else
                        Destroy(this);
            #endif
        }
    }

}
