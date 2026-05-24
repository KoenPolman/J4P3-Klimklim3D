using UnityEngine;
using System.Threading.Tasks;
// Script written by Koen Polman for KlimKlim3D i.e. Master project, 2/2026 - 4/2026
public class FallChecker : MonoBehaviour
{
    private PlayerInfo playerInfo;
    private bool hasStarted = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // +Remove this in exchange for a mechanic where the fall check is enabled when two hands are on the wall
        // Find the player input script and subscribe to input events for enableing the fall check
        PlayerInput input = FindFirstObjectByType<PlayerInput>();
        input.leftHandGrab.AddListener(TryEnableFallCheck);
        input.rightHandGrab.AddListener(TryEnableFallCheck);

        playerInfo = GetComponent<PlayerInfo>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Check if there are 0 hands on the wall, the player has started and has a rigid body already if so the player character has a rigidbody added which in turn makes it fall
        if (playerInfo.GetHandOnWallQty() == 0 && hasStarted && GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
        }
        else if (playerInfo.GetHandOnWallQty() > 0)
        {
            // If the player has more than 0 hands on the wall the rigidbody is destroyed and the rotation of the player is corrected
            Destroy(gameObject.GetComponent<Rigidbody>());
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.Euler(0, 0, 0),
                10f
                );
        }
    }
    /// <summary>
    /// Enables the fall check with a delay
    /// </summary>
    private async void TryEnableFallCheck()
    {
        if (hasStarted)
            return;

        // Fall check is enabled when the player has 2 hands on the wall
        if (playerInfo.GetHandOnWallQty() >= 2)
        {
            hasStarted = true;
            CoreMovement coreMovement = GetComponent<CoreMovement>();
            coreMovement.enable();
        }
            

        Debug.Log("fall check has been enabled");
        
    }
}
