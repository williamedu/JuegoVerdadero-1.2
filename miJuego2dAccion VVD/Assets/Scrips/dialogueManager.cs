using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogueManager : MonoBehaviour
{
    public Animator animator;
    public Text nameText;
    public Text dialogueText;
    public  PlayerController player;


    private Queue<string> senteces;


    // Start is called before the first frame update
    void Start()
    {
        senteces = new Queue<string>();
    }

    public void startDialogue(Dialogue dialogue)
        
    {
        animator.SetBool("isOpen", true);

        nameText.text = dialogue.name;
        senteces.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            senteces.Enqueue(sentence);
        }

        DisplayNextSentence();
        //Debug.Log("holaaaa se acabo la conversacion se puede mover elplayer ya ");

    }
    
    public void DisplayNextSentence()
    {
        if (senteces.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = senteces.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
            foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    public void EndDialogue()
    {

        player.activatePlayerMovement();
        animator.SetBool("isOpen", false);
        Debug.Log("se acabo la conversacion");
    }
}
