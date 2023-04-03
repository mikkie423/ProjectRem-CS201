using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public Rigidbody rb;
    public SphereCollider coll;
    public Transform player, itemContainer, playerCam, itemsParent;

    public float mass;
    public float pickupRange;
    public float pickupTime;
    public float dropForwardForce, dropUpwardForce;
    public bool equipped;
    public static bool slotFull;

    private void Start()
    {
        //Setup
        if (!equipped)
        { 
            rb.isKinematic = false;
            slotFull = false;
        }
        if (equipped)
        {
            rb.isKinematic = true;
            slotFull = true;
        }
    }

    private void Update()
    {
        //Check if player is in range and "E" is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude < pickupRange && Input.GetKeyDown(KeyCode.E) && !slotFull) PickUp();

        //Drop if eqippped and "Q" is pressed
        else if(equipped && Input.GetKeyDown(KeyCode.E)) Drop();
    }
    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        //Make item a child of the camera and move it to default position
        transform.SetParent(itemContainer);
        transform.localPosition = Vector3.zero;
        rb.freezeRotation = true;
        transform.localScale = new Vector3(.3f, .3f, .3f);

        rb.useGravity = false;
        rb.mass = 0;
        coll.enabled = false;
        rb.velocity = Vector3.zero;
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        //Set parent to null
        transform.SetParent(itemsParent);

        //Make Rigidbody not kinematic and BoxCollider normal
        rb.useGravity = true;
        rb.mass = mass;
        coll.enabled = true;
        rb.freezeRotation = false;
        transform.localScale = new Vector3(.3f, .3f, .3f);


        //Item carries momentum of player
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //AddForce
        rb.AddForce(playerCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(playerCam.up * dropUpwardForce, ForceMode.Impulse);

        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);
    }
}
