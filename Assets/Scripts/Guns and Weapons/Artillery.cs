using System.Collections;
using UnityEngine;

[AddComponentMenu("Weapons/Weapon Control/Artillery")]
public class Artillery : Bullet
{
    public GameObject Target { get; set; }
    protected ConstantForce CF { get; set; }
    protected float Damp { get; set; } = 0;
    private BoxCollider Sensor { get; set; }

    // Start is called before the first frame update
    protected override void Start()
    {
        Body = GetComponent<Rigidbody>();
        CF = GetComponent<ConstantForce>();
        Sensor = GetComponent<BoxCollider>();
        UsedImpactPrefab = explosionPrefab;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (Target) StartCoroutine(Guide());
        if (!Target) Target = GameObject.FindWithTag("nulltarget");
    }

    private IEnumerator Guide ()
    {
        CF.relativeForce = new Vector3(0, 0, 200);
        yield return new WaitForSeconds(1f);
        Sensor.enabled = true;
        yield return new WaitForSeconds(5f);
        Damp = 1f;
        CF.relativeForce = new Vector3(0, 0, 400);
        Quaternion rotate = Quaternion.LookRotation(Target.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * Damp);
        if(Vector3.Distance(Target.transform.position, transform.position) < 200)
        {
            Damp = 6f;
            Body.drag = 4;
            CF.relativeForce = new Vector3(0, 0, 100);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * Damp);
        }
    }
}
