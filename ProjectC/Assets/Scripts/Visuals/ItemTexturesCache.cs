using System.Collections.Generic;
using UnityEngine;

public class ItemTexturesCache : MonoBehaviour
{
    public static ItemTexturesCache Instance { get; private set; }

    private Dictionary<Item, Texture2D> _cache = new Dictionary<Item, Texture2D>();

    [System.Serializable]
    public class TextureEntry
    {
        public string itemName;
        public Texture2D texture;
    }

    [SerializeField]
    private List<TextureEntry> debugCacheView = new List<TextureEntry>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Add(Item item, Texture2D texture)
    {
        if (!_cache.ContainsKey(item))
        {
            _cache[item] = texture;
            UpdateDebugView();
        }
    }

    public bool TryGet(Item item, out Texture2D texture)
    {
        return _cache.TryGetValue(item, out texture);
    }

    public void Clear()
    {
        _cache.Clear();
        debugCacheView.Clear();
    }

    public IReadOnlyDictionary<Item, Texture2D> GetAll()
    {
        return _cache;
    }

    public void Remove(Item item)
    {
        if (_cache.Remove(item))
        {
            UpdateDebugView();
        }
    }

    private void UpdateDebugView()
    {
#if UNITY_EDITOR
        debugCacheView.Clear();
        foreach (var kvp in _cache)
        {
            debugCacheView.Add(new TextureEntry
            {
                itemName = kvp.Key?.Name ?? "Unnamed",
                texture = kvp.Value
            });
        }
#endif
    }
}
