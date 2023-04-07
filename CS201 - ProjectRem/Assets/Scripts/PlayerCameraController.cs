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

    public float maxYAngle;
    public float maxXAngle;



    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private void Start()
    {
        yRotation = 0;
        xRotation = 0;
    }
    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.unscaledDeltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.unscaledDeltaTime * sensY;

       

        xRotation += mouseX;

        yRotation -= mouseY;

        yRotation = Mathf.Clamp(yRotation, -maxYAngle, maxYAngle);

        //rotate cam and orientation
        transform.localRotation = Quaternion.Euler(yRotation, xRotation, 0);
        orientation.rotation = Quaternion.Euler(yRotation, xRotation, 0);

    }
}
