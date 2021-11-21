using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    private Animator m_Animator;
    public float jumPadForce = 15f;
    private Rigidbody2D rb;




   


    private void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.instance.jumPadForce();
            GetComponent<Animator>().SetTrigger("Jump");
            //m_Animator.SetTrigger("jumpPadAnimation");
        }

    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            GetComponent<Animator>().SetBool("Idle",true);

            //m_Animator.SetTrigger("jumpPadAnimation");
        }
    }

    //m_Animator.SetTrigger("jumpPadAnimation");
    //Debug.Log("coño");
    // if (collision.gameObject.CompareTag("Player"))





}
