using UnityEngine;

public class LaceleratorBeam : MonoBehaviour, IWeapon
{
    public ParticleSystem MuzzleFlash { get; set; }
    public int ImpactForce { get; set; } = 80;
    public float FireRate { get; set; } = 0.25f;
    public bool Explosive { get; set; } = false;
    public GameObject explosionPrefab, impactPrefab;
    private GameObject UsedImpactPrefab { get; set; }
    public float NextFire { get; set; }

    public static WeaponManager.Weapon WeaponID { get; } = WeaponManager.Weapon.LaceleratorBeam; //Player starts the game with this weapon.

    public void Start()
    {
        MuzzleFlash = GetComponentInChildren<ParticleSystem>();
        UsedImpactPrefab = Explosive ? explosionPrefab : impactPrefab;
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > NextFire && LaceleratorBeamAmmoControl.CurrentAmmo > 0)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        MuzzleFlash.Play();
        if (Physics.Raycast(transform.Find("Spawnpoint").position, transform.forward, out RaycastHit hit))
        {
            //hit effects
            if (hit.rigidbody != null) hit.rigidbody.AddForce(-hit.normal * ImpactForce);
            GameObject impactFX = Instantiate(UsedImpactPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            impactFX.transform.SetParent(hit.collider.transform);

            //hitting an enemy turret
            if (hit.transform.CompareTag("EnemyTurret"))
            {
                //instantiate a metal impact prefab
                DeployableHealth hitHealth = hit.transform.GetComponent<DeployableHealth>();
                hitHealth.Health -= 2f;
            }
        }
        LaceleratorBeamAmmoControl.CurrentAmmo--;
        NextFire = Time.time + FireRate;
    }
}
