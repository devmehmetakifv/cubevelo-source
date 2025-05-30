using UnityEngine;

public enum CollectibleType
{
    Coin,
    Heart,
    ExtraLife,
    SpeedBoost,
    DashRecharge
}

// Collectible data structure for Factory pattern
[System.Serializable]
public class CollectibleData
{
    public CollectibleType type;
    public GameObject prefab;
    public int value;
    public string collectSound;
    public string description;
    
    public CollectibleData(CollectibleType collectibleType, GameObject prefabReference, int itemValue, string sound, string desc)
    {
        type = collectibleType;
        prefab = prefabReference;
        value = itemValue;
        collectSound = sound;
        description = desc;
    }
} 