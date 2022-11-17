using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    [SerializeField] MenuButtonController MenuButtonController;
    [SerializeField] Animator animator;
    //[SerializeField] AnimatorFunctions animatorFunctions;
    [SerializeField] int thisIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(MenuButtonController.index == thisIndex)
        {
            animator.SetBool("selected", true);
            if(Input.GetAxis("Submit") == 1)
            {
                animator.SetBool("pressed", true);
            }

            else if(animator.GetBool("pressed"))
            {
                animator.SetBool("pressed", false);
                //animatorFunctions.disableOnce = true;
            }
        }
        else
        {
            animator.SetBool("selected", false);
        }    

        if(thisIndex == 0 && Input.GetAxis("Submit") == 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
