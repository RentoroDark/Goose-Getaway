using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUIScript : MonoBehaviour
{
    [SerializeField] AdBannerController bannerAd;
    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);

    }
    public void Close(GameObject panel)
    {
        panel.SetActive(false);
    }
    public void CloseWholeMenu()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    public void Respawn()
    {
        
    }
    public void Restart()
    {
        bannerAd.DestroyAd();
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void ReturnToMainMenu()
    {
        bannerAd.DestroyAd();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        
    }

    

}
