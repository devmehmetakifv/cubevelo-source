using UnityEngine;

public class GameEventSubscriber : MonoBehaviour
{
    // Observer Pattern - This class demonstrates how to subscribe to events
    
    void Start()
    {
        // Subscribe to game events (Observer Pattern)
        GameEventManager.OnCoinCollected += HandleCoinCollected;
        GameEventManager.OnCoinCollectedAt += HandleCoinCollectedAt;
        GameEventManager.OnLevelCompleted += HandleLevelCompleted;
        GameEventManager.OnLevelCompletedWithData += HandleLevelCompletedWithData;
        GameEventManager.OnPlayerDamaged += HandlePlayerDamaged;
        GameEventManager.OnPlayerDied += HandlePlayerDied;
        GameEventManager.OnPlayerHealed += HandlePlayerHealed;
        GameEventManager.OnCheckpointActivated += HandleCheckpointActivated;
        
        Debug.Log("[Observer] GameEventSubscriber initialized and listening to all events");
    }
    
    void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks (Good Observer pattern practice)
        GameEventManager.OnCoinCollected -= HandleCoinCollected;
        GameEventManager.OnCoinCollectedAt -= HandleCoinCollectedAt;
        GameEventManager.OnLevelCompleted -= HandleLevelCompleted;
        GameEventManager.OnLevelCompletedWithData -= HandleLevelCompletedWithData;
        GameEventManager.OnPlayerDamaged -= HandlePlayerDamaged;
        GameEventManager.OnPlayerDied -= HandlePlayerDied;
        GameEventManager.OnPlayerHealed -= HandlePlayerHealed;
        GameEventManager.OnCheckpointActivated -= HandleCheckpointActivated;
        
        Debug.Log("[Observer] GameEventSubscriber unsubscribed from all events");
    }
    
    // === EVENT HANDLERS (Observer Pattern - Concrete Observers) ===
    
    private void HandleCoinCollected(int totalCoins)
    {
        Debug.Log($"[Observer] Subscriber detected coin collection! Total coins: {totalCoins}");
        // Could trigger UI updates, achievements, save data, etc.
    }
    
    private void HandleCoinCollectedAt(Vector3 position)
    {
        Debug.Log($"[Observer] Coin collected at position: {position}");
        // Could spawn particle effects, update minimap, etc.
    }
    
    private void HandleLevelCompleted(int levelIndex)
    {
        Debug.Log($"[Observer] Level {levelIndex} completed!");
        // Could trigger level completion UI, save progress, etc.
    }
    
    private void HandleLevelCompletedWithData(float time, int coins)
    {
        Debug.Log($"[Observer] Level completed with time: {time} and coins: {coins}");
        // Could calculate score, update leaderboards, etc.
    }
    
    private void HandlePlayerDamaged(int remainingLives)
    {
        Debug.Log($"[Observer] Player took damage! Lives remaining: {remainingLives}");
        // Could trigger screen shake, damage indicators, etc.
    }
    
    private void HandlePlayerDied()
    {
        Debug.Log("[Observer] Player died!");
        // Could trigger death animation, game over screen, etc.
    }
    
    private void HandlePlayerHealed(int newLifeCount)
    {
        Debug.Log($"[Observer] Player healed! Lives: {newLifeCount}");
        // Could trigger healing effects, UI updates, etc.
    }
    
    private void HandleCheckpointActivated(Vector3 position)
    {
        Debug.Log($"[Observer] Checkpoint activated at: {position}");
        // Could save game state, show notification, etc.
    }
} 