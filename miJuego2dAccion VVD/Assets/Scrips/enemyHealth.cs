using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class enemyHealth : MonoBehaviour
{
    public bool lifeIsLessThan100; // BOOL QUE DETERMINA SI EL ENEMIGO YA FUE ATACADO, NECESARIA PARA PARA HACER APARECER EL HEALTHBAR (SI FUE ATACADO)
    
    public Animator anim; // REFERENCIA ANIMATOR
    public GameObject enemyHealthGameObject; // ESTE GAMEOBJECT ESTA APARTE EN EL HEALTH MANAGER DEL ENEMIGO MANEJA LA VIDA Y DEMAS 
    
    public float health; // PARA HEALTH
    public float maxHealth; // PARA MAX HEALTH
    public Image backHealthBar; // PARA AÒADIR BACK HEALTHB AR
    public Image frontHealthBar; // PARA AÒADIR FRONT HEALT BAR
    private float lerpTimer; // NECESARIO PARA EL CODIGO DE BACK HEALTH DECREASE
    public float chipSpeed = 2f; // VELOCIDAD CON LA QUE BAJA EL BACK HEALTH DAMAGE
    [SerializeField]
    private TextMeshProUGUI healthText; // REFERENCIA DEL TEXT (NUMERO DE VIDA DEL ENEMIGO

    private void Start()
    {
        
        anim = GetComponent<Animator>(); // ACTIVAR EL ANIMATOR AL COMIENZO DEL JUEGO
        lifeIsLessThan100 = false; // AL EMPEZAR SE NECEITA QUE SEA FALSO PARA QUE LA VIDA NO SE MUESTRE MAS ADELANTE
        health = maxHealth; // DEFINIMOS HEALTH COMO BACK HEALTH 
    }

    public void Die() // FUNCION PARA MUERTE DEL ENEMIGO
    {
        GetComponent<Collider2D>().enabled = false; //disable enemy
        anim.SetBool("isDead", true);  //die animation
    }
    public void hurtAnimation() // PARA LA ANIMACION DE HURT ENEMY
    {
        anim.SetTrigger("Hurt");
    }
    public void TakeDamage(int damage) // FUNCION PARA DAÒAR AL ENEMIGO
    {
        lifeIsLessThan100 = true;
        health -= damage;
        hurtAnimation();
        
        
        if (health <= 0) // SI LA VIDA ES MENOR O IGUAL A 0 ENTONCES EL ENEMIGO MUERE
        {      
            Die();
            setCollidersStatus(true);
            
        }
    }
    public void setCollidersStatus (bool active) // PARA DESACTIVAR TODOS LOS COLLIDERS DEL ENEMIGO 
    {
        foreach (Collider2D c in GetComponentsInChildren<Collider2D>())
        {
            c.enabled = false;
        }
    }

    void Update()
    {
        heathActive(); // HEALTHaCTIVE SE LLAMA EN UPDATE

        // ESTAS 2 FUNCIONES HACEN QUE EL TEXT SE CONVIERTA A NUMERO PARA SER VISTO POR LA UI COMO NUMERO
        healthText.text = health.ToString();
        health = Mathf.Clamp(health, 0, maxHealth);

        UpdateHealthUI(); // SE LLAMA EN UPDATE

    }

    public void UpdateHealthUI() // DEFINE EL COMPORTAMIENTO DE LA VIDA DEL ENEMIGO AL SER ATACADA
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
    public void heathActive() // FUNCION PARA HACER APARECER LA VIDA SI ESTA DAÒADO EL ENEMIGO
    {
        if (lifeIsLessThan100 == true)
        {
            enemyHealthGameObject.SetActive(true);
            //Debug.Log("tiene menos de 100 de vida");
        }

        if (health <= 0)
        {
            enemyHealthGameObject.SetActive(false);
        }
    }
    public void RestoreHealth(float healAmount) // RESTAURAR VIDA DE ENEMIGO, NO COMTEMPLADA POR AHORA 
    {
        health += healAmount;
        lerpTimer = 0f;       
    }

}

