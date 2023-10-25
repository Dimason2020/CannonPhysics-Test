using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Slider slider;

    public ReactiveProperty<Vector2> KeyboardInput = new ReactiveProperty<Vector2>();

    private Vector2 input => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

    private readonly string MouseScroll = "Mouse ScrollWheel";
    public float PowerValue => slider.value;


    public event Action OnSliderValueChanged;
    public event Action OnMoveInputChanged;
    public event Action OnMouseClicked;

    private CompositeDisposable disposable = new CompositeDisposable();


    private void Start()
    {
        Observable.EveryUpdate()
        .Subscribe(_ => UpdateInput())
        .AddTo(disposable);

        IObservable<float> sliderValueStream = this.UpdateAsObservable()
            .Where(_ => Input.GetAxis(MouseScroll) != 0)
            .Select(_ => Input.GetAxis(MouseScroll));
        sliderValueStream
            .Subscribe(input => ChangeSliderValueByMouseWheel(input))
            .AddTo(disposable);

        IObservable<Unit> leftButtonClickStream = this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0));
        leftButtonClickStream
            .Subscribe(_ => OnMouseButtonClicked())
            .AddTo(disposable);

    }

    private void UpdateInput()
    {
        KeyboardInput.Value = input;
        OnMoveInputChanged?.Invoke();
    }
    private void ChangeSliderValueByMouseWheel(float input)
    {
        float scrollDelta = input;

        slider.value += scrollDelta * 10;
        slider.value = Mathf.Clamp(slider.value, slider.minValue, slider.maxValue);

        OnSliderValueChanged?.Invoke();
    }
    private void OnMouseButtonClicked() => OnMouseClicked?.Invoke();
}
