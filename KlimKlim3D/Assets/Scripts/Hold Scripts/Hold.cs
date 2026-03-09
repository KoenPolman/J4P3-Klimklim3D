using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class Hold : MonoBehaviour
{
    [SerializeField] HoldData holdData;
    [SerializeField] Collider interactionCollider;
    private MeshFilter meshFilter;
    private Mesh[] meshes;

    /// <summary>
    /// Returns hold data
    /// </summary>
    /// <returns></returns>
    public HoldData GetHoldData()
    {
        return holdData;
    }

    public Vector3 GetCenterPosition()
    {
        return interactionCollider.bounds.center;
    }

    public bool GetHoldAvailability(LimbType type)
    {
        if (IsAtLimbCapacity())
        {
            return false;
        }
        if (holdData.holdType == HoldType.both)
        {
            return true;
        }
        else if (holdData.holdType == HoldType.hand && (type == LimbType.LeftHand || type == LimbType.RightHand))
        {
            return true;
        }
        else if (holdData.holdType == HoldType.foot && (type == LimbType.LeftFoot || type == LimbType.RightFoot))
        {
            return true;
        }
        return false;
    }

    private void Awake()
    {
        //get mesh filter
        meshFilter = GetComponent<MeshFilter>();
        //load meshes
        meshes = Resources.LoadAll<Mesh>("Holds");
        //pick random mesh
        if (meshes != null && meshes.Length > 0)
        {
            meshFilter.mesh = meshes[Random.Range(0, meshes.Length)];
        }
    }

    /// <summary>
    /// Draw gizmo for hold
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(transform.position, new Vector3(.4f, .4f, .4f));
    }
    private bool IsAtLimbCapacity()
    {
        return false;
    }
}