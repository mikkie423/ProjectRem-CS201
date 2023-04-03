using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{

    [SerializeField] Transform destination;
    DetectorController controller;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && other.TryGetComponent<PlayerMovementController>(out var player))
        {
            player.Teleport(destination.position, destination.rotation);
            controller.item.SetActive(false);
            controller.portal.SetActive(false);
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(destination.position, .4f);
        var direction = destination.TransformDirection(Vector3.forward);
        Gizmos.DrawRay(destination.position, direction);
    }
}
