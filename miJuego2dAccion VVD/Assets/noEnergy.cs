using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noEnergy : MonoBehaviour
{

    public Transform objectToFollow;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = objectToFollow.position + offset;
    }

    public void NoEnergy()
    {
        //this.gameObject.SetActive(true);
    }
}
