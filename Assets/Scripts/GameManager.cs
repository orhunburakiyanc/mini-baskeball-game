using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private PlayerController playerScript;
    private float spawnInterval = 20.0f;
    private float powerupLifetime = 7.0f; // Lifetime of the power-up
    public bool gameOverBoolean = false;
    private float boundX = 8.0f;
    private float boundZ = 8.0f;
    public int score = 0;
     

    public List<GameObject> powerupObjects;
    public GameObject ballPrefab; // Reference to the ball prefab
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnPowerUp());
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        if (playerScript == null)
        {
            Debug.LogError("PlayerController script not found on Player GameObject");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnPowerUp(){
        while(!gameOverBoolean){
          
            yield return new WaitForSeconds(spawnInterval);
            
            if (gameOverBoolean) break; // Additional check before instantiation
            if (playerScript.isOnDrug) continue; // 

            int index = Random.Range(0,powerupObjects.Count);
            
            GameObject obj= Instantiate(powerupObjects[index],SetPowerupPosition(),powerupObjects[index].transform.rotation);

            StartCoroutine(DestroyAfterTime(obj, powerupLifetime));
            
        }
       
    }
    IEnumerator DestroyAfterTime(GameObject powerup, float powerupLifetime ){
        Debug.Log("Entered DestroyAfterTime");
        yield return new WaitForSeconds(powerupLifetime);
        if (powerup != null)
        {
            Debug.Log("The object is destroyed!");
            Destroy(powerup);
        }

    }

    public void PowerUpY(){
        playerScript.playerSpeed = 65.0f;
        Debug.Log("(PowerUpY) Player's movement speed: " + playerScript.playerSpeed);
        StartCoroutine(DrugEffect());
        playerScript.playerSpeed = 5.0f;
       
        
    }
    IEnumerator DrugEffect(){
        yield return new WaitForSeconds(15);
        playerScript.isOnDrug = false; // Reset isOnDrug flag
        Debug.Log("(Drug Effect End) Player's movement speed: " + playerScript.playerSpeed);
        
    }
    public void PowerUpB()
    {
        Debug.Log("PowerUpB: Instantiating 5 balls.");
        for (int i = 0; i < 5; i++)
        {
            Vector3 ballPosition = new Vector3(Random.Range(-boundX, boundX), 0.225f, Random.Range(-boundZ, boundZ));
            Instantiate(ballPrefab, ballPosition, Quaternion.identity);
        }
    }

    public void PowerUpP()
    {
        Debug.Log("PowerUpP: Doubling the score for each basket.");
        playerScript.isDoubleScoreActive = true;
        StartCoroutine(DisableDoubleScore());
    }

    IEnumerator DisableDoubleScore()
    {
        yield return new WaitForSeconds(10); // Duration for double score effect
        playerScript.isDoubleScoreActive = false;
        Debug.Log("(PowerUpP) Double score effect ended.");
    }
    private Vector3 SetPowerupPosition(){
        return new Vector3(Random.Range(boundX,-boundX),0.225f,Random.Range(boundZ,-boundZ));
    } 

   
}
