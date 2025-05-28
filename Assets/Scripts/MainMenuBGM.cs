using UnityEngine;

public class MainMenuBGM : MonoBehaviour
{
    [SerializeField] AudioClip intro;
    [SerializeField] AudioClip main;
    [SerializeField] AudioSource audioSource;
    [SerializeField] PlayerData playerData;
    void Start()
    {
        playerData.OnVolumeChange += ApplyChange;
        ApplyChange();
        
        audioSource.clip = intro;
        audioSource.Play();
        audioSource.clip = main;
        
        
        
    }
    void ApplyChange()
    {
        audioSource.volume = playerData.musicVolume;
    }
    
}
