using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyGloveController : MonoBehaviour
{
    private CharacterController characterController;
    private bool isSticking = false;
    private bool isColliding = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Wall"))
        {
            isColliding = true;
            // Perform actions when the player collides with a wall
            Debug.Log("Player has hit a wall");
        }

            
            
        
        
    }


    void Update()
    {
        // Toggle sticking state when the "E" key is pressed
            if (Input.GetKeyDown(KeyCode.E))
            {
                    // Disable CharacterController when sticking to the wall
                    // characterController.enabled = false;
                    Debug.Log("Player is sticking");
                    isSticking = true;

            }
    
            if (Input.GetKeyUp(KeyCode.E)){
            // Enable CharacterController when no longer sticking
            // characterController.enabled = true;
            isSticking = false;
            Debug.Log("Player is released");
            }
            
            if(isSticking){
                characterController.enabled = false;
            }
            else{
                characterController.enabled = true;
            }
        // Other player movement logic...
    }

    // // Update is called once per frame
    // void Update()
    // {
    //     //is character colliding with a wall? 
    //     //while player is pressing e, stay on the wall 
    //     //if player is pressing e for 3 seconds, let go 
    //     //player must hit the ground before pressing e again 


        
    // }


}
