using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




public class PlayerHealth : MonoBehaviour
{
   public Enemy enemy;
   public static PlayerHealth instance;
   public Image frontHealthBar;
   public Image backHealthBar;

    private float health;
    private float lerpTimer;
    public float maxHealth = 100;
    public float chipSpeed = 2f;
    [SerializeField]
    private TextMeshProUGUI healthText;
    
    
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        

    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = health.ToString();


        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
           // TakeDamage(Random.Range(5, 10));
        //}

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

            backHealthBar.fillAmount = Mathf.Lerp( fillB, hFraction, percetcomplete);
        }
            if (fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;

            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
;       }

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        healthText.text = maxHealth.ToString();

    }
    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
        healthText.text = maxHealth.ToString();

    }

}

