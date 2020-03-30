using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private int ContainedHealth { get; set; } = 10;
    [Range(1, 10)] public static int healthMultiplier = 1;

    private void Start()
    {
        ContainedHealth *= healthMultiplier;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Health.CurrentHealth += ContainedHealth;
            Destroy(transform.parent.gameObject);
        }
    }
}
