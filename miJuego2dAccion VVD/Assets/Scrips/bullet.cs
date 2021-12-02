using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public enemyHealth enemyHealth;
    public GameObject enemyGameOb;
    public PlayerHealth playerHealth;
    public PlayerController player;
    public GameObject healthReduction;
    public GameObject hurtAnim;
    public float moveSpeed = 8f;
    public bool _returning;

    Rigidbody2D rb;

    public Transform target;
    Vector2 moveDirection;
    private void Awake()
    { // code to hurt enemies in the path of bullets if this is retuning
        enemyGameOb = GameObject.Find("barrirDisparador");
        enemyHealth = enemyGameOb.GetComponent<enemyHealth>();
        //code to set reference of damage
        healthReduction = GameObject.Find("MainCharacter");
        playerHealth = healthReduction.GetComponent<PlayerHealth>();
        //code to set reference of hurt animation
        hurtAnim = GameObject.Find("MainCharacter");
        player = hurtAnim.GetComponent<PlayerController>();
    }
    // Use this for initialization
    void Start()
    {
        _returning = false;
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, 5f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("MainCharacter"))
        {
            Debug.Log("Hit!");
            Destroy(gameObject);
            playerHealth.TakeDamage(Random.Range(15, 25));
            player.hurtAnimation();
        }

        if (col.gameObject.name.Equals("shield"))
        {
            Debug.Log("shield!");
            player.shieldHit();
            player.useStamina();
            Destroy(gameObject);
        }

        if (col.CompareTag("parry"))
        {
          
            rb.velocity = rb.velocity * -1;
            _returning = true;
            Debug.Log("se hizo un parry bacanisimo");
            //Destroy(gameObject);
           // moveDirection = moveDirection * -1;
        }

        if (_returning == true && col.CompareTag("enemy") )
        {
            enemyHealth.TakeDamage(500);
            Debug.Log("la bala choco con un enemigo");
            Destroy(gameObject);
        }
    }

    
}
