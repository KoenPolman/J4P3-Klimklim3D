using System;
using UnityEngine;

public class ClimbingPickupEffects : MonoBehaviour
{
    // Configurable effect parameters
    private float chalkStaminaMultiplier = 0.5f;
    private float coffeeDurationSeconds = 15f;
    public int holdsRemaining = 0;
    private float coffeeEffectEndTime = -1f;

    public float ChalkStaminaMultiplier => chalkStaminaMultiplier;
    public bool IsCoffeeEffectActive => Time.time < coffeeEffectEndTime;

    public void ActivateChalkEffect(int holds)
    {
        holdsRemaining = Mathf.Max(0, holds);
    }

    public void ActivateCoffeeEffect()
    {
        coffeeEffectEndTime = Time.time + Mathf.Max(0f, coffeeDurationSeconds);
    }

    public void UseHandHold()
    {
        if (holdsRemaining > 0)
        {
            holdsRemaining--;
        }
    }

    public void consumeHandHold()
    {
        UseHandHold();
    }   
}