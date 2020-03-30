using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyWeaponLauncher : MonoBehaviour
{
    private Missile missile;
    private Bullet bullet1;
    private StrikeMarker marker;
    public GameObject missilePrefab, bulletPrefab1, markerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) FireBulletType1();
        if (Input.GetButtonDown("Fire2")) FireStrikeMarker();
        if(Input.GetButtonDown("Fire3")) FireMissile();

    }

    void FireBulletType1 ()
    {
        GameObject bul = Instantiate(bulletPrefab1, transform.position, transform.rotation);
        bullet1 = bul.GetComponent<Bullet>();
        bullet1.ExpulsionForce = 300;
        bullet1.Incendiary = true;
        bullet1.UsedImpactPrefab = bullet1.explosionPrefab;
    }

    void FireStrikeMarker()
    {
        GameObject mkr = Instantiate(markerPrefab, transform.position, transform.rotation);
        marker = mkr.GetComponent<StrikeMarker>();
        marker.ExpulsionForce = 100;
    }

    void FireMissile ()
    {
        GameObject msl = Instantiate(missilePrefab, transform.position, transform.rotation);
        missile = msl.GetComponent<Missile>();
        missile.Target = GameObject.Find("Dummy Target");
        missile.MotorForce = 500;
        missile.Guided = false;
        missile.Damping = 0.5f;
    }
}
