using UnityEngine;

public class FallChecker : MonoBehaviour
{
    private PlayerInfo playerInfo;
    private bool hasStarted = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerInput input = GetComponent<PlayerInput>();
        input.leftHandGrab.AddListener(EnableFallCheck);
        input.rightHandGrab.AddListener(EnableFallCheck);

        playerInfo = GetComponent<PlayerInfo>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerInfo.GetLimbOnWallQty() == 0 && hasStarted && GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
        }
        else if (playerInfo.GetLimbOnWallQty() > 0)
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
        }
    }
    private void EnableFallCheck()
    {
        Debug.Log("enable fall check");
        hasStarted = true;
    }
}
