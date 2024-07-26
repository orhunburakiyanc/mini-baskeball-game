using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBoundaries : MonoBehaviour
{

    private float boundX = 8.0f;
    private float boundZ = 8.0f;
    private Rigidbody ballRb;
    // Start is called before the first frame update
    void Start()
    {
        ballRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > 10.38f || transform.position.x < -10.38f || 
           transform.position.z < -10.38f ||  transform.position.z > 10.38f){
        
        ballRb.velocity = Vector3.zero;
        ballRb.angularVelocity = Vector3.zero;
        gameObject.transform.position = SetBallPosition();
       
        }
    }

    private Vector3 SetBallPosition(){
        return new Vector3(Random.Range(boundX,-boundX),0.225f,Random.Range(boundZ,-boundZ));
    } 
}
