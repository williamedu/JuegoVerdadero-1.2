using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // VARIABLES PARA MOVIMIENTO Y DIRRECION DEL ENEMIGO
    public float cronometro;
    public float speed_walk;
    public float speed_run;
    public int direccion;
    public int rutina;
    // VARIABLES QUE DETERMINAN SI ENEMIGO ESTA ATACANDO EL TARGET (JUGADOR) Y ANIMATOR
    public GameObject target;
    public bool atacando;
    public Animator ani;
    // VARIABLES QUE DETERMIANDN RANGO DE VISION Y DE ATAQUE DEL ENEMIGO
    public float rango_vision;
    public float rango_ataque;
    public GameObject rango; // GAMEOBJECT DONDE VA EL RANGO DEL ENEMIGO EN UN BOX COLLIDER(TIENE SU PROPIO SCRIPT)
    public GameObject Hit; // / GAMEOBJECT DONDE VA EL HITBOX DEL ENEMIGO EN UN BOX COLLIDER(TIENE SU PROPIO SCRIPT)

    void Start()
    {
        
        ani = GetComponent<Animator>(); //PARA REFERENCIA DEL ANIMATOR
        target = GameObject.Find("MainCharacter"); //PARA DEFINIR TARGET AL COMIENZO DEL JUEGO
    }
       

    void Update()
    {
        Comportamientos(); // UPDATE DEL MOVIMIENTO DEL ENEMIGO Y RUTINA
    }
   
    public void Comportamientos() // ESTE SCRIP DETERMINA EL MOVIMEINTO DEL ENEMIGO DICE SI EL ENEMIGO SE MUEVE (ALEATORIAMNETE)
        // A LA DERECHA, IZQUIERDA O SE QUEDA PARADO, TAMBIEN DEFINE EL COMPORTAMIENTO DE CUANDO EL JUGADOR ENTRA EN CONTACTO CON EL 
        // RANGO DE ATAQUE Y RANGO DE VISION
    {
        // si no esta en tu campo de vision
        if (Mathf.Abs(transform.position.x - target.transform.position.x) > rango_vision && !atacando)
        {
            ani.SetBool("run", false);
            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }
            switch (rutina)
            {
                case 0:
                    ani.SetBool("walk", false);
                    break;

                case 1:
                    direccion = Random.Range(0, 2);
                    rutina++;
                    break;

                case 2:

                    switch (direccion)
                    {
                        case 0:
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                            transform.Translate(Vector3.right * speed_walk * Time.deltaTime);
                            break;

                        case 1:
                            transform.rotation = Quaternion.Euler(0, 180, 0);
                            transform.Translate(Vector3.right * speed_walk * Time.deltaTime);
                            break;
                    }
                    ani.SetBool("walk", true);
                    break;
            }
        }
        else
        {
            // si esta en tu campo de vision
            if (Mathf.Abs(transform.position.x - target.transform.position.x) > rango_ataque && !atacando)
            {
                if (transform.position.x < target.transform.position.x)
                {
                    ani.SetBool("walk", false);
                    ani.SetBool("run", true);
                    transform.Translate(Vector3.right * speed_run * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    ani.SetBool("attack", false);
                }
                else
                {
                    ani.SetBool("walk", false);
                    ani.SetBool("run", true);
                    transform.Translate(Vector3.right * speed_run * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    ani.SetBool("attack", false);
                }
            }
            else
            {
                if (!atacando)
                {
                    if (transform.position.x < target.transform.position.x)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                    ani.SetBool("walk", false);
                    ani.SetBool("run", false);
                }
            }
        }
    }

    public void Final_Ani() //ESTA FUNCION DICTAMINA EL FINAL DEL ATAQUE DEL ENEMIGO EL CUAL VA AL FINAL DEL FRAME
    {
        ani.SetBool("attack", false);
        atacando = false;
        rango.GetComponent<BoxCollider2D>().enabled = true;
    }
    public void ColliderWeaponTrue() // EST AFUNCION VA AL INICIAR EL FRAME DE ATAQUE EL CUAL ACTIVA EL BOX COLLLIDER DAÒANDO AL JUGADOR
    {
        Hit.GetComponent<BoxCollider2D>().enabled = true;
    }
    public void ColliderWeaponFalse() // ESTA FUNCION VA AL FINAL DE LA ANIMACION DE ATAQUE EL CUAL DESACTIVA EL BOX COLLIDER DEL ENEMY HIT 
    {
        Hit.GetComponent<BoxCollider2D>().enabled = false;
    }

    
}
