using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemigo : MonoBehaviour
{
    public PlayerHealth health; // REFERENCIA AL PLAYER HEALTH PARA HACER DAÒO
    public PlayerController player; // REFERENCIA AL PLAYER CONTROLLER PARA CONTROLAR ANIMACIONES CON DAÒO
    private void OnTriggerEnter2D(Collider2D collision) // SI COLLISIONA ACTIVA HURT ANIMATION DEL PLAYER Y DAÒO ALEATORIO 
    {
        if (collision.CompareTag("Player"))
        {
            player.hurtAnimation();
            health.TakeDamage(Random.Range(8, 15));
            
        }
    }

    
}
