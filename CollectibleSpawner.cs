using UnityEngine;
using System.Collections;

public class CollectibleSpawner : MonoBehaviour
{
    // Factory Pattern Demonstration - Shows how to use the CollectibleFactory
    
    [Header("Factory Pattern Demo Settings")]
    public bool enableAutoSpawning = false;
    public float spawnInterval = 3f;
    public Transform[] spawnPoints;
    public CollectibleType[] typesToSpawn = { CollectibleType.Coin, CollectibleType.Heart };
    
    void Start()
    {
        Debug.Log("[Factory Demo] CollectibleSpawner initialized");
        
        if (enableAutoSpawning)
        {
            StartCoroutine(AutoSpawnCollectibles());
        }
        
        // Factory Pattern Demo - Show factory capabilities
        DemonstrateFactoryPattern();
    }
    
    void DemonstrateFactoryPattern()
    {
        Debug.Log("[Factory Demo] Demonstrating Factory Pattern capabilities:");
        
        // Factory Pattern - Get available types
        CollectibleType[] availableTypes = CollectibleFactory.GetAvailableTypes();
        Debug.Log($"[Factory Demo] Available collectible types: {availableTypes.Length}");
        
        foreach (CollectibleType type in availableTypes)
        {
            CollectibleData data = CollectibleFactory.GetCollectibleData(type);
            if (data != null)
            {
                Debug.Log($"[Factory Demo] {type}: {data.description} (Value: {data.value})");
            }
        }
    }
    
    IEnumerator AutoSpawnCollectibles()
    {
        while (enableAutoSpawning)
        {
            yield return new WaitForSeconds(spawnInterval);
            
            if (spawnPoints.Length > 0 && typesToSpawn.Length > 0)
            {
                // Factory Pattern - Create collectible using factory
                Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                CollectibleType typeToCreate = typesToSpawn[Random.Range(0, typesToSpawn.Length)];
                
                GameObject collectible = CollectibleFactory.CreateCollectible(typeToCreate, spawnPosition);
                
                if (collectible != null)
                {
                    Debug.Log($"[Factory Demo] Auto-spawned {typeToCreate} at {spawnPosition}");
                }
            }
        }
    }
    
    // Factory Pattern Demo - Manual spawning methods (can be called from UI or other scripts)
    [ContextMenu("Spawn Random Collectible")]
    public void SpawnRandomCollectible()
    {
        if (spawnPoints.Length > 0)
        {
            Vector3 randomPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            GameObject collectible = CollectibleFactory.CreateRandomCollectible(randomPosition);
            Debug.Log($"[Factory Demo] Manually spawned random collectible at {randomPosition}");
        }
    }
    
    [ContextMenu("Spawn Coin")]
    public void SpawnCoin()
    {
        SpawnSpecificCollectible(CollectibleType.Coin);
    }
    
    [ContextMenu("Spawn Heart")]
    public void SpawnHeart()
    {
        SpawnSpecificCollectible(CollectibleType.Heart);
    }
    
    public void SpawnSpecificCollectible(CollectibleType type)
    {
        if (spawnPoints.Length > 0)
        {
            Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            GameObject collectible = CollectibleFactory.CreateCollectible(type, spawnPosition);
            Debug.Log($"[Factory Demo] Manually spawned {type} at {spawnPosition}");
        }
    }
    
    // Factory Pattern Demo - Batch spawning
    [ContextMenu("Spawn Multiple Coins")]
    public void SpawnMultipleCoins()
    {
        if (spawnPoints.Length > 0)
        {
            Vector3[] positions = new Vector3[spawnPoints.Length];
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                positions[i] = spawnPoints[i].position;
            }
            
            GameObject[] coins = CollectibleFactory.CreateMultipleCollectibles(CollectibleType.Coin, positions);
            Debug.Log($"[Factory Demo] Batch spawned {coins.Length} coins");
        }
    }
    
    void Update()
    {
        // Factory Pattern Demo - Keyboard shortcuts for testing
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SpawnCoin();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            SpawnHeart();
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            SpawnRandomCollectible();
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            SpawnMultipleCoins();
        }
    }
} 