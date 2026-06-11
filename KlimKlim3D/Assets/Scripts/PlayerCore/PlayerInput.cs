using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;
// Script written by Koen Polman & Sietse Houkes for KlimKlim3D i.e. Master project, 2/2026 - 4/2026
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [HideInInspector] public UnityEvent leftHandGrab;
    [HideInInspector] public UnityEvent rightHandGrab;
    [HideInInspector] public UnityEvent leftHandRelease;
    [HideInInspector] public UnityEvent rightHandRelease;
    private Camera mainCamera;
    
    private static PlayerInput _instance;
    public static PlayerInput Instance => _instance;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject); // Deze regel
    }
    
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
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

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
