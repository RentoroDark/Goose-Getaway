using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class EnergyController : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] TextMeshProUGUI energyDisplay;
    [SerializeField] TextMeshProUGUI runButtonText;
    [SerializeField] TextMeshProUGUI runButtonEnergy;
    [SerializeField] Image energyIcon;
    [SerializeField] PlayerData playerData;
    private int timeSinceLastTick;
    void Start()
    {
        timeSinceLastTick = (int)(DateTimeOffset.UtcNow.ToUnixTimeSeconds() -  playerData.energyTickTime);
        playerData.energy += timeSinceLastTick / 300;
        if (playerData.energy > 20)
        {
            playerData.energy = 20;
        }
        
        
        InvokeRepeating("RestoreEnergy", Math.Max(300 - timeSinceLastTick, 0), 300);
        playerData.saveAction += UpdateEnergy;
        UpdateEnergy();
    }

    public void UpdateEnergy()
    {
        energyDisplay.text = $"{playerData.energy}/{playerData.maxEnergy}";
        if (playerData.energy < 2)
        {
            energyIcon.color = Color.red;
            runButtonText.color = Color.red;
            runButtonEnergy.color = Color.red;
            startButton.interactable = false;
        }
        else
        {
            energyIcon.color = new Color(1, 0.5f, 0, 1);
            runButtonText.color = new Color(0, 0, 0, 1);
            runButtonEnergy.color = new Color(1, 0.5f, 0, 1);
            startButton.interactable = true;
        }
    }
    public void SpendEnergy()
    {
        playerData.energy -= 2;
        UpdateEnergy();
    }

    public void RestoreEnergy()
    {
        playerData.RestoreEnergy(2);
        playerData.energyTickTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        UpdateEnergy();
    }
    void OnDestroy()
    {
        playerData.saveAction -= UpdateEnergy;
    }
}
