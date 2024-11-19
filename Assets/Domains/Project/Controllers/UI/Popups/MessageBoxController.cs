using Project.Data.Models;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class MessageBoxController : PopupController
{
    [SerializeField] private Text bodyText;
    [SerializeField] private Text titleText;
    [SerializeField] private Image imageView;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button backButton;

    [Inject]
    private void Construct(MessageBoxData data, RectTransform parent)
    {
        gameObject.transform.SetParent(parent, false);
        bodyText.text = data.Body;
        titleText.text = data.Title.ToUpper();

        InitializeImage(data);
        InitializeButtons(data);
    }

    private void InitializeImage(MessageBoxData data)
    {
        if (data.Image == null)
        {
            imageView.gameObject.SetActive(false);
            return;
        }

        imageView.sprite = data.Image;
        imageView.gameObject.SetActive(true);
        if (data.ShouldImageTween)
        {
            ApplyImageAnimation();
        }
    }

    private void ApplyImageAnimation()
    {
        LeanTween.moveLocalX(imageView.gameObject, 10f, 2f)
            .setEaseInOutSine()
            .setLoopPingPong().setFrom(-10f);
            

        LeanTween.moveLocalY(imageView.gameObject, 10f, 1f)
            .setEaseInOutSine()
            .setLoopPingPong();

        LeanTween.rotateZ(imageView.gameObject, 10f, 0.5f)
            .setEaseInOutSine()
            .setLoopPingPong()
            .setFrom(-10f);
    }


    private void InitializeButtons(MessageBoxData data)
    {
        confirmButton.onClick.AddListener(Close);

        if (data.OnClickConfirm != null)
        {
            confirmButton.onClick.AddListener(new UnityAction(data.OnClickConfirm));
        }

        if (data.OnClickBackArrow != null)
        {
            backButton.onClick.AddListener(new UnityAction(data.OnClickBackArrow));
        }
        else
        {
            backButton.gameObject.SetActive(false);
        }
    }

    public class Factory : PlaceholderFactory<MessageBoxData, RectTransform, MessageBoxController>
    {
    }
}