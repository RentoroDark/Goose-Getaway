using TMPro;
using UnityEngine;

public class UIDataController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI health;
    [SerializeField] TextMeshProUGUI money;
    [SerializeField] PlayerMoney playerMoney;
    [SerializeField] PlayerHealth playerHealth;
    void Start()
    {
        playerHealth.healthChanged += UpdateHealth;
        playerMoney.moneyCollected += UpdateMoney;
        
    }

    private void UpdateHealth()
    {
        Debug.Log(health.text);
        Debug.Log(playerHealth.health);
        health.text = $"{playerHealth.health}";
    }
    private void UpdateMoney()
    {
        Debug.Log(money.text);
        Debug.Log(playerMoney.collectedMoney);
        money.text = $"{playerMoney.collectedMoney}";
    }
}
