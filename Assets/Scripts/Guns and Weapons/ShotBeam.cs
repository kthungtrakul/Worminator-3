using UnityEngine;

public class ShotBeam : MonoBehaviour, IWeapon
{
    public ParticleSystem MuzzleFlash { get; set; }
    public int ImpactForce { get; set; } = 80;
    public int Delay { get; set; } = 2;
    public bool Explosive { get; set; } = false;
    public GameObject explosionPrefab, impactPrefab;
    private GameObject UsedImpactPrefab { get; set; }
    private GameObject[] Spawnpoints { get; set; }
    public float NextFire { get; set; }

    public static WeaponManager.Weapon WeaponID { get; } = WeaponManager.Weapon.ShotBeam; //Player must acquire this weapon by finding it in-game.

    // Start is called before the first frame update
    public void Start()
    {
        Spawnpoints = GameObject.FindGameObjectsWithTag("Shot Beam Spawnpoint");
        MuzzleFlash = GetComponentInChildren<ParticleSystem>();
        UsedImpactPrefab = Explosive ? explosionPrefab : impactPrefab;
    }

    // Update is called once per frame
    public void Update()
    {
        if(Input.GetButton("Fire1") && Time.time > NextFire && ShotBeamAmmoControl.CurrentAmmo > 0)
        {
            Shoot();
        }
    }

    public void Shoot ()
    {
        MuzzleFlash.Play();
        foreach(GameObject spawnpoint in Spawnpoints)
        {
            if(Physics.Raycast(spawnpoint.transform.position,spawnpoint.transform.forward,out RaycastHit hit))
            {
                //hit effects
                if (hit.rigidbody != null) hit.rigidbody.AddForce(-hit.normal * ImpactForce);
                GameObject impactFX = Instantiate(UsedImpactPrefab, hit.point, Quaternion.LookRotation(hit.normal,transform.up));
                impactFX.transform.SetParent(hit.collider.transform);

                //hitting an enemy turret
                if(hit.transform.CompareTag("EnemyTurret"))
                {
                    //instantiate a metal impact prefab
                    DeployableHealth hitHealth = hit.transform.GetComponent<DeployableHealth>();
                    hitHealth.Health -= 3f;
                }
            }
        }
        ShotBeamAmmoControl.CurrentAmmo--;
        NextFire = Time.time + Delay;
    }
}
