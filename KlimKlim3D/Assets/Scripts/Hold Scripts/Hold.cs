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
        switch (GetHoldData().holdType)
        {
            case HoldType.both:
                Gizmos.color = Color.yellow;
                break;
            case HoldType.hand:
                Gizmos.color = Color.red;
                break;
            case HoldType.foot:
                Gizmos.color = Color.green;
                break;
            default:
                Gizmos.color = Color.white;
                break;
        }

        Gizmos.DrawCube(transform.position, new Vector3(.4f, .4f, .4f));
    }

    private bool IsAtLimbCapacity()
    {
        return false;
    }
}