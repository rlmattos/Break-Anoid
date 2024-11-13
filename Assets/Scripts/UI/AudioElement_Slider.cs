using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudioElement_Slider : AudioElement
{
    Slider slider;
    private void OnEnable()
    {
        slider = GetComponentInParent<Slider>();
    }

    private void Start()
    {
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }

    void OnSliderValueChanged(float newValue)
    {
        PlayClick();
    }

    public override void OnDrag(PointerEventData eventData) { }
}