using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemigo : MonoBehaviour
{
    public PlayerHealth health;
    public PlayerController player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //player.hurtAnimation();
            //health.TakeDamage(Random.Range(8, 15));
            
        }
    }

    
}
