using System;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Slider slider;

    public float HorizontalInput
    {
        get
        {
            OnMoveInputChange();
            return Input.GetAxis("Horizontal");
        }
    }
    public float VerticalInput
    {
        get
        {
            OnMoveInputChange();
            return Input.GetAxis("Vertical");
        }
    }

    public float PowerValue => slider.value;


    public event Action OnSliderValueChanged;
    public event Action OnMoveInputChanged;
    public event Action OnMouseClicked;



    private void Start()
    {
        slider.onValueChanged.AddListener(OnSliderValueChange);
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            OnMouseButtonClicked();
        }

        ChangeSliderValueByMouseWheel();
    }

    private void ChangeSliderValueByMouseWheel()
    {
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");

        slider.value += scrollDelta * 10;
        slider.value = Mathf.Clamp(slider.value, slider.minValue, slider.maxValue);
    }

    private void OnSliderValueChange(float value) => OnSliderValueChanged?.Invoke();
    private void OnMouseButtonClicked() => OnMouseClicked?.Invoke();
    private void OnMoveInputChange() => OnMoveInputChanged?.Invoke();
}
