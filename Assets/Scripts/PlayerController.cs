using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float mouseX;
    private float mouseY;
    [SerializeField] private float shotPower = 14.0f;
    public float playerSpeed = 5.0f;
    [SerializeField] private float mouseSensitivity;  // Adjust the sensitivity of the mouse
    private Camera mainCamera;
    public bool isOnDrug = false;
    public bool isShot = false; 
    public bool isDoubleScoreActive = false;
    private GameObject ball;
    private Rigidbody ballRb;
    private Vector3 ballOffset = new Vector3(0.09f,-0.018f,1.76f);
   
    // Start is called before the first frame update
    void Start()
    {
        /*When you use Camera.main, Unity performs a lookup to find the camera with the "MainCamera" tag. It returns 
        a reference to that camera, allowing you to easily access and manipulate it.*/
        mainCamera =  Camera.main;
        Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor in the center of the screen
        ball = GameObject.Find("Ball");
        ballRb = ball.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerRotation();
        BallPosition();

        if (Input.GetMouseButtonDown(0))
        {
            ShootBall();
        }
    }

    private void PlayerMovement(){
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * verticalInput * playerSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * horizontalInput * playerSpeed * Time.deltaTime);
    }
    private void PlayerRotation(){
       
        mouseX += Input.GetAxis("Mouse X");
        mouseY += Input.GetAxis("Mouse Y");

        //Prevent the rotation of the camera on Y axis. 
        mouseY = Math.Clamp(mouseY, -35.0f, 55.0f);
       
        mainCamera.transform.localRotation = Quaternion.Euler(-mouseY,0,0);
        transform.localRotation = Quaternion.Euler(0,mouseX,0);
    }

    //To Stabilize the ball in front of the player.
    public void BallPosition(){
        if (!isShot){  
           ballRb.velocity = Vector3.zero;
           ballRb.angularVelocity = Vector3.zero;
           ball.transform.position = mainCamera.transform.position + mainCamera.transform.forward * ballOffset.z +
                                      mainCamera.transform.right * ballOffset.x + mainCamera.transform.up * ballOffset.y;          
        }
    }

    //Add force to take a shot.
    private void ShootBall()
    {
        if (!isShot)
        {
            isShot = true;
            ballRb.AddForce(mainCamera.transform.forward * shotPower, ForceMode.Impulse);
        }
    }
}
