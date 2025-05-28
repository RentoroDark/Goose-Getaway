using System;
using TMPro;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    [SerializeField] GameOverScript gameOverScript;
    [SerializeField] PlayerData playerData;
    public int goldMultiplier = 1;
    public int collectedMoney = 0;
    public event Action moneyCollected;
    void Start()
    {
        moneyCollected?.Invoke();
    }
   
    public void Add()
    {
        collectedMoney += 1 * goldMultiplier;
        moneyCollected?.Invoke();
    }
    public void SaveMoney(int amount)
    {
        playerData.Add(amount);
    }
}
