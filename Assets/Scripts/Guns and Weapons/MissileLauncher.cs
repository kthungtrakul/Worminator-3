using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour, IWeapon
{
	public ParticleSystem MuzzleFlash { get; set; }
	public GameObject missilePrefab;
    private int Delay { get; } = 2;
	private Missile Missile { get; set; }
    public float NextFire { get; set; }
    
    public static WeaponManager.Weapon WeaponID { get; } = WeaponManager.Weapon.MissileLauncher; //Player must acquire this weapon by finding it in-game.

	// Start is called before the first frame update
	public void Start()
    {
        MuzzleFlash = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    public void Update()
    {
        if(Input.GetButtonDown("Fire1") && Time.time > NextFire && MissileLauncherAmmoControl.CurrentAmmo > 0)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        MuzzleFlash.Play();
        GameObject missile = Instantiate(missilePrefab, transform.Find("Spawnpoint").position, transform.rotation);
        missile.tag = "Missile";
        Missile msl = missile.GetComponent<Missile>();
        msl.MotorForce = 500;
        msl.Guided = false;
        MissileLauncherAmmoControl.CurrentAmmo--;
        NextFire = Time.time + Delay;
    }
}
