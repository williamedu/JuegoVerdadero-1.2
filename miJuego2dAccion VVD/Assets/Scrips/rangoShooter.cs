using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangoShooter : MonoBehaviour
{
       
    public Animator ani;
    public shooterEnemy shooter;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ani.SetBool("walk", false);
            ani.SetBool("run", false);
            ani.SetBool("attack", true);
            shooter.atacando = true;
            GetComponent<BoxCollider2D>().enabled = false;
            //Invoke("playAttackAnim", 0.25f);

            
                  
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //shooter.atacando = false;
            //ani.SetBool("attack", false);
        }
        
    }
    void playAttackAnim()
    {
        ani.SetBool("waiting", true);
    }
    

    
    
}