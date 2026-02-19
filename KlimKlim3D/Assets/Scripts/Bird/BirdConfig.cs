using UnityEngine;

[CreateAssetMenu(menuName = "Bird/BirdConfig", fileName = "BirdConfig")]
public class BirdConfig : ScriptableObject
{
    [Header("Spawn")]
    [Range(0, 100)] public int spawnChance = 35;
    [Range(0, 100)] public int spawnSittingChance = 50;

    [Header("Approach")]
    public float offscreenPadding = 0.15f;
    public float approachSpeed = 6f;
    public float sittingSnapDistance = 0.2f;

    [Header("React to Hand")]
    [Range(0, 100)] public int circleHoldChance = 50;
    public float circleDuration = 1.25f;
    public float circleRadius = 1.2f;
    public float circleAngularSpeedDeg = 360f;

    [Header("Flee")]
    public float fleeSpeed = 10f;
    public float fleeTime = 1.2f;
}
