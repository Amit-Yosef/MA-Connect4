using Project.Systems;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class VolumeController : MonoBehaviour
{
    [Inject] private SoundSystem _soundSystem;
    [SerializeField] private Slider volumeSlider;

    private void OnEnable()
    {
        volumeSlider.value = _soundSystem.Volume;

        if (volumeSlider != null)
            volumeSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnDisable()
    {
        if (volumeSlider != null)
            volumeSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        _soundSystem.Volume = value;
    }
}