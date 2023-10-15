using System;
using UnityEngine;

public class PhysicsBody : MonoBehaviour
{
    private float simulationSpeed = 2f;
    private float reflectedMinimumZ = -1;
    private float movementSpeed;
    private Vector3 gravity => new Vector3(0f, -9.81f, 0f) * simulationSpeed;
    public Vector3 MoveVelocity { get; set; }

    public event Action<RaycastHit> OnProjectileCollided;


    private void FixedUpdate()
    {
        CheckColliders();
        Move();
        AddGravity(gravity * Time.fixedDeltaTime);
    }

    private void CheckColliders()
    {
        Ray ray = new Ray(transform.position, MoveVelocity);

        if (Physics.Raycast(ray, out var hitInfo, movementSpeed) && hitInfo.distance < movementSpeed)
        {
            transform.position = hitInfo.point;

            float bounciness = Mathf.Max(hitInfo.collider.material.bounciness, 0.1f);
            Vector3 reflectedVector = Vector3.Reflect(MoveVelocity, hitInfo.normal) * bounciness;
            reflectedVector = ClampReflectedVector(reflectedVector);
            MoveVelocity = reflectedVector;

            movementSpeed -= hitInfo.distance;
            OnProjectileCollided?.Invoke(hitInfo);
        }
    }

    private void Move()
    {
        movementSpeed = (MoveVelocity * Time.fixedDeltaTime).magnitude * simulationSpeed;
        transform.position += MoveVelocity.normalized * movementSpeed;
    }

    private Vector3 ClampReflectedVector(Vector3 _reflectedVector)
    {
        if(_reflectedVector.z <= 0)
        {
            _reflectedVector.z = Mathf.Clamp(_reflectedVector.z, reflectedMinimumZ, 0f);
            return _reflectedVector;
        }
        else

            return _reflectedVector;
    }

    public void AddGravity(Vector3 force)
    {
        MoveVelocity += force;
    }
}