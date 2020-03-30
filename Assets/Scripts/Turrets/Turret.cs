using System.Collections;
using UnityEngine;

[AddComponentMenu("Turrets/Turret Base Class")]
public class Turret : MonoBehaviour
{
    public GameObject Target { get; set; }
    protected float Damp { get; set; }
    public GameObject bulletPrefab;
    protected Bullet Bullet { get; set; }
    protected int SavedTime { get; set; }
    protected ParticleSystem MuzzFlash { get; set; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        MuzzFlash = transform.Find("MuzzleFlash").GetComponent<ParticleSystem>();
    }

    protected virtual void LookAtTarget()
    {
        Quaternion rotate = Quaternion.LookRotation(Target.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * Damp);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual void Shoot(int seconds)
    {
        if (seconds != SavedTime)
        {
            MuzzFlash.Play();
            SavedTime = seconds;
        }
    }

    protected virtual IEnumerator Shoot()
    {
        yield return null;
    }
}
