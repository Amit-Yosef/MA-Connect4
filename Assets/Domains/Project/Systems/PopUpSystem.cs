using System;
using Project.Data.Models;
using Project.Factories;
using UnityEngine;
using Zenject;


public class PopUpSystem : IDisposable
{
    [Inject] private PopupFactory _factory;
    [Inject] private MessageBoxController.Factory _messageBoxFactory;
    [Inject(Optional = true)] private RectTransform parent;

    private PopupController _currentPopup;

    public void Open(PopUpType popUpType, Vector2? position = null)
    {
        if (position == null) position = Vector2.zero;

        _currentPopup?.Close();
        _currentPopup = _factory.Create(popUpType, parent);
        _currentPopup.Open((Vector2)position);
    }
    public void OpenMessageBox(MessageBoxData messageBoxData, Vector2? position = null)
    {
        if (position == null) position = Vector2.zero;

        _currentPopup?.Close();
        _currentPopup = _messageBoxFactory.Create(messageBoxData, parent);
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