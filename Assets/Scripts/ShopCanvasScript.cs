using System;
using UnityEngine;

public class ShopCanvasScript : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] GameObject randomRewardPanel;
    [SerializeField] SkinManager skinManager;
    public event Action SkinPicked;
    

    public void PickSkin(SkinData data)
    {
        
        if (playerData.unlockedSkins[data.skinNumber])
        {
            playerData.selectedSkin = playerData.skins[data.skinNumber];
            skinManager.SetSkin();
            SkinPicked?.Invoke();
            
        }
        else
        {
            if (data.skinPrice <= playerData.savedMoney)
            {
                playerData.savedMoney -= data.skinPrice;
                playerData.unlockedSkins[data.skinNumber] = true;

                if (data.skinNumber < 8)
                {
                    playerData.availableRareSkins.Remove(playerData.skins[data.skinNumber]);
                }
                else if (data.skinNumber  < 14)
                {
                    playerData.availableEpicSkins.Remove(playerData.skins[data.skinNumber]);
                }
                else
                {
                    playerData.availableLegendarySkins.Remove(playerData.skins[data.skinNumber]);
                }
            }
            SkinPicked?.Invoke();
        }
        playerData.SavePlayerData();
    }
    public void OpenRandomRewardPanel()
    {
        randomRewardPanel.SetActive(true);
    }
    public void CloseRandomRewardPanel()
    {
        randomRewardPanel.SetActive(false);
    }
}
