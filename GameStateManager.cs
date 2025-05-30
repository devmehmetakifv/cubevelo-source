using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    // Production component that uses Facade Pattern for centralized game management
    
    [Header("Game State Management")]
    public bool enableDebugLogging = false;
    public KeyCode gameStateKey = KeyCode.Tab;
    public KeyCode restartKey = KeyCode.R;
    
    [Header("UI References (Optional)")]
    public Text debugText;
    
    private float lastStateCheck = 0f;
    private const float STATE_CHECK_INTERVAL = 1f;
    
    void Start()
    {
        Debug.Log("[GameState] GameStateManager initialized - using Facade Pattern for game coordination");
        
        // Facade Pattern - Initialize game systems through simplified interface
        InitializeGameSystems();
    }
    
    void Update()
    {
        // Facade Pattern - Handle input for game state management
        HandleGameStateInput();
        
        // Facade Pattern - Periodic state monitoring
        if (Time.time - lastStateCheck > STATE_CHECK_INTERVAL)
        {
            MonitorGameState();
            lastStateCheck = Time.time;
        }
    }
    
    void InitializeGameSystems()
    {
        // Facade Pattern - Use simplified interface to verify all systems are ready
        GameStateInfo state = GameFacade.GetGameState();
        
        if (enableDebugLogging)
        {
            Debug.Log($"[GameState] Systems initialized: {state}");
        }
    }
    
    void HandleGameStateInput()
    {
        // Facade Pattern - Centralized input handling using Facade methods
        if (Input.GetKeyDown(gameStateKey))
        {
            DisplayCurrentGameState();
        }
        
        if (Input.GetKeyDown(restartKey))
        {
            RestartGameSafely();
        }
        
        // Production feature: Quick sound test
        if (Input.GetKeyDown(KeyCode.T))
        {
            TestGameSounds();
        }
    }
    
    void MonitorGameState()
    {
        // Facade Pattern - Use Facade to get aggregated game state
        GameStateInfo state = GameFacade.GetGameState();
        
        // Update debug UI if available
        if (debugText != null)
        {
            debugText.text = $"Lives: {state.lives} | Coins: {state.coins} | Level: {state.currentLevel}";
        }
        
        // Production monitoring: Check for game over conditions
        if (state.lives <= 0 && !state.isDead)
        {
            Debug.Log("[GameState] Game over condition detected");
        }
        
        // Production monitoring: Achievement checks
        CheckAchievements(state);
    }
    
    void DisplayCurrentGameState()
    {
        // Facade Pattern - Use Facade to get complete game state information
        GameStateInfo state = GameFacade.GetGameState();
        
        string stateMessage = $"[GameState] Current State: {state}";
        Debug.Log(stateMessage);
        
        // Could trigger UI display, save state, etc.
    }
    
    void RestartGameSafely()
    {
        // Facade Pattern - Use Facade for safe game restart
        Debug.Log("[GameState] Player initiated restart");
        GameFacade.RestartGame();
    }
    
    void TestGameSounds()
    {
        // Facade Pattern - Use Facade for safe sound testing
        string[] testSounds = { "JumpSound", "DashSound", "CollectCoinSound", "CheckpointSound" };
        string randomSound = testSounds[Random.Range(0, testSounds.Length)];
        
        GameFacade.PlaySoundSafely(randomSound, transform.position);
        Debug.Log($"[GameState] Testing sound: {randomSound}");
    }
    
    void CheckAchievements(GameStateInfo state)
    {
        // Production feature: Achievement system using Facade pattern
        
        // Example achievement checks
        if (state.coins >= 10)
        {
            UnlockAchievement("Coin Collector");
        }
        
        if (state.lives == 5)
        {
            UnlockAchievement("Full Health");
        }
        
        if (state.currentLevel >= 3)
        {
            UnlockAchievement("Level Master");
        }
    }
    
    void UnlockAchievement(string achievementName)
    {
        // Facade Pattern - Use Facade for achievement sound and notification
        GameFacade.PlaySoundSafely("CheckpointSound"); // Achievement sound
        Debug.Log($"[GameState] Achievement Unlocked: {achievementName}");
        
        // Could trigger UI notification, save achievement data, etc.
    }
    
    // Production method: Manual game state queries
    public void LogGameState()
    {
        // Facade Pattern - Public method using Facade for external access
        GameStateInfo state = GameFacade.GetGameState();
        Debug.Log($"[GameState] Manual state query: {state}");
    }
    
    // Production method: Emergency reset
    public void EmergencyReset()
    {
        // Facade Pattern - Emergency reset using Facade
        Debug.LogWarning("[GameState] Emergency reset initiated!");
        GameFacade.RestartGame();
    }
    
    // Production method: Get performance metrics
    public void GetPerformanceMetrics()
    {
        // Facade Pattern - Use Facade to gather performance data
        GameStateInfo state = GameFacade.GetGameState();
        
        string metrics = $"Performance Metrics:\n" +
                        $"Current Level: {state.currentLevel}\n" +
                        $"Survival Rate: {(float)state.lives / 5f * 100f:F1}%\n" +
                        $"Collection Rate: {state.coins} coins\n" +
                        $"Active: {!state.isDead}";
        
        Debug.Log($"[GameState] {metrics}");
    }
} 