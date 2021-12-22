using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueTrigger : MonoBehaviour
{
    public PlayerController player;
    public Dialogue dialogue;
    public bool playerInteraction;
    public bool isCOllisioningWithDialogueTrigger;
    public void TriggerDialogue()
    {
        FindObjectOfType<dialogueManager>().startDialogue(dialogue);
    }

    public void activateInteractBar()
    {
        FindObjectOfType<interactBar>().interactOpen();
    }

    public void desactivateInteractBar()
    {
        FindObjectOfType<interactBar>().interactClose();

    }

    private void Update()
    {
        if (player.readyToInteract == true && player.interact == true)
        {
            desactivateInteractBar();
            TriggerDialogue();
            player.disablePlayerMovement();
            

        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isCOllisioningWithDialogueTrigger = true;
        }
        else
        {
            isCOllisioningWithDialogueTrigger = false;
        }
        // if (player.interact == true)
        //{

        //desactivateInteractBar();

        // TriggerDialogue();
        //  }

        

        
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            activateInteractBar();
            player.readyToInteract = true;
            //TriggerDialogue();
        }
    }
     private void OnTriggerExit2D(Collider2D collision)
   {
     if (collision.CompareTag("Player"))
     {
            player.readyToInteract = false;
        desactivateInteractBar();


     }


     }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        Debug.Log("se desactivo interact");

    }

    public void notInteracting()
    {
        Debug.Log("se desactivo interact");
        player.interact = false;
    }
    // private void Update()
    // {
    // if (player.interact == true)
    // {
    //desactivateInteractBar();
    // TriggerDialogue();
    //}
    //}
}
