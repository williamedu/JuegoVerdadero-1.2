using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class enemyHealth : MonoBehaviour
{
    //aqui nuevo codigo
    
    public Image frontHealthBar;
    public Image backHealthBar;

    public float health;
    private float lerpTimer;
    public float maxHealth;
    public float chipSpeed = 2f;

    [SerializeField]
    private TextMeshProUGUI healthText;

    //para animaciones

    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        health = maxHealth;
    }

    public void Die()
    {
        Debug.Log("enemy died");
        anim.SetBool("isDead", true);  //die animation
        GetComponent<Collider2D>().enabled = false; //disable enemy
        //this.enabled = false;
    }
    public void hurtAnimation()
    {
        anim.SetTrigger("Hurt"); //hurt animation
    }
    public void TakeDamage(int damage)
    {
        hurtAnimation();
        health -= damage;
        
        
        if (health <= 0)
        {
            Die();
            setCollidersStatus(true);
            
        }
    }

   

    public void setCollidersStatus (bool active)
    {
        foreach (Collider2D c in GetComponentsInChildren<Collider2D>())
        {
            c.enabled = false;
        }
    }
    //aqui nuevo codigo

    


    // Start is called before the first frame update
   // void Start()
   // {
      //  health = maxHealth;


   // }

    // Update is called once per frame
    void Update()
    {
        healthText.text = health.ToString();
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();


        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        // TakeDamage(Random.Range(5, 10));
       // }

        //if (Input.GetKeyDown(KeyCode.F))
        // {
        // RestoreHealth(Random.Range(5, 10));
        //}
    }

    public void UpdateHealthUI()
    {
        //Debug.Log(health);
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.yellow;
            lerpTimer += Time.deltaTime;
            float percetcomplete = lerpTimer / chipSpeed;
            percetcomplete = percetcomplete * percetcomplete;

            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percetcomplete);
        }
        if (fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;

            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
            ;
        }

    }

  
    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
        //healthText.text = maxHealth.ToString();

    }

}

