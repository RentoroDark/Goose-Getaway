using UnityEngine;
using UnityEngine.UI;

public class AudioSettingController : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Slider slider;

    void Start()
    {
        slider.value = playerData.musicVolume;
        audioSource.volume = slider.value;
    }
    public void ChangeMusicVoluem()
    {
        playerData.musicVolume = slider.value;
        audioSource.volume = slider.value;
    }
}
