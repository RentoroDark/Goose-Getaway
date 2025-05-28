using UnityEngine;

public class EscapeScript : MonoBehaviour
{
    [SerializeField] PlayerMoney playerMoney;
    [SerializeField] PlayerData playerData;
    
    public void SaveMoney()
    {
        playerData.Add(playerMoney.collectedMoney);
    }
}
