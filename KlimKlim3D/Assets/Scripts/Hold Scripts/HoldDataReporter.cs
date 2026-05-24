using UnityEngine;

public class HoldDataReporter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Hold hold;
    void Start()
    {
        hold = gameObject.GetComponent<Hold>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Current limb count on this hold: " + hold.GetLimbCount());
    }
}
