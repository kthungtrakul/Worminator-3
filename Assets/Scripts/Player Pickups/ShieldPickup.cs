using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    private int ContainedShield { get; set; } = 10;
    [Range(1, 10)] public static int shieldMultiplier = 1;

    private void Start()
    {
        ContainedShield *= shieldMultiplier;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Shield.CurrentShield += ContainedShield;
            Destroy(transform.parent.gameObject);
        }
    }
}
