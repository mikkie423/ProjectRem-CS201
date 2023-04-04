using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenController : MonoBehaviour
{
    public float gardenhealth;

    public void TakeDamage()
    {
        gardenhealth--;
        if (gardenhealth == 0) Invoke(nameof(DestroyGarden), 0.5f);
    }

    private void DestroyGarden()
    {
        this.gameObject.SetActive(false);
    }
}
