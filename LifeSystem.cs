using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeSystem : MonoBehaviour
{
    public GameObject[] hearts;
    public int lifes = 5;
    public int extraLifes;
    private int maxLife;
    public bool isDead;
    public GameObject bgAudio;
    private void Start(){
        maxLife = lifes + extraLifes;
    }

    void Update()
    {
        if (lifes == 0){
            isDead = true;
            Debug.Log("We're dead!");
            ReloadSceneWhenLifesZero();
        }
    }
    public void TakeDamage(){
        if (lifes >= 1){
            lifes--;
            hearts[lifes].gameObject.SetActive(false);
            
            GameEventManager.TriggerPlayerDamaged(lifes);
        }
    }
    public void ReloadSceneWhenLifesZero(){
        Destroy(bgAudio);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        GameFacade.RestartGame();
    }
    public void AddLife(){
        if (lifes < maxLife){
            hearts[lifes].gameObject.SetActive(true);
            lifes += 1;
            
            GameEventManager.TriggerPlayerHealed(lifes);
            
            GameFacade.PlayerHeal(transform.position);
        }
    }
}
