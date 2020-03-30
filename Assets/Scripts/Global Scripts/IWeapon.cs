using UnityEngine;

public interface IWeapon
{
    ParticleSystem MuzzleFlash { get; set; }
    float NextFire { get; set; }
    
    void Start();
    void Update();
    void Shoot();
}