
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GetRandomReward : MonoBehaviour
{
    [SerializeField] RewardedAdScript rewardedAdScript;
    public List<GameObject> availableRewards;
    [SerializeField] PlayerData playerData;
    private bool notRolling = true;
    private int k = 0;
    private int j;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rewardedAdScript.rewardShown += RollForSkin;
    }
    void OnDestroy()
    {
        rewardedAdScript.rewardShown -= RollForSkin;
    }
    // Update is called once per frame

    public void RollForSkin()
    {
        if (notRolling)
        {
            StartCoroutine("Roll");
        }
        
        
    }
    IEnumerator Roll()
    {
        notRolling = false;
        for (int i = 0; i < 10; i++)
        {
            availableRewards[k].GetComponent<Outline>().enabled = false;
            do 
            {
                j = Random.Range(0, availableRewards.Count);
            }while(k == j);
            k = j;
            if (i == 9)
            {
                j = Random.Range(0, 100);
                
                switch (j)
                {
                    case int j when j < 40:
                    {
                        k = Random.Range(0, 2);
                        break;
                    }
                    case int j when (j > 40) && (j < 50):
                    {
                        k = 2;
                        break;
                    }
                    case int j when (j > 50) && (j < 75):
                    {
                        k = Random.Range(3, 5);
                        break;
                    }
                    case int j when (j > 75) && (j < 80):
                    {
                        k = 5;
                        break;
                    }
                    case int j when (j > 80) && (j < 98):
                    {
                        k = Random.Range(6, 8);
                        break;
                    }
                    default:
                    {
                        k = 9;
                        break;
                    }
                }
            }
            availableRewards[k].GetComponent<Outline>().enabled = true;
            var timer = (float)math.pow(i * 0.3f, 2) * 0.1f+0.15f;
            yield return new WaitForSeconds(timer);
            
        }
        switch (availableRewards[k].GetComponent<RewardType>().itemType)
        {
            case RewardType.ItemType.Energy:
            {
                
                playerData.energy += availableRewards[k].GetComponent<RewardType>().value;
                
                break;
            }
            case RewardType.ItemType.Money:
            {
                
                playerData.savedMoney += availableRewards[k].GetComponent<RewardType>().value;
                
                break;
            }
            case RewardType.ItemType.Skin:
            {
                
                var rarity = availableRewards[k].GetComponent<RewardType>().value;
                
                switch (rarity)
                {
                    case 1:
                    {
                        var randNum = Random.Range(0, playerData.availableRareSkins.Count);
                        
                        var skin = playerData.availableRareSkins[randNum];
                        
                        foreach (var a in playerData.availableRareSkins)
                        {
                            
                        }
                        playerData.availableRareSkins.RemoveAt(randNum);
                        foreach (var a in playerData.availableRareSkins)
                        {
                            
                        }
                        playerData.unlockedSkins[playerData.skins.IndexOf(skin)] = true;
                        foreach (var a in playerData.unlockedSkins)
                        {
                            
                        }
                        break;
                    }
                    case 2:
                    {
                        var randNum = Random.Range(0, playerData.availableEpicSkins.Count);
                        
                        var skin = playerData.availableEpicSkins[randNum];
                        
                        foreach (var a in playerData.availableEpicSkins)
                        {
                            
                        }
                        playerData.availableEpicSkins.RemoveAt(randNum);
                        foreach (var a in playerData.availableEpicSkins)
                        {
                            
                        }
                        playerData.unlockedSkins[playerData.skins.IndexOf(skin)] = true;
                        foreach (var a in playerData.unlockedSkins)
                        {
                            
                        }
                        break;
                    }
                    case 3:
                    {
                        var randNum = Random.Range(0, playerData.availableLegendarySkins.Count);
                        
                        var skin = playerData.availableLegendarySkins[randNum];
                        
                        foreach (var a in playerData.availableLegendarySkins)
                        {
                            
                        }
                        playerData.availableLegendarySkins.RemoveAt(randNum);
                        foreach (var a in playerData.availableLegendarySkins)
                        {
                            
                        }
                        playerData.unlockedSkins[playerData.skins.IndexOf(skin)] = true;
                        foreach (var a in playerData.unlockedSkins)
                        {
                            
                        }
                        break;
                    }
                }
                break;
            }
            default:
            {
                break;
            }
        }
        
        notRolling = true;
        playerData.SavePlayerData();
    }
    
}
