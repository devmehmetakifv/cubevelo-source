using UnityEngine;

public interface ICollectible
{
    // Factory Pattern - Common interface for all collectibles
    CollectibleType GetCollectibleType();
    void OnCollected(GameObject collector);
    int GetValue();
    string GetDescription();
} 