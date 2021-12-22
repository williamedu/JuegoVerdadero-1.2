using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vincent : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("isTalking", true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        anim.SetBool("isTalking", false);

    }
}
