using UnityEngine;
using System.Collections.Generic;

public class CollectibleFactory : MonoBehaviour
{
    // Factory Pattern Implementation - Creates different types of collectibles
    
    [Header("Factory Pattern - Collectible Prefabs")]
    public GameObject coinPrefab;
    public GameObject heartChestPrefab;
    public GameObject extraLifePrefab;
    public GameObject speedBoostPrefab;
    public GameObject dashRechargePrefab;
    
    // Factory Pattern - Collectible registry
    private static Dictionary<CollectibleType, CollectibleData> collectibleRegistry;
    private static CollectibleFactory instance;
    
    void Awake()
    {
        instance = this;
        InitializeFactory();
    }
    
    void InitializeFactory()
    {
        // Factory Pattern - Register all collectible types with their data
        collectibleRegistry = new Dictionary<CollectibleType, CollectibleData>()
        {
            { CollectibleType.Coin, new CollectibleData(CollectibleType.Coin, coinPrefab, 1, "CollectCoinSound", "Increases score") },
            { CollectibleType.Heart, new CollectibleData(CollectibleType.Heart, heartChestPrefab, 1, "OpenChestSound", "Restores one life") },
            { CollectibleType.ExtraLife, new CollectibleData(CollectibleType.ExtraLife, extraLifePrefab, 1, "CollectCoinSound", "Grants extra life") },
            { CollectibleType.SpeedBoost, new CollectibleData(CollectibleType.SpeedBoost, speedBoostPrefab, 5, "CollectCoinSound", "Temporary speed increase") },
            { CollectibleType.DashRecharge, new CollectibleData(CollectibleType.DashRecharge, dashRechargePrefab, 1, "DashSound", "Recharges dash ability") }
        };
        
        Debug.Log("[Factory] CollectibleFactory initialized with " + collectibleRegistry.Count + " collectible types");
    }
    
    // Factory Pattern - Main factory method
    public static GameObject CreateCollectible(CollectibleType type, Vector3 position, Quaternion rotation = default)
    {
        if (collectibleRegistry == null || !collectibleRegistry.ContainsKey(type))
        {
            Debug.LogError($"[Factory] Collectible type {type} not registered in factory!");
            return null;
        }
        
        CollectibleData data = collectibleRegistry[type];
        
        if (data.prefab == null)
        {
            Debug.LogError($"[Factory] No prefab assigned for collectible type {type}!");
            return null;
        }
        
        // Factory Pattern - Create the collectible instance
        GameObject collectible = Instantiate(data.prefab, position, rotation);
        
        Debug.Log($"[Factory] Created {type} collectible at {position}");
        
        // Trigger Observer Pattern event for factory creation
        GameEventManager.TriggerSoundRequested("CollectCoinSound"); // Generic creation sound
        
        return collectible;
    }
    
    // Factory Pattern - Overload with Transform parent
    public static GameObject CreateCollectible(CollectibleType type, Vector3 position, Transform parent, Quaternion rotation = default)
    {
        GameObject collectible = CreateCollectible(type, position, rotation);
        if (collectible != null && parent != null)
        {
            collectible.transform.SetParent(parent);
        }
        return collectible;
    }
    
    // Factory Pattern - Get collectible data without creating
    public static CollectibleData GetCollectibleData(CollectibleType type)
    {
        if (collectibleRegistry != null && collectibleRegistry.ContainsKey(type))
        {
            return collectibleRegistry[type];
        }
        return null;
    }
    
    // Factory Pattern - Get all available collectible types
    public static CollectibleType[] GetAvailableTypes()
    {
        if (collectibleRegistry == null) return new CollectibleType[0];
        
        CollectibleType[] types = new CollectibleType[collectibleRegistry.Count];
        collectibleRegistry.Keys.CopyTo(types, 0);
        return types;
    }
    
    // Factory Pattern - Batch creation method
    public static GameObject[] CreateMultipleCollectibles(CollectibleType type, Vector3[] positions)
    {
        GameObject[] collectibles = new GameObject[positions.Length];
        
        for (int i = 0; i < positions.Length; i++)
        {
            collectibles[i] = CreateCollectible(type, positions[i]);
        }
        
        Debug.Log($"[Factory] Created {positions.Length} {type} collectibles");
        return collectibles;
    }
    
    // Factory Pattern - Random collectible creator (useful for procedural generation)
    public static GameObject CreateRandomCollectible(Vector3 position)
    {
        if (collectibleRegistry == null || collectibleRegistry.Count == 0) return null;
        
        CollectibleType[] types = GetAvailableTypes();
        CollectibleType randomType = types[Random.Range(0, types.Length)];
        
        Debug.Log($"[Factory] Creating random collectible: {randomType}");
        return CreateCollectible(randomType, position);
    }
} 