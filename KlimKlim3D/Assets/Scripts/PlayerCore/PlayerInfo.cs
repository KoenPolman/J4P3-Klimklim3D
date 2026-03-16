using Unity.VisualScripting;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private LimbInfo[] limbs;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        limbs = FindObjectsByType<LimbInfo>(FindObjectsSortMode.None);
    }
    public int GetHandOnWallQty()
    {
        int qty = 0;
        foreach (LimbInfo limb in limbs)
        {
            if (limb.GetState() == LimbState.holding && (limb.GetTypeStrict() == LimbType.RightHand || limb.GetTypeStrict() == LimbType.LeftHand))
            {
                qty++;
            }
        }
        return qty;
    }
}
