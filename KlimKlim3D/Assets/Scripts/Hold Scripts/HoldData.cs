using UnityEngine;

using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Hold", menuName = "Holds/Hold")]
public class HoldData : ScriptableObject
{
    /// <summary>
    /// gamemode difficulty multiplier
    /// 1 is normal 2 is hard
    /// </summary>
    [Range(0,2)]
    public float difficulty = 1f;

    /// <summary>
    /// Defines the type of hold hand, foot, or both
    /// </summary>
    public HoldType holdType = HoldType.both;

    /// <summary>
    /// Defines the multiplier for stamina drain rate
    /// </summary>
    [Range(0,5)]
    public float staminaDrainMultiplier = 1f;
    public int limbCapacity = 2;
    
    public bool inUse = false;
}