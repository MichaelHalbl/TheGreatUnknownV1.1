using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_PlayerMovement : MonoBehaviour
{


    public CharacterController control;

    public float speed = 12f;

    public float gravity = -9.81f *2;
    
    public Transform groundCheck;
    public float groundDistance = 0.2f;

    public LayerMask groundMask;

    bool isGrounded;

    public float jumphight = 2f;

    bool crouched = false;
    Vector3 velocity;
 

    // Update is called once per frame
    void Update()
    {

        isGrounded= Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0){

            velocity.y = -1f;
        }

        

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x +transform.forward *z;

        control.Move(move * speed* Time.deltaTime);  


        if(Input.GetButtonDown("Jump") && isGrounded){  //jump

            velocity.y = Mathf.Sqrt(jumphight * -2 * gravity);

        }

        if(Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.C)){
        
         if(crouched){    //try to stand up

            Vector3 top = new Vector3(transform.position.x, transform.position.y+control.height, transform.position.z); 

            var canStand = Physics.Raycast(top, Vector3.up, 1f);
            Debug.Log(transform.position);
            Debug.Log(canStand);

            

            if(!canStand){
            Debug.Log("Not Crouching");
            control.height = 2f;
            control.center = new Vector3 (control.center.x, 0f, control.center.z);
            crouched = false;
            }else{

                Debug.Log("cant Stand Up");

            }


         }else{         //crouch

            Debug.Log(transform.position);

            Debug.Log("Crouching");
            control.height = 1f;
            control.center = new Vector3 (control.center.x, 0.5f, control.center.z);
            crouched = true;

         }

        }

    
        velocity.y += gravity*Time.deltaTime;

        control.Move(velocity * Time.deltaTime);
    
    }


}