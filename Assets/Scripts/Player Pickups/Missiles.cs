using UnityEngine;

public class Missiles : MonoBehaviour
{
    private int ContainedAmmo { get; set; } = 5;
    [Range(1, 5)] public static int ammoMultiplier = 1;

    private void Start()
    {
        ContainedAmmo *= ammoMultiplier;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            MissileLauncherAmmoControl.CurrentAmmo += ContainedAmmo;
            Destroy(gameObject);
        }
    }
}
