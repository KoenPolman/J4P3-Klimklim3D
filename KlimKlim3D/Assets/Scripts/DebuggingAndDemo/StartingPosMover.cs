using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartingPosMover : MonoBehaviour
{
    [SerializeField] Transform[] startPositions;

    // Update is called once per frame
    void Update()
    {
        // Dit zal verschrikkelijk geprogrammeerd en niet schaalbaar zijn, ik heb geen spijt -Koen 6-2026

        if (Keyboard.current.digit1Key.isPressed)
        {
            if (startPositions[0] == null)
                return;

            transform.position = startPositions[0].position;
        }

        if (Keyboard.current.digit2Key.isPressed)
        {
            if (startPositions[1] == null)
                return;

            transform.position = startPositions[1].position;
        }

        if (Keyboard.current.digit3Key.isPressed)
        {
            if (startPositions[2] == null)
                return;

            transform.position = startPositions[2].position;
        }

        if (Keyboard.current.digit4Key.isPressed)
        {
            if (startPositions[3] == null)
                return;

            transform.position = startPositions[3].position;
        }

        if (Keyboard.current.digit5Key.isPressed)
        {
            if (startPositions[4] == null)
                return;

            transform.position = startPositions[4].position;
        }
    }
}