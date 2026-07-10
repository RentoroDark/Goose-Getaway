using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainCanvasScript : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] TextMeshProUGUI money;
    [SerializeField] EnergyController energyController;
    

    void Start()
    {
        playerData.saveAction += RefreshUI;
        money.text = $"{playerData.savedMoney}";
        RefreshUI();
    }

    public void StartGame()
    {
        energyController.SpendEnergy();
        if (playerData.HighScore == 0)
            SceneManager.LoadScene(2);
        else
            SceneManager.LoadScene(1);
    }

    public void OpenCanvas(GameObject canvas)
    {
        canvas.SetActive(true);
    }
    public void CloseCanvas(GameObject canvas)
    {
        canvas.SetActive(false);
    }
    void RefreshUI()
    {
        money.text = $"{playerData.savedMoney}";
    }
    void OnDestroy()
    {
        playerData.saveAction -= RefreshUI;
    }
}
