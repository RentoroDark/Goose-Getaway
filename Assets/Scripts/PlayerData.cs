
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
[System.Serializable]
public class PlayerData : ScriptableObject
{
    public event Action OnVolumeChange;
    public int HighScore;
    public int energy;
    public int maxEnergy;
    public long energyTickTime;
    public int savedMoney;
    public int playerMaxHealth;
    public List<SkinManager.Skin> skins;
    public SkinManager.Skin selectedSkin;
    public List<bool> unlockedSkins;
    public bool diesFromFire = true;
    public event System.Action saveAction;
    public int healingStrenght;
    public int healthRegeneration;
    public bool secondLife;
    public bool shield;
    public int[] upgradePrices;
    public int[] upgradeLevels;
    public float sensetivity;
    public float SFXvolume;
    public float musicVolume;
    public List<SkinManager.Skin> availableRareSkins;
    public List<SkinManager.Skin> availableEpicSkins;
    public List<SkinManager.Skin> availableLegendarySkins;
    public int GetMaxHealth()
    {
        return playerMaxHealth;
    }
    public void Add(int amount)
    {
        savedMoney += amount;
        SavePlayerData();
    }
    public void SavePlayerData()
    {
        saveAction?.Invoke();
    }
    public void RestoreEnergy(int amount)
    {
        if (energy < 20)
        {
            energy += amount;
        }
    }
    
    
}
