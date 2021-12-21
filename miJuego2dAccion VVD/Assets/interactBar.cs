using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactBar : MonoBehaviour
{

    public Animator animator;
   public void interactOpen()
    {
        animator.SetBool("interact", true);
    }

    public void interactClose()
    {
        animator.SetBool("interact", false);

    }
}
