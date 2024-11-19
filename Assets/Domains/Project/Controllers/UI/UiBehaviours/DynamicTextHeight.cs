using UnityEngine;
using UnityEngine.UI;

namespace Project.Controllers.UI.UiBehaviours
{
    public class DynamicTextHeight : MonoBehaviour
    {
        [SerializeField] private Text textComponent;
        [SerializeField] private RectTransform textRectTransform;

        void Start()
        {
            AdjustHeight();
        }

        public void AdjustHeight()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(textRectTransform);

            float preferredHeight = textComponent.preferredHeight;

            textRectTransform.sizeDelta = new Vector2(textRectTransform.sizeDelta.x, preferredHeight);
        }
    }
}