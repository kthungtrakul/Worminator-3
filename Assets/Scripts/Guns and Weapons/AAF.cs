using System.Collections;
using UnityEngine;

public class AAF : Missile
{
    private SphereCollider SensorRange { get; set; }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        SensorRange = GetComponent<SphereCollider>();
        StartCoroutine(InitializeSensor());
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if(Target) GuidedMissileTracking();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "enemyArtillery")
        {
            Missile msl = other.gameObject.GetComponent<Missile>();
            Instantiate(msl.explosionPrefab, msl.transform.position, Quaternion.identity);
            Destroy(msl.gameObject);
            Destroy(this);
        }
        base.OnTriggerEnter(other);
    }

    protected override void GuidedMissileTracking()
    {
        base.GuidedMissileTracking();
        if(Vector3.Distance(Target.transform.position,transform.position) < 20)
        {
            rocketMotor.relativeForce = new Vector3(0, 0, 500);
            SensorRange.radius = 10f;
            missileBody.drag = 2f;
        }
    }

    IEnumerator InitializeSensor ()
    {
        yield return new WaitForSeconds(3f);
        GetComponent<CapsuleCollider>().enabled = false;
        SensorRange.enabled = true;
    }
}
