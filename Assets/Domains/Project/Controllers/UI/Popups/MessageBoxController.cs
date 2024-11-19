using Project.Controllers.UI.UiBehaviours;
using Project.Data.Models;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class MessageBoxController : PopupController
{
    [SerializeField] private Text bodyText;
    [SerializeField] private Text titleText;
    [SerializeField] private AnimatedImage imageView;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button backButton;
    
    [Inject]
    private void Construct(MessageBoxRequest request, RectTransform parent)
    {
        gameObject.transform.SetParent(parent, false);
        bodyText.text = request.Body;
        titleText.text = request.Title.ToUpper();

        InitializeImage(request);
        InitializeButtons(request);
    }

    private void InitializeImage(MessageBoxRequest request)
    {
        if (request.Image == null)
        {
            imageView.gameObject.SetActive(false);
            return;
        }

        imageView.Image.sprite = request.Image;
        imageView.gameObject.SetActive(true);
        if (request.ShouldImageTween)
        {
            imageView.ApplyImageAnimation();
        }
    }

    private void InitializeButtons(MessageBoxRequest request)
    {
        confirmButton.onClick.AddListener(Close);

        if (request.OnClickConfirm != null)
        {
            confirmButton.onClick.AddListener(new UnityAction(request.OnClickConfirm));
        }

        if (request.OnClickBackArrow != null)
        {
            backButton.onClick.AddListener(new UnityAction(request.OnClickBackArrow));
        }
        else
        {
            backButton.gameObject.SetActive(false);
        }
    }

    public class Factory : PlaceholderFactory<MessageBoxRequest, RectTransform, MessageBoxController>
    {
    }
}