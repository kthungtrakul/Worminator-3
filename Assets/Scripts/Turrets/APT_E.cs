using UnityEngine;
//using TMPro;

public class APT_E : Turret
{
    private int RadarDistance { get; set; } = 50;
    private DeployableHealth health { get; set; } //accesses the health script for this turret
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        health = GetComponent<DeployableHealth>();
        Target = GameObject.FindWithTag("Player");
        Damp = 2f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (Target && !health.Dead)
        {
            float range = Vector3.Distance(Target.transform.position, transform.position);
            if (range < RadarDistance)
            {
                LookAtTarget();
                if (range < RadarDistance / 2)
                {
                    int seconds = (int)Time.timeSinceLevelLoad;
                    Shoot(seconds);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Missile"))
        {
            GetComponent<DeployableHealth>().Health -= 5f;
        }
    }

    protected override void Shoot(int seconds)
    {
        if (seconds != SavedTime)
        {
            MuzzFlash.Play();
            if (Physics.Raycast(transform.Find("Spawnpoint").position, transform.forward, out RaycastHit hit))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    if (!Shield.ShieldActive) Health.CurrentHealth -= 5f;
                    else Shield.CurrentShield -= 5f;
                }
            }
            SavedTime = seconds;
        }
    }
}
