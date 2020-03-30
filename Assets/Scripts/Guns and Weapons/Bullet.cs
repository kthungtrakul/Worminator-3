using UnityEngine;

//Required for bullet functionality
[RequireComponent(typeof(Rigidbody))]
[AddComponentMenu("Weapons/Weapon Base Classes/Bullet")]
public class Bullet : MonoBehaviour
{
    protected int MaxLifeTime { get; } = 20;
    protected Rigidbody Body { get; set; }
    public int ExpulsionForce { get; set; }
    public bool Incendiary { get; set; }
    public GameObject UsedImpactPrefab { get; set; }
    public GameObject explosionPrefab;
    public GameObject impactPrefab;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Body = GetComponent<Rigidbody>();
        Body.AddRelativeForce(transform.forward * ExpulsionForce);
        UsedImpactPrefab = Incendiary ? explosionPrefab : impactPrefab;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Destroy(gameObject, MaxLifeTime);
    }

    protected virtual void OnTriggerEnter()
    {
        Instantiate(UsedImpactPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
