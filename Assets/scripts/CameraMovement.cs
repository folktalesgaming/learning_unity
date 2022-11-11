using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform player;

    private Vector3 tempCameraPosition;

    [SerializeField]
    private float minX, maxX;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Hero").transform;    
    }

    // Late Update is called after all calculation is done for update
    void LateUpdate()
    {
        tempCameraPosition = transform.position;
        tempCameraPosition.x = player.position.x;

        if (tempCameraPosition.x < minX)
            tempCameraPosition.x = minX;
        if (tempCameraPosition.x > maxX)
            tempCameraPosition.x = maxX;

        transform.position = tempCameraPosition;
    }
}
