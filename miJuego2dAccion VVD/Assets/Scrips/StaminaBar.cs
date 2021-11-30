using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public GameObject noEnergy;
    public Slider staminaBar;
    private int maxStamina = 50;
    private int currentStamina;
    public static StaminaBar instance;
    
    private Animator anim;
    public PrototypeHero canAttack;
    public PrototypeHero notEnoughEnergy;

    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine regen;
    
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
        
    }
    public void UseStamina (int amount)
    {
        if( currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            staminaBar.value = currentStamina;

            if (regen != null)
                StopCoroutine(regen);
            
            regen = StartCoroutine(RegenStamina());
            

        }
        else
        {
            
            Debug.Log("no enought staminas");
        }
    }
        private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(1);
      
        while (currentStamina < maxStamina)
        {
            currentStamina += maxStamina / 20;
            staminaBar.value = currentStamina;
            yield return regenTick;
        }
        regen = null;

    }

    private void Update()
    {
        if (currentStamina >= 20)
        {
            GetComponent<Animator>().SetBool("BarEffect", false);
            noEnergy.SetActive(false);
            //attackEnable();
            //GetComponent<Animator>().SetBool("NormalEnergy", true);
            //GetComponent<Animator>().SetBool("BarEffect", false);
            GameObject.Find("MainCharacter").GetComponent<PlayerController>().canAttackAnim = true;
            //GameObject.Find("noEnergy").GetComponent<noEnergy>().


        }
        else
        {
            Debug.Log("tienes menos de 20 ");
            
            //GetComponent<Animator>().SetBool("NormalEnergy", false);
             GetComponent<Animator>().SetBool("BarEffect", true);
            GameObject.Find("MainCharacter").GetComponent<PlayerController>().canAttackAnim = false;
            
            //attackDisable();
            //GameObject.Find("Sprite").GetComponent<PlayerCombat>().enabled = true;

        }
        
    }


    //public void attackEnable()
   // {
       // GameObject.Find("Sprite").GetComponent<PlayerCombat>().enabled = true;
   // }
   // public void attackDisable()
   // {      
            
       // GameObject.Find("Sprite").GetComponent<PlayerCombat>().enabled = false;                                   
    //}
    

}
