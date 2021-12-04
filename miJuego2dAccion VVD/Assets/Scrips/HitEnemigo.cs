using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemigo : MonoBehaviour
{
    public PlayerHealth health; // REFERENCIA AL PLAYER HEALTH PARA HACER DA�O
    public PlayerController player; // REFERENCIA AL PLAYER CONTROLLER PARA CONTROLAR ANIMACIONES CON DA�O
    private void OnTriggerEnter2D(Collider2D collision) // SI COLLISIONA ACTIVA HURT ANIMATION DEL PLAYER Y DA�O ALEATORIO 
    {
        if (collision.CompareTag("Player"))
        {
            player.hurtAnimation();
            health.TakeDamage(Random.Range(8, 15));
            
        }
    }

    
}
