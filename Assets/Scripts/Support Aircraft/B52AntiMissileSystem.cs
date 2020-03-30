using System.Collections;
using UnityEngine;

[AddComponentMenu("Support Aircraft/B-52/Anti-Missile System")]
public class B52AntiMissileSystem : MonoBehaviour
{
    public GameObject missilePrefab;
    private GameObject Target { get; set; }
    private int Damp { get; } = 6;
    private int RadarDistance { get; } = 4000;
    private bool Detect { get; set; }
    private bool Fired { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Target = GameObject.FindWithTag("enemyArtillery");
        //Target = GameObject.Find("Dummy Target");
        //if (!Target) Target = GameObject.FindWithTag("enemyMissile");
        //if (!Target) Target = GameObject.FindWithTag("enemySSM");

        if(Target)
        {
            Detect = Target.transform.position.y <= transform.position.y;
        }

        if (Target && Detect && Vector3.Distance(Target.transform.position, transform.position) < RadarDistance)
        {
            Quaternion rotate = Quaternion.LookRotation(Target.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * Damp);
            if(!Fired)
            {
                StartCoroutine(Fire());
                Fired = true;
            }
        }
        if(!Target)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, Time.deltaTime * Damp);
        }
    }

    IEnumerator Fire()
    {
        yield return new WaitForSeconds(3f);
        GameObject rkt = Instantiate(missilePrefab, transform.Find("Spawnpoint").position, transform.rotation);
        AAF aa = rkt.GetComponent<AAF>();
        aa.Target = Target;
        aa.Damping = 0.75f;
        aa.MotorForce = 300;
        aa.gameObject.tag = "AntiArtillery";
        yield return new WaitForSeconds(5f);
        Fired = false;
    }
}
