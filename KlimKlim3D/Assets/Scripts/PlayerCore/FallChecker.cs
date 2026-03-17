using UnityEngine;
using System.Threading.Tasks;
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
        if (playerInfo.GetHandOnWallQty() == 0 && hasStarted && GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
        }
        else if (playerInfo.GetHandOnWallQty() > 0)
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.Euler(0, 0, 0),
                10f
                );
        }
    }
    private async void EnableFallCheck()
    {
        await Task.Delay(2000);
        Debug.Log("enable fall check");
        hasStarted = true;
    }
}
