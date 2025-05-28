using UnityEngine;

public class UpgradeCanvasScript : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    
    public void BuyHealth(UpgradeData upgradeData)
    {
        playerData.savedMoney -= upgradeData.price;
        playerData.upgradePrices[0] += 100;
        playerData.upgradeLevels[0] += 1;
        playerData.playerMaxHealth += 1;
        playerData.SavePlayerData();
    }
    public void BuyFireProtection(UpgradeData upgradeData)
    {
        playerData.savedMoney -= upgradeData.price;
        playerData.upgradeLevels[1] += 1;
        playerData.diesFromFire = false;
        playerData.SavePlayerData();
    }
    public void BuyHealingStrenght(UpgradeData upgradeData)
    {
        playerData.savedMoney -=  upgradeData.price;
        playerData.upgradePrices[2] += 500;
        playerData.upgradeLevels[2] += 1;
        playerData.healingStrenght += 1;
        playerData.SavePlayerData();
    }
    public void HealthRegeneration(UpgradeData upgradeData)
    {
        playerData.savedMoney -= upgradeData.price;
        playerData.upgradePrices[3] += 500;
        playerData.upgradeLevels[3] += 1;
        playerData.healthRegeneration += 1;
        playerData.SavePlayerData();
    }
    public void SecondLife(UpgradeData upgradeData)
    {
        playerData.savedMoney -= upgradeData.price;
        playerData.secondLife = true;
        playerData.upgradeLevels[4] += 1;
        playerData.SavePlayerData();
    }
    public void Shield(UpgradeData upgradeData)
    {
        playerData.savedMoney -= upgradeData.price;
        playerData.shield = true;
        playerData.upgradeLevels[5] += 1;
        playerData.SavePlayerData();
    }

}
