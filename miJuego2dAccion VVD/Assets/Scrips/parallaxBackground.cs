using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxBackground : MonoBehaviour
{
   [SerializeField] private  float parallaxEffectMultiplier;
    private Transform cameraTransfrom;
    private Vector3 previusCameraPosition;
    private void Start()
    {
        cameraTransfrom = Camera.main.transform;
        previusCameraPosition = cameraTransfrom.position;

    }

    private void LateUpdate()
    {
        float deltaX = (cameraTransfrom.position.x - previusCameraPosition.x) * 0.5f;
        transform.Translate(new Vector3(deltaX, 0, 0));
        previusCameraPosition = cameraTransfrom.position;

    }
}
