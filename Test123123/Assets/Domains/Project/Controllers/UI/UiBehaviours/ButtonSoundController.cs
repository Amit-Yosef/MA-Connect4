using Project.Data.Models;
using Project.Systems;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Project.Controllers.UI.UiBehaviours
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