using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    //healt
    public int maxHealth;
    int currentHealth;

    public Animator ani;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void Die()
    {
        Debug.Log("enemy died");
        //die animation
        ani.SetBool("isDead", true);
        //disable enemy
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //hurt animation
        ani.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            setCollidersStatus(true);
            Die();
        }
    }

    public void setCollidersStatus (bool active)
    {
        foreach (Collider2D c in GetComponentsInChildren<Collider2D>())
        {
            c.enabled = false;
        }
    }
    
}
