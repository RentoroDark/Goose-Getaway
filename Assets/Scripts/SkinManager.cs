using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] List<GameObject> skinAtributes;
    [SerializeField] List<Material> skinMaterials;
    [SerializeField] Material[] goldenMaterials;
    [SerializeField] Material[] cosmicMaterials;
    [SerializeField] Material[] lavaMaterials;
    [SerializeField] GameObject mainBody;
    [SerializeField] FooprintsController footprints;
    [SerializeField] Renderer footprintRenderer;
    [SerializeField] Material[] footprintMaterials;
    
    public enum Skin
    {
        Base,
        Wild,
        RedPunk,
        GreenPunk,
        PinkPunk,
        PinkEmo,
        YelowEmo,
        WhiteEmo,
        Chicken,
        BrownChicken,
        Cylindre,
        GoldenCylindre,
        Gachi,
        GoldenGachi,
        Fire,
        Cosmic
    }

        void Start()
    {
        SetSkin();
        
    }

    
    

    public void SetSkin()
    {
        Strip();
        switch (playerData.selectedSkin)
        {
            case Skin.Base:
            {
                
                break;
            }
            case Skin.Wild:
            {
                skinMaterials[0].SetColor("_Color", new Color(0.1f, 0.1f, 0.1f, 1));
                skinMaterials[1].SetColor("_Color", new Color(0.18f, 0.1f, 0.07f, 1));
                break;
            }
            case Skin.RedPunk:
            {
                skinAtributes[0].SetActive(true);
                skinMaterials[2].SetColor("_Color", Color.red);
                break;
            }
            case Skin.GreenPunk:
            {
                skinAtributes[0].SetActive(true);
                skinMaterials[2].SetColor("_Color", Color.green);
                break;
            }
            case Skin.PinkPunk:
            {
                skinAtributes[0].SetActive(true);
                skinMaterials[2].SetColor("_Color", new Color(1, 0, 0.78f, 1));
                break;
            }
            case Skin.PinkEmo:
            {
                skinAtributes[1].SetActive(true);
                skinAtributes[2].SetActive(true);
                skinMaterials[3].SetColor("_Color", new Color(0, 0, 0, 1));
                skinMaterials[4].SetColor("_Color", new Color(1, 0, 0.78f, 1));
                skinMaterials[5].SetColor("_Color", Color.white);
                skinMaterials[6].SetColor("_Color", Color.red);
                break;
            }
            case Skin.YelowEmo:
            {
                skinAtributes[1].SetActive(true);
                skinAtributes[2].SetActive(true);
                skinMaterials[3].SetColor("_Color", new Color(0, 0, 0, 1));
                skinMaterials[4].SetColor("_Color", Color.yellow);
                skinMaterials[5].SetColor("_Color", Color.black);
                skinMaterials[6].SetColor("_Color", Color.red);
                break;
            }
            case Skin.WhiteEmo:
            {
                skinAtributes[1].SetActive(true);
                skinAtributes[2].SetActive(true);
                skinMaterials[3].SetColor("_Color", new Color(0.9f, 0.9f, 0.9f, 1));
                skinMaterials[4].SetColor("_Color", new Color(0.9f, 0.9f, 0.9f, 1));
                skinMaterials[5].SetColor("_Color", new Color(0.55f, 0, 0, 1));
                skinMaterials[6].SetColor("_Color", new Color(0.55f, 0, 0, 1));
                break;
            }
            case Skin.Chicken:
            {
                skinAtributes[3].SetActive(true);
                break;
            }
            case Skin.BrownChicken:
            {
                skinAtributes[3].SetActive(true);
                skinMaterials[0].SetColor("_Color", new Color(0.43f, 0.19f, 0.1f, 1));
                skinMaterials[1].SetColor("_Color", new Color(0.43f, 0.19f, 0.1f, 1));
                skinMaterials[10].SetColor("_Color", new Color(0.43f, 0.19f, 0.1f, 1));
                break;
            }
            case Skin.Cylindre:
            {
                skinAtributes[4].SetActive(true);
                break;
            }
            case Skin.GoldenCylindre:
            {
                skinAtributes[4].SetActive(true);
                Renderer renderer = mainBody.GetComponent<Renderer>();
                renderer.materials = goldenMaterials;
                break;
            }
            case Skin.Gachi:
            {
                skinAtributes[5].SetActive(true);
                skinAtributes[6].SetActive(true);
                break;
            }
            case Skin.GoldenGachi:
            {
                skinAtributes[5].SetActive(true);
                skinAtributes[6].SetActive(true);
                Renderer renderer = mainBody.GetComponent<Renderer>();
                renderer.materials = goldenMaterials;
                break;
            }
            case Skin.Fire:
            {
                
                Renderer renderer = mainBody.GetComponent<Renderer>();
                renderer.materials = lavaMaterials;
                footprints.enabled = true;
                footprintRenderer.material = footprintMaterials[0];
                break;
            }
            case Skin.Cosmic:
            {
                Renderer renderer = mainBody.GetComponent<Renderer>();
                renderer.materials = cosmicMaterials;
                footprints.enabled = true;
                footprintRenderer.material = footprintMaterials[1];
                break;
            }
        }
        /* playerData.SavePlayerData(); */
    }

    private void Strip()
    {
        foreach (var a in skinAtributes)
        {
            footprints.enabled = false;
            a.SetActive(false);
            skinMaterials[0].SetColor("_Color", new Color(1f, 1f, 1f, 1));
            skinMaterials[1].SetColor("_Color", new Color(1f, 1f, 1f, 1));
            skinMaterials[10].SetColor("_Color", new Color(1f, 1f, 1f, 1));
        }
    }
}
