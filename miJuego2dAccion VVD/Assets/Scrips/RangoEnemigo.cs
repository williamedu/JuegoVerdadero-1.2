using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangoEnemigo : MonoBehaviour
{

    public Animator ani; // REFERENCIA AL ANIMATOR
    public Enemy enemigo; // REFERENCIA AL ENEMYSCRIPT

    private void OnTriggerEnter2D(Collider2D collision) // DEFINE SI RANGO DE ATAQUE COLISIONA CON JUGADOR EMPIEZE A ATACAR Y SE DESCATIVE RANGO DE ATAQUE
    {
        if (collision.CompareTag("Player"))
        {
            ani.SetBool("walk", false);
            ani.SetBool("run", false);
            ani.SetBool("attack", true);
            enemigo.atacando = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
