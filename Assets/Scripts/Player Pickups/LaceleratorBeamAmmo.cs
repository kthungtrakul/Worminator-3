using UnityEngine;

public class LaceleratorBeamAmmo : MonoBehaviour
{
    private int ContainedAmmo { get; set; } = 50;
    [Range(1, 5)] public static int ammoMultiplier = 1;

    private void Start()
    {
        ContainedAmmo *= ammoMultiplier;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            LaceleratorBeamAmmoControl.CurrentAmmo += ContainedAmmo;
            Destroy(gameObject);
        }
    }
}
