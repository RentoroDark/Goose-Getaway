using System.Collections;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] AdBannerController adBannerController;
    [SerializeField] GameObject hud;
    public void Pause()
    {
        Time.timeScale = 0;
        adBannerController.DisplayAd();
        pausePanel.SetActive(true);
        
        hud.SetActive(false);
    }
    public void Continue()
    {
        adBannerController.DestroyAd();
        hud.SetActive(true);
        pausePanel.SetActive(false);
        StartCoroutine("TimeLerp");
    }
    IEnumerator TimeLerp()
    {
        do
        {
            Time.timeScale += Time.deltaTime + 0.01f;
            yield return null;
        }while (Time.timeScale < 1);
        Time.timeScale = 1;
    }
    
}
