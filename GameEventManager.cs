using System;
using UnityEngine;

public static class GameEventManager
{
    // Observer Pattern Implementation - Events (Subjects)
    
    // Coin collection events
    public static event Action<int> OnCoinCollected;           // Passes coin amount
    public static event Action<Vector3> OnCoinCollectedAt;     // Passes collection position
    
    // Level progression events  
    public static event Action<int> OnLevelCompleted;          // Passes level index
    public static event Action<float, int> OnLevelCompletedWithData; // Passes time and coins
    
    // Player health events
    public static event Action<int> OnPlayerDamaged;           // Passes remaining lives
    public static event Action OnPlayerDied;                  // Player death notification
    public static event Action<int> OnPlayerHealed;           // Passes new life count
    
    // Checkpoint events
    public static event Action<Vector3> OnCheckpointActivated; // Passes checkpoint position
    
    // Audio events (for centralized sound management)
    public static event Action<string> OnSoundRequested;      // Passes sound name
    
    // === EVENT TRIGGER METHODS (Called by game objects) ===
    
    public static void TriggerCoinCollected(int newCoinAmount, Vector3 position)
    {
        OnCoinCollected?.Invoke(newCoinAmount);
        OnCoinCollectedAt?.Invoke(position);
        Debug.Log($"[Observer] Coin collected at {position}. Total coins: {newCoinAmount}");
    }
    
    public static void TriggerLevelCompleted(int levelIndex, float completionTime, int totalCoins)
    {
        OnLevelCompleted?.Invoke(levelIndex);
        OnLevelCompletedWithData?.Invoke(completionTime, totalCoins);
        Debug.Log($"[Observer] Level {levelIndex} completed in {completionTime:F2}s with {totalCoins} coins");
    }
    
    public static void TriggerPlayerDamaged(int remainingLives)
    {
        OnPlayerDamaged?.Invoke(remainingLives);
        if (remainingLives <= 0)
        {
            OnPlayerDied?.Invoke();
        }
        Debug.Log($"[Observer] Player damaged. Lives remaining: {remainingLives}");
    }
    
    public static void TriggerPlayerHealed(int newLifeCount)
    {
        OnPlayerHealed?.Invoke(newLifeCount);
        Debug.Log($"[Observer] Player healed. Lives: {newLifeCount}");
    }
    
    public static void TriggerCheckpointActivated(Vector3 checkpointPosition)
    {
        OnCheckpointActivated?.Invoke(checkpointPosition);
        Debug.Log($"[Observer] Checkpoint activated at {checkpointPosition}");
    }
    
    public static void TriggerSoundRequested(string soundName)
    {
        OnSoundRequested?.Invoke(soundName);
        Debug.Log($"[Observer] Sound requested: {soundName}");
    }
    
    // === CLEANUP METHOD (Good practice for static events) ===
    public static void UnsubscribeAll()
    {
        OnCoinCollected = null;
        OnCoinCollectedAt = null;
        OnLevelCompleted = null;
        OnLevelCompletedWithData = null;
        OnPlayerDamaged = null;
        OnPlayerDied = null;
        OnPlayerHealed = null;
        OnCheckpointActivated = null;
        OnSoundRequested = null;
        Debug.Log("[Observer] All event subscriptions cleared");
    }
} 