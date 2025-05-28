using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinData : MonoBehaviour
{
    public int skinNumber;
    public int skinPrice;
    [SerializeField] TextMeshProUGUI priceTag;
    [SerializeField] PlayerData pd;
    [SerializeField] ShopCanvasScript shopCanvasScript;

    void Awake()
    {
        shopCanvasScript.SkinPicked += UpdateUI;
        priceTag.text = $"{skinPrice}";
        UpdateUI();
    }
    void UpdateUI()
    {
        
        Outline outline = GetComponentInParent<Outline>();
        if (pd != null)
        {
            if (pd.unlockedSkins[skinNumber])
            {
                priceTag.enabled = false;
                if (pd.selectedSkin == pd.skins[skinNumber])
                {
                    outline.enabled = true;
                    outline.effectColor = Color.black;
                }
                else
                {
                    outline.enabled = false;
                }
            }
            else
            {
                outline.enabled = false;
            }
            
        }
    }
    void OnDisable()
    {
        
        shopCanvasScript.SkinPicked -= UpdateUI;
    }

}
