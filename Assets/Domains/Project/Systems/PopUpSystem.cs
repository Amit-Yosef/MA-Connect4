using System;
using Project.Data.Models;
using Project.Factories;
using UnityEngine;
using Zenject;


public class PopUpSystem : IDisposable
{
    [Inject] private PopupFactory _factory;
    [Inject] private MessageBoxController.Factory _messageBoxFactory;
    [Inject] private RectTransform _parent;

    private PopupController _currentPopup;

    public void Open(PopUpType popUpType, Vector2? position = null)
    {
        if (position == null) position = Vector2.zero;

        _currentPopup?.Close();
        _currentPopup = _factory.Create(popUpType, _parent);
        _currentPopup.Open((Vector2)position);
    }
    public void OpenMessageBox(MessageBoxRequest messageBoxRequest, Vector2? position = null)
    {
        if (position == null) position = Vector2.zero;

        _currentPopup?.Close();
        _currentPopup = _messageBoxFactory.Create(messageBoxRequest, _parent);
        _currentPopup.Open((Vector2)position);
    }


    public void Dispose()
    {
        _currentPopup?.Close();
    }
}

public enum PopUpType
{
    SelectSides,
    Settings,
}