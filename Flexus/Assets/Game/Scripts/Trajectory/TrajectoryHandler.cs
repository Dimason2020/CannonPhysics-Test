using UnityEngine;
using Zenject;

public class TrajectoryHandler : MonoBehaviour
{
    [SerializeField] private CannonRotateHandler _cannon;
    [SerializeField] private LineRenderer _line;
    [Inject] private InputHandler _inputHandler;

    private Vector3 verticalDirection = Vector3.up;
    private Vector3 horizontalDirection;

    private float horizontalVelocity;
    private float verticalVelocity;

    private const float timeStep = 0.08f;
    private const float gravity = 9.81f;

    private void Start()
    {
        UpdateTrajectory();

        _inputHandler.OnMoveInputChanged += UpdateTrajectory;
        _inputHandler.OnSliderValueChanged += UpdateTrajectory;
    }

    private void UpdateTrajectory()
    {
        CalculateTrajectory();
        RenderTrajectory();
    }


    private void CalculateTrajectory()
    {
        Vector3 forwardDirection = transform.forward;

        horizontalDirection = forwardDirection;
        horizontalDirection.y = 0;
        horizontalDirection.Normalize();

        Vector3 velocity = forwardDirection * _inputHandler.PowerValue;
        horizontalVelocity = new Vector2(velocity.x, velocity.z).magnitude;
        verticalVelocity = velocity.y;
    }

    private void RenderTrajectory()
    {
        Vector3 trajectoryPoint = transform.position;
        
        for (var i = 0; i < _line.positionCount; i++)
        {
            _line.SetPosition(i, trajectoryPoint);
            
            trajectoryPoint += horizontalDirection * (horizontalVelocity * timeStep);
            trajectoryPoint += verticalDirection   * (verticalVelocity   * timeStep - 4.9f * timeStep * timeStep);
            
            verticalVelocity -= gravity * timeStep;
        }
    }
}