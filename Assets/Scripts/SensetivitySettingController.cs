using UnityEngine;
using UnityEngine.UI;

public class SensetivitySettingController : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetComponent<Slider>().value = playerData.sensetivity;
    }

    public void ChangeSensetivity()
    {
        playerData.sensetivity = gameObject.GetComponent<Slider>().value;
    }
}
