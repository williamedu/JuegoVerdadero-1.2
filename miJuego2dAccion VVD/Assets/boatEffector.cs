using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatEffector : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("MainCharacter"))
        {
            Debug.Log("entro el palyer");
            GetComponent<AreaEffector2D>().enabled = true;
            player.GetComponent<PlayerController>().canMove = false;
            player.GetComponent<PlayerController>().canJump = false;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("MainCharacter"))
        {
            Debug.Log("salio el palyer");
            GetComponent<AreaEffector2D>().enabled = false;
            player.GetComponent<PlayerController>().canMove = true;
            player.GetComponent<PlayerController>().canJump = true;
        }
    }
}
