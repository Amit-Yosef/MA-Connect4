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
    [SerializeField] private Button backButton;

    [Inject]
    private void Construct(MessageBoxData data, RectTransform parent)
    {
        gameObject.transform.SetParent(parent);
        bodyText.text = data.Body;
        titleText.text = data.Title.ToUpper();

        IniitailizeButtons(data);
    }

    private void IniitailizeButtons(MessageBoxData data)
    {
        confirmButton.onClick.AddListener(Close);

        if (data.OnClickConfirm !=null)
        {
            confirmButton.onClick.AddListener(new UnityAction(data.OnClickConfirm));
        }

        if (data.OnClickBackArrow != null)
        {
            backButton.onClick.AddListener(new UnityAction(data.OnClickBackArrow));
            return;
        }
        backButton.gameObject.SetActive(false);
    }


    public class Factory : PlaceholderFactory<MessageBoxData, RectTransform, MessageBoxController >
    {
                
    }
}
