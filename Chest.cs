using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, ICollectible
{
    public SpriteRenderer chestSpriteRenderer;
    public Sprite heartSprite;
    public LifeSystem ls;
    private bool hasBeenOpened = false;
    
    // Factory Pattern - ICollectible implementation
    public CollectibleType GetCollectibleType()
    {
        return CollectibleType.Heart;
    }
    
    public void OnCollected(GameObject collector)
    {
        // Factory Pattern - Standardized collection behavior
        if (collector.CompareTag("Player") && !hasBeenOpened)
        {
            // Original exit logic preserved
            gameObject.SetActive(false);
            ls.AddLife();
            SoundManager.PlaySound("CollectCoinSound");
            Debug.Log("Heart collected!");
            
            // Observer Pattern - Notify about heart collection
            GameEventManager.TriggerPlayerHealed(ls.lifes);
            
            hasBeenOpened = true;
        }
    }
    
    public int GetValue()
    {
        return 1; // Heart value (1 life)
    }
    
    public string GetDescription()
    {
        return "Restores one life";
    }
    
    void Start(){
        gameObject.SetActive(true);
    }
    
    // Keep original methods for backward compatibility
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
       {
            // Original logic preserved
            SoundManager.PlaySound("OpenChestSound");
            chestSpriteRenderer.sprite = heartSprite;
        }
    }
    
    void OnTriggerExit2D(Collider2D col){
        if (col.gameObject.CompareTag("Player")){
            // Use the Factory Pattern interface method
            OnCollected(col.gameObject);
        }
    }
}
