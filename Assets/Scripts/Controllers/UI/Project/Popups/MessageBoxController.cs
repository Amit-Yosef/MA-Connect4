using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

public class MessageBoxController : PopupController
{
    [SerializeField] private Text bodyText;
    [SerializeField] private Text titleText;
    [SerializeField] private Button confirmButton;

    [Inject]
    private void Construct(MessageBoxData data, RectTransform parent)
    {
        gameObject.transform.SetParent(parent);
        bodyText.text = data.Body;
        titleText.text = data.Title.ToUpper();
        confirmButton.onClick.AddListener(Close);

        if (data.OnClick !=null)
        {
            confirmButton.onClick.AddListener(new UnityAction(data.OnClick));
        }
    }

   
    public class Factory : PlaceholderFactory<MessageBoxData, RectTransform, MessageBoxController >
    {
                
    }
}
