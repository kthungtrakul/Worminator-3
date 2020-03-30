using UnityEngine;

[AddComponentMenu("Weapons/Weapon Base Classes/Missile")]
public class Missile : MonoBehaviour
{
    protected Rigidbody missileBody;
    public GameObject explosionPrefab;
    //private ParticleSystem missileTrail;
    protected ConstantForce rocketMotor;

    //[HideInInspector] public GameObject Target;
    public GameObject Target { get; set; }
    //[HideInInspector] public float Damping, MotorForce;
    public float Damping { get; set; }
    public int MotorForce { get; set; }
    //[HideInInspector] public bool Guided;
    public bool Guided { get; set; }
    public float ImpactForce { get; set; } = 150f;
    public float Damage { get; set; } = 20f;
    public float BlastRadius { get; set; } = 5f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        missileBody = GetComponent<Rigidbody>();
        rocketMotor = GetComponent<ConstantForce>();
        rocketMotor.relativeForce = new Vector3(0, 0, MotorForce);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!Target) FindATarget();
        if(Guided) GuidedMissileTracking();
        Destroy(gameObject, 30);
    }

    protected virtual void OnCollisionEnter(Collision other) //For missiles that rely on collision detection
    {
        BlowUp();
        if(other.gameObject.GetComponent<Rigidbody>())
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.AddExplosionForce(ImpactForce, transform.position, BlastRadius);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
        /*
         *  For missiles that rely on triggers, such as those that destroy themselves when in range of
         *  (not necessarily touching) their targets as to get the target to explode
         */
    {
        BlowUp();
        if (other.gameObject.GetComponent<Rigidbody>())
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.AddExplosionForce(ImpactForce, transform.position, BlastRadius);
        }
    }

    void FindATarget()
    {
        //Method called when target is null
        Target = GameObject.FindWithTag("nulltarget");
    }

    protected virtual void GuidedMissileTracking ()
    {
        Quaternion rotate = Quaternion.LookRotation(Target.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * Damping);
        if (Vector3.Distance(Target.transform.position, transform.position) < 100)
        {
            Damping = 5f;
            missileBody.drag = 3f;
        }
    }

    void BlowUp ()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
