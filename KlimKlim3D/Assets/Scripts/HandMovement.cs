using UnityEngine;
using UnityEngine.InputSystem;

public class HandMovement : MonoBehaviour
{
    [SerializeField] HandType handType;
    [SerializeField] float maxRadius;

    Transform shoulderPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shoulderPosition = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = new Vector3();
        mousePos = Input.mousePosition;
        Debug.Log(mousePos);
        // if the mouse button bound to the hand in not down place the hand
        if (!Input.GetMouseButtonDown((int)handType))
        {
            placeHand();
        }
    }
    /// <summary>
    /// place the hand at the mouse position
    /// </summary>
    void placeHand()
    {

    }
}
