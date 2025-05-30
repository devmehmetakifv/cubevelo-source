using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndPortalSFX : MonoBehaviour
{
    public AudioSource aSrc;
    private int sceneNumber;
    public GameObject bgAudio;
    public Text scoreText;
    public Text coinText;
    Text textData;
    Text coinData;
    public timeDataManager tDManager;
    public coinDataManager cDManager;
    void Start(){
        tDManager = FindObjectOfType<timeDataManager>();
        cDManager = FindObjectOfType<coinDataManager>();
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
    }
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.tag == "Player"){
            aSrc.Play();
            Destroy(bgAudio);
            textData = scoreText;
            coinData = coinText;
            tDManager.AddTimeData(textData,sceneNumber);
            cDManager.AddCoinData(coinData,sceneNumber);
            
            float completionTime = float.Parse(scoreText.text.Replace(":", "").Replace(".", ""));
            int totalCoins = int.Parse(coinText.text);
            GameEventManager.TriggerLevelCompleted(sceneNumber, completionTime, totalCoins);
            
            GameFacade.CompleteLevel(sceneNumber, scoreText, coinText);
            
            // Note: Scene loading moved to Facade, so commenting out to avoid double-loading
            // SceneManager.LoadScene(sceneBuildIndex:sceneNumber+1);
        }
    }
}
