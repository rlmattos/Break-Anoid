using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudioElement_Button : AudioElement
{
    Button button;
    private void OnEnable()
    {
        button = GetComponentInParent<Button>();
    }

    private void Start()
    {
        if (!button)
            button = button = GetComponentInParent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        if (!button)
            button = button = GetComponentInParent<Button>();
        button.onClick.RemoveListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        PlayClick();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {

    }
}