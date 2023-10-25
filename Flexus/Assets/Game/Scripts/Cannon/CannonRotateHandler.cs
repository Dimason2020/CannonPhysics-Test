using System;
using UniRx;
using UnityEngine;
using Zenject;

public class CannonRotateHandler : MonoBehaviour
{
    [SerializeField] private Transform cannonRoot;
    [SerializeField] private Transform shootPoint;
    [Space]
    [SerializeField] private float rotationSpeed = 10f;
    [Inject] private InputHandler inputHandler;

    private void Start()
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            RotateCannon();
        });
    }

    private void RotateCannon()
    {
        float moveX = inputHandler.KeyboardInput.Value.x;
        float moveY = inputHandler.KeyboardInput.Value.y;

        if (moveX != 0 || moveY != 0)
        {
            transform.Rotate(Vector3.up, moveX * Time.deltaTime * rotationSpeed);
            cannonRoot.Rotate(Vector3.left, moveY * Time.deltaTime * rotationSpeed);
        }
    }
}