using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTest : MonoBehaviour
{
    public Animator animator;

    public KeyCode keyToPlay = KeyCode.A;
    //public KeyCode keyToExit = KeyCode.S;
    public string triggerToPlay = "flybool";
    public bool flyAnim = false;

    // Start is called before the first frame update
    private void OnValidate()
    {
        if(animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(keyToPlay))
        {
            flyAnim = !flyAnim;
            animator.SetBool(triggerToPlay, flyAnim);
        }


        // if(Input.GetKeyDown(keyToPlay))
        // {
        //     animator.SetTrigger(triggerToPlay);
        // }
        
        // if(Input.GetKeyDown(keyToPlay))
        // {
        //     animator.SetBool(triggerToPlay, true);
        // }
        // else if(Input.GetKeyDown(keyToExit))
        // {
        //     animator.SetBool(triggerToPlay, false);
        // }

        // if(flyAnim)
        // {
        //     animator.SetBool(triggerToPlay, true);
        // }
        // else if(!flyAnim)
        // {
        //     animator.SetBool(triggerToPlay, false);
        // }
    }
}
