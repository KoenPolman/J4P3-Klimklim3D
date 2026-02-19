using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [HideInInspector] public UnityEvent leftHandGrab;
    [HideInInspector] public UnityEvent rightHandGrab;
    [HideInInspector] public UnityEvent leftHandRelease;
    [HideInInspector] public UnityEvent rightHandRelease;
    private Camera mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            leftHandGrab.Invoke();
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            leftHandRelease.Invoke();
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            rightHandGrab.Invoke();
        }

        if (Mouse.current.rightButton.wasReleasedThisFrame)
        {
            rightHandRelease.Invoke();
        }

    }
    public Vector3 GetMousePosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, groundLayer))
        {
            return hit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
