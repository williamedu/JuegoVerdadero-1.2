using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public PlayerHealth health;
    public PlayerController player;
    public GameObject healthReduction;
    public GameObject hurtAnim;
    float moveSpeed = 7f;

    Rigidbody2D rb;

    public Transform target;
    Vector2 moveDirection;
    private void Awake()
    {   //code to set reference of damage
        healthReduction = GameObject.Find("MainCharacter");
        health = healthReduction.GetComponent<PlayerHealth>();
        //code to set reference of hurt animation
        hurtAnim = GameObject.Find("MainCharacter");
        player = hurtAnim.GetComponent<PlayerController>();
    }
    // Use this for initialization
    void Start()
    {
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
            health.TakeDamage(Random.Range(15, 25));
            player.hurtAnimation();
        }

        if (col.gameObject.name.Equals("shield"))
        {
            Debug.Log("shield!");
            player.shieldHit();
            player.useStamina();
            Destroy(gameObject);
        }
       
    }
    
}
