using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooterEnemy : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator ani;
    public int direccion;
    public float speed_walk;
    public float speed_run;
    public GameObject target;
    public bool atacando;

    public float rango_vision;
    public float rango_ataque;
    public GameObject rango;
    public GameObject Hit;
    public bool itsOnTheEdge;

    //healt
    //public int maxHealth = 100;
    //int currentHealth;

    // to shoot
    [SerializeField]
    GameObject bullet;
    public Transform firePoint;
    float fireRate, nextFire;





    // Start is called before the first frame update
    void Start()
    {
        //to shoot
        fireRate = 1f;
        nextFire = Time.time;

        //currentHealth = maxHealth;
        ani = GetComponent<Animator>();
        target = GameObject.Find("MainCharacter");
    }
    // Update is called once per frame

    void Update()
    {
        Comportamientos();
    }

   // public void TakeDamage(int damage)
   // {
        //currentHealth -= damage;

        //hurt animation
        //ani.SetTrigger("Hurt");

       // if (currentHealth <= 0)
        //{
           // Die();


       // }

    //}
    //public void Die()
    //{
        //Debug.Log("enemy died");
        //die animation
        //ani.SetBool("isDead", true);
        //disable enemy
        //GetComponent<Collider2D>().enabled = false;
        //this.enabled = false;
   // }
    public void Comportamientos()
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
            if (Mathf.Abs(transform.position.x - target.transform.position.x) > rango_ataque && !atacando && itsOnTheEdge == false)
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

    void checkTimeToFire()
    {
        if (Time.time > nextFire)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("turnRight"))
        {
            rango_vision = 0;
            direccion = 0;
            itsOnTheEdge = true;
            turn();
            //cancelPlayerFollow();
        }

        if (collision.CompareTag("turnLeft"))
        {
            rango_vision = 0;
            direccion = 1;
            itsOnTheEdge = true;
            turn();
            // cancelPlayerFollow();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("turnRight"))
        {

            itsOnTheEdge = false;

        }

        if (collision.CompareTag("turnLeft"))
        {

            itsOnTheEdge = false;

        }
    }

    public void cancelPlayerFollow()
    {
        if (itsOnTheEdge == true)
        {
            rango_vision = 0f;
        }
    }

    public void continuePlayerFollow()
    {
        if (itsOnTheEdge == false)
        {
            rango_vision = 5f;
        }
    }
    public void turn()
    {
        //rango_vision = 0;
        StartCoroutine("ExecuteAfterTime", 2f);
    }
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        rango_vision = 5;

    }
    //no necesario para enemigos que disparan!!!
    public void Final_Ani()
    {
        ani.SetBool("attack", false);
        atacando = false;
        rango.GetComponent<BoxCollider2D>().enabled = true;
    }
    public void ColliderWeaponTrue()
    {
        Hit.GetComponent<BoxCollider2D>().enabled = true;
    }
    public void ColliderWeaponFalse()
    {
        Hit.GetComponent<BoxCollider2D>().enabled = false;
    }


}