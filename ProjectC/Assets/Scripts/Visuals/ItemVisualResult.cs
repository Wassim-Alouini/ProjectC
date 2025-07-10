using UnityEngine;

public class ItemVisualResult
{
    public GameObject Visual;
    public Mesh ColliderMesh;

    public ItemVisualResult(GameObject visual, Mesh colliderMesh = null)
    {
        Visual = visual;
        ColliderMesh = colliderMesh;
    }
}
