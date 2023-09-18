using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;

    private int desireddLane = 1;
    public float laneDistance = 4;

    public float JumpForce;
    public AudioSource source;
    public AudioLoudnessDetection detector;
    public float loudnessSensibility = 1;
    public float threshold = 0.1f;

    public float Gravity = -20;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.z = forwardSpeed;
        
        float loudness = detector.GetLoudnessFromMicrophone() * loudnessSensibility;

        if (controller.isGrounded){
            direction.y=-1;
        if (loudness > threshold)
        //if (Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            Jump();
        }
        }else
        {
            direction.y += Gravity* Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            desireddLane++;
            if (desireddLane == 3)
                desireddLane = 2;
        }

         if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desireddLane--;
            if (desireddLane == -1)
                desireddLane = 0;
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desireddLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }else if (desireddLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        if (transform.position == targetPosition)
        return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);
    }

    private void FixedUpdate()
    {
        controller.Move(direction*Time.fixedDeltaTime);
    }

    private void Jump()
    {
        direction.y = JumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag =="Obstacle")
        {
            PlayerManager.gameOver = true;
        }
    }
}
