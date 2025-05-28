using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeData : MonoBehaviour
{
    public int price;
    public int lvl;
    public int upgradeNumber;
    [SerializeField] int maxLvl;
    [SerializeField] PlayerData playerData;
    [SerializeField] TextMeshProUGUI priceTMP;
    void Start()
    {
        price = playerData.upgradePrices[upgradeNumber];
        lvl = playerData.upgradeLevels[upgradeNumber];
        CheckPrice();
        playerData.saveAction += CheckPrice;
    }

    private void CheckPrice()
    {
        Debug.Log("SaveRecieved");
        if (lvl < maxLvl)
        {
            priceTMP.text = $"{price}";
            if (price > playerData.savedMoney)
            {
                gameObject.GetComponent<Button>().interactable = false;
                priceTMP.color = new Color(1, 0.5f, 0.5f);
            }
        }
        else
        {
            priceTMP.text = "Max lvl";
            gameObject.GetComponent<Button>().interactable = false;
            priceTMP.color = new Color(1, 0.5f, 0.5f);
        }
        
    }
    void OnDestroy()
    {
        playerData.saveAction -= CheckPrice;
    }
}
