using UnityEngine;
// Script written by Sietse Houkes & Koen Polman for KlimKlim3D i.e. Master project, 2/2026 - 4/2026
public class Hold : MonoBehaviour
{
    [SerializeField] HoldData holdData;
    [SerializeField] Collider interactionCollider;
    private MeshFilter meshFilter;
    private Mesh[] meshes;
    private int limbCount = 0;
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
            Debug.Log("Hold is at max capacity, limbcount: " + limbCount + " capacity: " + holdData.LimbCapacity);
            return false;
        }

        if (holdData.HoldType == HoldType.both)
        {
            return true;
        }
        else if (holdData.HoldType == HoldType.hand && (type == LimbType.LeftHand || type == LimbType.RightHand))
        {
            return true;
        }
        else if (holdData.HoldType == HoldType.foot && (type == LimbType.LeftFoot || type == LimbType.RightFoot))
        {
            return true;
        }
        else
        {
            //Debug.Log("Hold + limb type mismatch");
            return false;
        }
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
        switch (GetHoldData().HoldType)
        {
            case HoldType.both:
                Gizmos.color = Color.yellow;
                break;
            case HoldType.hand:
                Gizmos.color = Color.green;
                break;
            case HoldType.foot:
                Gizmos.color = Color.red;
                break;
            default:
                Gizmos.color = Color.white;
                break;
        }
        Gizmos.DrawCube(transform.position, new Vector3(.4f, .4f, .4f));
    }

    private bool IsAtLimbCapacity()
    {
        return limbCount > holdData.LimbCapacity;
    }
    public void CheckIn()
    {
        limbCount++;
    }
    public void CheckOut()
    {
        limbCount--;

        if (limbCount < 0)
        {
            limbCount = 0;
        }
    }
}