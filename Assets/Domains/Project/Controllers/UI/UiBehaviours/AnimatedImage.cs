using UnityEngine;
using UnityEngine.UI;

namespace Project.Controllers.UI.UiBehaviours
{
    public class AnimatedImage : MonoBehaviour
    {
        public Image Image => image;
        
        [SerializeField] private Image image;
        
        [Header("Image Animation Settings")]
        [SerializeField] private float movementRange = 20;
        [SerializeField] private float rotationRange = 20;
        [SerializeField] private float intervalsPerSecond = 2;
        
        public void ApplyImageAnimation()
        {
            LeanTween.moveLocalX(gameObject, movementRange/2, intervalsPerSecond)
                .setEaseInOutSine()
                .setLoopPingPong().setFrom(-movementRange/2);
            
            LeanTween.moveLocalY(gameObject, movementRange/2, intervalsPerSecond/2)
                .setEaseInOutSine()
                .setLoopPingPong();

            LeanTween.rotateZ(gameObject, rotationRange/2, intervalsPerSecond/4)
                .setEaseInOutSine()
                .setLoopPingPong()
                .setFrom(-rotationRange/2);
        }
        
        public void StopAnimation()
        {
         LeanTween.cancel(gameObject);
        }
    }
}