using UnityEngine;
using UnityEngine.UIElements;

public class Hold : MonoBehaviour
{
    [SerializeField] HoldData holdData;
    [SerializeField] Collider interactionCollider;
    private MeshFilter meshFilter;
    private Mesh[] meshes;

    public HoldData GetHoldData()
    {
        return holdData;
    }

    public Vector3 GetCenterPosition()
    {
        return interactionCollider.bounds.center;
    }

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshes = Resources.LoadAll<Mesh>("Holds");
        if (meshes != null && meshes.Length > 0)
        {
            meshFilter.mesh = meshes[Random.Range(0, meshes.Length)];
        }
    }
}