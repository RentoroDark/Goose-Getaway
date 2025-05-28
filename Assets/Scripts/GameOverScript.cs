using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.InputSystem;

public class GameOverScript :MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] PlayerMoney playerMoney;
    [SerializeField] PlayerScore playerScore;
    [SerializeField] GameObject gameOverStatsPanel;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] GameObject escapeStatsPanel;
    [SerializeField] TextMeshProUGUI escapeText;
    [SerializeField] RewardedAdScript rewardedAdScript;
    [SerializeField] GameObject playerBody;
    [SerializeField] List<Material> materials;
    [SerializeField] GameObject panel;
    private Collider[] objectsToDestroy;
    
    private void Awake() 
    {
        rewardedAdScript.rewardShown += Respawn;    
    }
    public void Death()
    {
        playerMovement.enabled = false;
        gameOverStatsPanel.gameObject.SetActive(true);
        if (playerScore.displaydScore > playerData.HighScore)
        {
            gameOverText.text = $"New High Score:\n{playerScore.displaydScore}\nPrevious Record:\n{playerData.HighScore}\n";

        }
        else
        {
            gameOverText.text = $"Score:\n{playerScore.displaydScore}\nHigh Score:\n{playerData.HighScore}\n";
        }
        gameOverText.text += $"Money Collected:\n{playerMoney.collectedMoney}\nSaved Money:\n{playerMoney.collectedMoney / 2}";
        playerMoney.SaveMoney(playerMoney.collectedMoney / 2);
        Time.timeScale = 0;
        playerData.SavePlayerData();
    }
    
    public void Escape()
    {
        escapeStatsPanel.gameObject.SetActive(true);
        if (playerScore.displaydScore > playerData.HighScore)
        {
            escapeText.text = $"New High Score:\n{playerScore.displaydScore}\nPrevious Record:\n{playerData.HighScore}\n";

        }
        else
        {
            escapeText.text = $"Score:\n{playerScore.displaydScore}\nHigh Score:\n{playerData.HighScore}\n";
        }
        escapeText.text += $"MoneyCollected:\n{playerMoney.collectedMoney}\nSaved Money:\n{playerMoney.collectedMoney}";
        Time.timeScale = 0;
        playerMoney.SaveMoney(playerMoney.collectedMoney);
        playerData.RestoreEnergy(2);
        playerData.SavePlayerData();
    }
    public void Respawn()
    {
        Physics.OverlapSphereNonAlloc(gameObject.transform.position, 20, objectsToDestroy,LayerMask.GetMask("Obstacle"));
        foreach (var collider in objectsToDestroy)
        {
            Destroy(collider.gameObject);
        }
        panel.SetActive(false);
        playerHealth.RestoreHealth(3);
        Time.timeScale = 1;
        transform.localPosition = new Vector3(0, 0.5f, 0.4f);
        playerMovement.enabled = true;

    }
    
}
