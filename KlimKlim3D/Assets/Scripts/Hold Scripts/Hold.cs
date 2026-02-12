using System;
using UnityEngine;

public class Hold : MonoBehaviour
{
    [SerializeField] HoldData holdData;
    [SerializeField] Collider interactionCollider;

    public HoldData GetHoldData()
    {
        return holdData;
    }

    public Vector3 GetCenterPosition()
    {
        return interactionCollider.bounds.center;
    }
}