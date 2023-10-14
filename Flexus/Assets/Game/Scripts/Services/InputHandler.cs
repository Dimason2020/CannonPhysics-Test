using System;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Slider slider;

    public Vector3 GetMousePosition => Input.mousePosition;
    public float PowerValue => slider.value;

    public event Action<float> OnSliderValueChanged;
    public event Action OnMouseClicked;

    private void Start()
    {
        slider.onValueChanged.AddListener(OnSliderValueChange);
    }

    private void OnSliderValueChange(float value)
    {
        OnSliderValueChanged?.Invoke(value);
    }

    private void OnMouseButtonClicked()
    {
        OnMouseClicked?.Invoke();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            OnMouseButtonClicked();
        }
        Cursor.visible = false;
    }
}
