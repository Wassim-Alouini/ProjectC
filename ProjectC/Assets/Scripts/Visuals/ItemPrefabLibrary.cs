using UnityEngine;

public class ItemPrefabLibrary : MonoBehaviour
{
    public GameObject[] ItemPrefabs;
    public static ItemPrefabLibrary Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
