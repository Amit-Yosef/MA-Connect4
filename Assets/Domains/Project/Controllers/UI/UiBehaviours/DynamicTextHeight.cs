using UnityEngine;
using UnityEngine.UI;

namespace Project.Controllers.UI.UiBehaviours
{
    public class DynamicTextHeight : MonoBehaviour
    {
        [SerializeField] private Text textComponent;
        [SerializeField] private RectTransform textRectTransform;

        private void Start()
        {
            AdjustHeight();
        }

        private void AdjustHeight()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(textRectTransform);

            float preferredHeight = textComponent.preferredHeight;

            textRectTransform.sizeDelta = new Vector2(textRectTransform.sizeDelta.x, preferredHeight);
        }
    }
}