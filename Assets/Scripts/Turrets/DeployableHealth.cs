using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployableHealth : MonoBehaviour
{
    public float Health { get; set; }
    public float hitPoints;
    public ParticleSystem explosion;
    public GameObject reactorCore;
    [HideInInspector] public bool Dead { get; set; } = false;
    // Start is called before the first frame update
    private void Start()
    {
        Health = hitPoints;
    }

    private void LateUpdate()
    {
        if (Health <= 0 && !Dead)
        {
            BlowUp();
            Dead = true;
        }
    }

    private void BlowUp()
    {
        Rigidbody[] turretComponents = GetComponentsInChildren<Rigidbody>();
        explosion.Play();
        GameObject core = Instantiate(reactorCore, transform.position, transform.rotation);
        core.GetComponent<Rigidbody>().AddExplosionForce(800f, transform.position, 5f);
        foreach (Rigidbody body in turretComponents)
        {
            body.isKinematic = false;
            body.useGravity = true;
            body.gameObject.transform.parent = null;
            body.AddExplosionForce(800f, transform.position, 5f);
            Destroy(body.gameObject, 5f);
        }
        Destroy(gameObject, 5f);
    }
}
