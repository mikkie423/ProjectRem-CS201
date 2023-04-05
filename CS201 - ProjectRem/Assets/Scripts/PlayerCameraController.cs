using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{

    public float sensX;
    public float sensY;

    public Transform orientation;

    public float xRotation;
    public float yRotation;


    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        xRotation += mouseX;

        yRotation -= mouseY;

        //rotate cam and orientation
        transform.localRotation = Quaternion.Euler(yRotation, xRotation, 0);
        orientation.rotation = Quaternion.Euler(yRotation, xRotation, 0);

    }
}
