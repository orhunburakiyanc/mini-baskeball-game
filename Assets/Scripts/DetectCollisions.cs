using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Both attached to player(Tag: Player) and ball (Tag: Ball)
public class DetectCollisions : MonoBehaviour
{

   
    private PlayerController playerScript;
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
      
       gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
       playerScript = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter(Collision other){
        Rigidbody ballRb = GetComponent<Rigidbody>();

        if(other.gameObject.CompareTag("Ball")){
            
           
            Debug.Log("After Ball Rigidbody Angular Velocity: " + ballRb.angularVelocity);

            playerScript.isShot = false;
            playerScript.BallPosition();
        }
        if(other.gameObject.CompareTag("Fence") && gameObject.CompareTag("Ball")){
           
            ballRb.velocity = Vector3.zero;
            ballRb.angularVelocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider collider){
        if(gameObject.CompareTag("Player") && collider.gameObject.CompareTag("PowerupB")){
            playerScript.isOnDrug = true;
            Destroy(collider.gameObject);


        }
        else if(gameObject.CompareTag("Player") && collider.gameObject.CompareTag("PowerupY")){
            playerScript.isOnDrug = true;
            Debug.Log("Yellow power-up is gained.");
            Destroy(collider.gameObject);
            gameManager.PowerUpY();

        }
        else if(gameObject.CompareTag("Player") && collider.gameObject.CompareTag("PowerupP")){
            playerScript.isOnDrug = true;
            Destroy(collider.gameObject);

        }
    }
     
}
