using Data;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Controllers.UI.Project.UiBehaviours
{
    public class ButtonSoundController : MonoBehaviour , IPointerEnterHandler, IPointerClickHandler
    {
        [Inject] private SoundSystem _soundSystem;

        public void OnPointerEnter(PointerEventData eventData)
        {
            _soundSystem.PlaySound(SoundType.OnButtonHover);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _soundSystem.PlaySound(SoundType.OnButtonClick);
        }


    }
}