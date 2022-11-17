using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class callAnimation : MonoBehaviour
{
    //call for animator
    Animator animator;
    int isWalkingHash;
    bool forwardPressed;
   
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Debug.Log(animator);
        isWalkingHash = Animator.StringToHash("isWalking");
        
    }    

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool("isRunning");

        if (GameObject.Find("Manager").GetComponent<InventoryManager>().bagEnabled == false)
        {
            if (Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f)
            {
                 forwardPressed = true;
            
            } 
        
            else{
                 forwardPressed = false;
            

            }

            if (!isWalking && forwardPressed)
            {
                animator.SetBool(isWalkingHash, true);
            }

            if (isWalking && !forwardPressed)
            {
                animator.SetBool(isWalkingHash, false);

            }

            if (Input.GetKey("space")  && !forwardPressed)
            {
                animator.SetBool("isJumping", true);

            }

            if (!Input.GetKey("space") && !forwardPressed)
            {
                animator.SetBool("isJumping", false);

            }


            if ((Input.GetKey("space") && forwardPressed) && !isRunning)
            {
                animator.SetBool("isRunning", true);

            }

            if ((!Input.GetKey("space") || !forwardPressed) && isRunning)
            {
                animator.SetBool("isRunning", false);

            }

        }

        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isJumping", false);
        }

        

    }
    
}
