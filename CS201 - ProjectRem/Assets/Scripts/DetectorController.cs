using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorController : MonoBehaviour
{
    public bool itemDetected;
    public GameObject portal;
    public GameObject item;

    private void Start()
    {
        portal.SetActive(false);

    }
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("PickupItem"))
        {
            Debug.Log("Triggered by a PickupItem");
            itemDetected = true;
            portal.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger not activated");
        itemDetected = false;
        portal.SetActive(false);

    }
}
