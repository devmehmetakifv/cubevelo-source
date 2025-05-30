using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinSystem : MonoBehaviour
{
    public int coinAmount = 0;
    public Text coinText;
    // Start is called before the first frame update
    void Start()
    {
        // Observer Pattern - Subscribe to coin collection events
        GameEventManager.OnCoinCollected += OnCoinCollectedEvent;
    }

    void OnDestroy()
    {
        // Observer Pattern - Unsubscribe to prevent memory leaks
        GameEventManager.OnCoinCollected -= OnCoinCollectedEvent;
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = coinAmount.ToString();
    }
    public void AddCoin(){
        coinAmount++;
    }
    
    // Observer Pattern - Event handler
    private void OnCoinCollectedEvent(int totalCoins)
    {
        // Additional reactions to coin collection could go here
        Debug.Log($"[Observer] CoinSystem detected coin collection via event! Total: {totalCoins}");
        
        // Example: Could trigger achievements, sound effects, UI animations, etc.
        // This demonstrates how multiple systems can react to the same event
    }
}
