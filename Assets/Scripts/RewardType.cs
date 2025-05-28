using UnityEngine;

public class RewardType : MonoBehaviour
{
    public ItemType itemType;
    public int value;
    public enum ItemType
    {
        Energy,
        Money,
        Skin
    }
}
