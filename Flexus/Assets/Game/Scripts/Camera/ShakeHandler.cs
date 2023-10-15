using System.Collections;
using System.Linq;
using UnityEngine;
using Zenject;

public class ShakeHandler : MonoBehaviour
{
    [SerializeField] private AnimationCurve shakeCurve;
    [SerializeField] private Vector3 shakePower;
    [Inject] private InputHandler inputHandler;
    private Coroutine shakeRoutine;
    private Vector3 startPosition;

    private void Start()
    {
        inputHandler.OnMouseClicked += StartShaking;
        startPosition = transform.localPosition;
    }

    public void StartShaking()
    {
        if (shakeRoutine != null) StopCoroutine(shakeRoutine);

        shakeRoutine = StartCoroutine(ShakeRoutine());
    }

    private IEnumerator ShakeRoutine()
    {
        for (float t = 0; t < shakeCurve.keys.Last().time; t += Time.deltaTime)
        {
            transform.localPosition = startPosition + shakePower * shakeCurve.Evaluate(t);
            yield return null;
        }

        transform.localPosition = startPosition;
    }
}