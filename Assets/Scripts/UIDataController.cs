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
        health.text = $"{playerHealth.health}";
    }
    private void UpdateMoney()
    {
        money.text = $"{playerMoney.collectedMoney}";
    }
}
