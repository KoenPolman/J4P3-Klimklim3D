using UnityEngine;

[CreateAssetMenu(fileName = "Hold", menuName = "Holds/Hold")]
public class HoldData : ScriptableObject
{
    [SerializeField, Range(0f, 2f)] float difficulty = 1f;

    [SerializeField] HoldType holdType = HoldType.both;

    [SerializeField, Range(0f, 5f)] float staminaDrainMultiplier = 1f;

    [SerializeField] int limbCapacity = 2;

    [SerializeField] bool inUse = false;

    [SerializeField] Color gizmoColour = Color.white;
    
    //public returns

    /// <summary>
    /// Gamemode difficulty multiplier.
    /// 1 is normal, 2 is hard.
    /// </summary>
    public float Difficulty => difficulty;

    /// <summary>
    /// Defines the type of hold: hand, foot, or both.
    /// </summary>
    public HoldType HoldType => holdType;

    /// <summary>
    /// Defines the multiplier for stamina drain rate.
    /// </summary>
    public float StaminaDrainMultiplier => staminaDrainMultiplier;

    /// <summary>
    /// Returns the limb capacity
    /// </summary>
    public int LimbCapacity => limbCapacity;

    /// <summary>
    /// returns gizmo color
    /// </summary>
    public Color GizmoColour => gizmoColour;
    
    /// <summary>
    /// Returns the inUse status
    /// </summary>
    //public bool InUse => inUse;
}