using System;
using UnityEngine;
using Zenject;

public class CannonRotateHandler : MonoBehaviour
{
    [SerializeField] private Transform cannonRoot;
    [SerializeField] private Transform shootPoint;
    [Space]
    [SerializeField] private float rotationSpeed = 10f;
    [Inject] private InputHandler inputHandler;

    private void Update()
    {
        RotateCannon();
    }

    private void RotateCannon()
    {
        float moveX = inputHandler.HorizontalInput;
        float moveY = inputHandler.VerticalInput;

        if (moveX != 0 || moveY != 0)
        {
            transform.Rotate(Vector3.up, moveX * Time.deltaTime * rotationSpeed);
            cannonRoot.Rotate(Vector3.left, moveY * Time.deltaTime * rotationSpeed);
        }
    }
}