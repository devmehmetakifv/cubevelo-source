using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectible
{
    public CoinSystem cs;
    
    // Factory Pattern - ICollectible implementation
    public CollectibleType GetCollectibleType()
    {
        return CollectibleType.Coin;
    }
    
    public void OnCollected(GameObject collector)
    {
        // Factory Pattern - Standardized collection behavior
        if (collector.CompareTag("Player"))
        {
            // Original logic preserved
            SoundManager.PlaySound("CollectCoinSound");
            cs.AddCoin();
            
            // Observer Pattern - Notify about coin collection
            GameEventManager.TriggerCoinCollected(cs.coinAmount, transform.position);
            
            Destroy(gameObject);
        }
    }
    
    public int GetValue()
    {
        return 1; // Coin value
    }
    
    public string GetDescription()
    {
        return "Increases score";
    }
    
    // Keep original method for backward compatibility
    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.tag == "Player"){
            // Use the Factory Pattern interface method
            OnCollected(col.gameObject);
        }
    }
}
