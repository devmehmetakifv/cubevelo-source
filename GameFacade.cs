using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameFacade : MonoBehaviour
{
    // Facade Pattern Implementation - Simplified interface to complex game systems
    
    [Header("Facade Pattern - System References")]
    public CoinSystem coinSystem;
    public LifeSystem lifeSystem;
    public PlayerMovement playerMovement;
    public Timer gameTimer;
    
    // Facade Pattern - Singleton instance for easy access
    private static GameFacade instance;
    public static GameFacade Instance 
    { 
        get 
        { 
            if (instance == null)
            {
                instance = FindObjectOfType<GameFacade>();
                if (instance == null)
                {
                    Debug.LogError("[Facade] GameFacade instance not found in scene!");
                }
            }
            return instance; 
        } 
    }
    
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        
        Debug.Log("[Facade] GameFacade initialized - providing simplified interface to game systems");
    }
    
    void Start()
    {
        // Auto-find components if not assigned
        if (coinSystem == null) coinSystem = FindObjectOfType<CoinSystem>();
        if (lifeSystem == null) lifeSystem = FindObjectOfType<LifeSystem>();
        if (playerMovement == null) playerMovement = FindObjectOfType<PlayerMovement>();
        if (gameTimer == null) gameTimer = FindObjectOfType<Timer>();
    }
    
    // === FACADE PATTERN - SIMPLIFIED HIGH-LEVEL OPERATIONS ===
    
    /// <summary>
    /// Facade Pattern - Handle complete coin collection process
    /// Coordinates: Sound, UI, Events, Scoring
    /// </summary>
    public static void CollectCoin(Vector3 collectionPosition)
    {
        // Facade Pattern - Single method coordinates multiple systems
        SoundManager.PlaySound("CollectCoinSound");
        
        if (Instance.coinSystem != null)
        {
            Instance.coinSystem.AddCoin();
            GameEventManager.TriggerCoinCollected(Instance.coinSystem.coinAmount, collectionPosition);
        }
        
        Debug.Log($"[Facade] Coin collection completed at {collectionPosition}");
    }
    
    /// <summary>
    /// Facade Pattern - Handle complete player damage process
    /// Coordinates: Life System, Sound, Respawn, Events
    /// </summary>
    public static void PlayerTakeDamage(Vector3 damagePosition)
    {
        // Facade Pattern - Coordinates complex damage response across multiple systems
        SoundManager.PlaySound("DeathSound");
        
        if (Instance.lifeSystem != null)
        {
            Instance.lifeSystem.TakeDamage();
            
            // If player still alive, respawn at checkpoint
            if (Instance.lifeSystem.lifes > 0 && Instance.playerMovement != null)
            {
                Instance.playerMovement.transform.position = Instance.playerMovement.checkPointPos;
            }
        }
        
        GameEventManager.TriggerPlayerDamaged(Instance.lifeSystem ? Instance.lifeSystem.lifes : 0);
        Debug.Log($"[Facade] Player damage processed at {damagePosition}");
    }
    
    /// <summary>
    /// Facade Pattern - Handle complete healing process
    /// Coordinates: Life System, Sound, UI, Events
    /// </summary>
    public static void PlayerHeal(Vector3 healPosition)
    {
        // Facade Pattern - Simplifies healing across multiple systems
        SoundManager.PlaySound("CollectCoinSound");
        
        if (Instance.lifeSystem != null)
        {
            Instance.lifeSystem.AddLife();
            GameEventManager.TriggerPlayerHealed(Instance.lifeSystem.lifes);
        }
        
        Debug.Log($"[Facade] Player healing completed at {healPosition}");
    }
    
    /// <summary>
    /// Facade Pattern - Handle complete checkpoint activation
    /// Coordinates: Player Position, Sound, Visual Feedback, Events
    /// </summary>
    public static void ActivateCheckpoint(Vector3 checkpointPosition, SpriteRenderer checkpointRenderer = null)
    {
        // Facade Pattern - Coordinates checkpoint system across multiple components
        SoundManager.PlaySound("CheckpointSound");
        
        if (Instance.playerMovement != null)
        {
            Instance.playerMovement.checkPointPos = checkpointPosition;
        }
        
        // Visual feedback
        if (checkpointRenderer != null)
        {
            checkpointRenderer.color = new Color(0, 127, 0); // Green = activated
        }
        
        GameEventManager.TriggerCheckpointActivated(checkpointPosition);
        Debug.Log($"[Facade] Checkpoint activated at {checkpointPosition}");
    }
    
    /// <summary>
    /// Facade Pattern - Handle complete level completion process
    /// Coordinates: Data saving, Events, Scene transition, Sound
    /// </summary>
    public static void CompleteLevel(int levelIndex, Text timeText, Text coinText)
    {
        // Facade Pattern - Orchestrates complex level completion across multiple managers
        SoundManager.PlaySound("CheckpointSound"); // Level complete sound
        
        // Data management
        timeDataManager timeManager = FindObjectOfType<timeDataManager>();
        coinDataManager coinManager = FindObjectOfType<coinDataManager>();
        
        if (timeManager != null && timeText != null)
        {
            timeManager.AddTimeData(timeText, levelIndex);
        }
        
        if (coinManager != null && coinText != null)
        {
            coinManager.AddCoinData(coinText, levelIndex);
        }
        
        // Parse completion data for events
        float completionTime = 0f;
        int totalCoins = 0;
        
        if (timeText != null)
        {
            string timeString = timeText.text.Replace(":", "").Replace(".", "");
            float.TryParse(timeString, out completionTime);
        }
        
        if (coinText != null)
        {
            int.TryParse(coinText.text, out totalCoins);
        }
        
        // Trigger completion events
        GameEventManager.TriggerLevelCompleted(levelIndex, completionTime, totalCoins);
        
        Debug.Log($"[Facade] Level {levelIndex} completion processed - Time: {completionTime}, Coins: {totalCoins}");
        
        // Scene transition
        SceneManager.LoadScene(levelIndex + 1);
    }
    
    /// <summary>
    /// Facade Pattern - Handle game restart process
    /// Coordinates: Scene reload, Audio cleanup, Data reset
    /// </summary>
    public static void RestartGame()
    {
        // Facade Pattern - Simplifies complex restart process
        Debug.Log("[Facade] Game restart initiated");
        
        // Cleanup
        GameObject bgAudio = GameObject.FindGameObjectWithTag("Music");
        if (bgAudio != null)
        {
            Destroy(bgAudio);
        }
        
        // Event cleanup
        GameEventManager.UnsubscribeAll();
        
        // Reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    /// <summary>
    /// Facade Pattern - Get complete game state information
    /// Coordinates: Multiple system states into single data structure
    /// </summary>
    public static GameStateInfo GetGameState()
    {
        // Facade Pattern - Aggregates complex state from multiple systems
        GameStateInfo state = new GameStateInfo();
        
        if (Instance.coinSystem != null)
        {
            state.coins = Instance.coinSystem.coinAmount;
        }
        
        if (Instance.lifeSystem != null)
        {
            state.lives = Instance.lifeSystem.lifes;
            state.isDead = Instance.lifeSystem.isDead;
        }
        
        if (Instance.playerMovement != null)
        {
            state.playerPosition = Instance.playerMovement.transform.position;
            state.checkpointPosition = Instance.playerMovement.checkPointPos;
        }
        
        state.currentLevel = SceneManager.GetActiveScene().buildIndex;
        
        Debug.Log($"[Facade] Game state retrieved - Lives: {state.lives}, Coins: {state.coins}, Level: {state.currentLevel}");
        return state;
    }
    
    /// <summary>
    /// Facade Pattern - Handle complete sound request with fallback
    /// Coordinates: Sound system with error handling and logging
    /// </summary>
    public static void PlaySoundSafely(string soundName, Vector3? position = null)
    {
        // Facade Pattern - Provides safe sound playing with error handling
        try
        {
            SoundManager.PlaySound(soundName);
            GameEventManager.TriggerSoundRequested(soundName);
            
            string positionText = position.HasValue ? $" at {position.Value}" : "";
            Debug.Log($"[Facade] Sound '{soundName}' played safely{positionText}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[Facade] Failed to play sound '{soundName}': {e.Message}");
        }
    }
}

// Facade Pattern - Data structure for aggregated game state
[System.Serializable]
public class GameStateInfo
{
    public int coins;
    public int lives;
    public bool isDead;
    public Vector3 playerPosition;
    public Vector3 checkpointPosition;
    public int currentLevel;
    
    public override string ToString()
    {
        return $"GameState[Lives:{lives}, Coins:{coins}, Dead:{isDead}, Level:{currentLevel}]";
    }
} 