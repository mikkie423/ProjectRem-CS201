using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    public float planthealth;
    public bool beingAttacked;
    public GameObject garden;
    public bool canBeAttacked;

    private void Awake()
    {
        canBeAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        if (canBeAttacked)
        {
            planthealth -= damage;
            if (planthealth == 0) Invoke(nameof(DestroyPlant), 0.5f);
        }
    }

    private void DestroyPlant()
    {
        garden.TryGetComponent<GardenController>(out var g);
        g.TakeDamage();

        this.gameObject.SetActive(false);

    }

}
