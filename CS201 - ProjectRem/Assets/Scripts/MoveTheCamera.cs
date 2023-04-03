using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTheCamera : MonoBehaviour
{
    public Transform cameraPosition;
    public Transform playerHead;

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraPosition.position;
        transform.position = playerHead.position;
    }
}
