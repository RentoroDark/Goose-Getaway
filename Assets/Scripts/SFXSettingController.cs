using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SFXSettingController : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioMixerGroup audioMixerGroup;
    [SerializeField] Slider slider;
    void Start()
    {
        slider.value = playerData.SFXvolume;
        audioMixer.SetFloat("MasterVolume", slider.value);
        
    }
    public void ChangeSFXVoluem()
    {

        playerData.SFXvolume = slider.value;
        audioMixer.SetFloat("MasterVolume", slider.value);
        
    }
}
