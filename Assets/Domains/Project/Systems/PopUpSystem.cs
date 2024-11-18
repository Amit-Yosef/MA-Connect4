using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Data;
using UnityEngine;
using Zenject;
using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Data;
using MoonActive.Connect4;
using UnityEngine;
using Zenject;

public class PopUpSystem
{
    [Inject] private PopupFactory _factory;
    [Inject(Optional = true )] private SelectSidesPopupFactory _selectSidesPopupFactory;
    [Inject] private MessageBoxController.Factory _messageBoxFactory;
    [Inject(Optional = true , Id = typeof(Canvas))] private RectTransform parent;


    private PopupController _currentPopup;

    public PopupController Get(PopUpType popUpType, Vector2? position = null)
    {
        if (position == null) position = new Vector2(0,0);

        _currentPopup?.Close();
        _currentPopup = _factory.Create(popUpType, parent);
        _currentPopup.Open((Vector2)position);
        return _currentPopup;
    }

    public PopupController GetSelectSidesPopup(Vector2? position = null)
    {
        if (position == null) position = new Vector2(0,0);

        _currentPopup?.Close();
        _currentPopup = _selectSidesPopupFactory.Create(parent);
        _currentPopup.Open((Vector2)position);
        return _currentPopup;
    }

    public PopupController GetMessageBox(MessageBoxData messageBoxData, Vector2? position = null)
    {
        if (position == null) position = new Vector2(0,0);
        
        _currentPopup?.Close();
        _currentPopup = _messageBoxFactory.Create(messageBoxData, parent);
        _currentPopup.Open((Vector2)position);
        return _currentPopup;
    }
}

public enum PopUpType
{
    SelectSides,
    Settings
}