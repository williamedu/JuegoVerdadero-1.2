using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueTrigger : MonoBehaviour
{
    public PlayerController player;
    public Dialogue dialogue;
    public bool playerInteraction;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            activateInteractBar();
            //TriggerDialogue();
        }

        if (player.interact == true)
        {

            desactivateInteractBar();

            TriggerDialogue();
        }

        if (collision.CompareTag("Player") && player.interact == true)
        {
            desactivateInteractBar();
            TriggerDialogue();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            desactivateInteractBar();


        }
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
