using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandMovement : MonoBehaviour
{
    [SerializeField] HandType handType;
    [SerializeField] float maxRadius;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float moveSpeed = 10f;

    private Vector3 targetPosition;

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        targetPosition = transform.position;
    }

    private void Update()
    {
        UpdateTargetPosition();
        MoveTowardsTarget();
    }

    private void UpdateTargetPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, groundLayer))
        {
            targetPosition = hit.point;
        }
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );
    }
}
