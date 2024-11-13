using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudioElement_Toggle : AudioElement
{
    Toggle toggle;
    private void OnEnable()
    {
        toggle = GetComponentInParent<Toggle>();
    }

    private void Start()
    {
        if (!toggle)
            toggle = toggle = GetComponentInParent<Toggle>();
        toggle.onValueChanged.AddListener(OnToggle);
    }

    private void OnDisable()
    {
        if (!toggle)
            toggle = toggle = GetComponentInParent<Toggle>();
        toggle.onValueChanged.RemoveListener(OnToggle);
    }

    void OnToggle(bool ligou)
    {
        if (ligou)
            PlayClick();
        else
            PlayBlock();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {

    }
}