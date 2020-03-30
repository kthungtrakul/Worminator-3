using System.Collections;
using UnityEngine;

[AddComponentMenu("Turrets/Artillery/Artillery Gun")]
public class ArtilleryGun : Turret
{
    [Range(1, 20)] public int limitRounds;
    private int RoundsFired { get; set; }
    private bool RoundFired { get; set; }
    private GameObject Mark { get; set; }
    
    // Start is called before the first frame update
    protected override void Start()
    {
        MuzzFlash = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(Mark)
        {
            if(!RoundFired)
            {
                StartCoroutine(Shoot());
                RoundFired = true;
            }
        }
        if(!Mark)
        {
            Target = GameObject.FindWithTag("Artillery Strike Point");
            Mark = GameObject.FindWithTag("Artillery Strike Marker");
        }
    }

    protected override IEnumerator Shoot()
    {
        GameObject ar = Instantiate(bulletPrefab, transform.Find("Spawnpoint").position, transform.rotation);
        MuzzFlash.Play();
        Artillery artillery = ar.GetComponent<Artillery>();
        artillery.Target = Target;
        RoundsFired++;
        yield return new WaitForSeconds(3f);
        RoundFired = false;
    }

    private void LateUpdate()
    {
        if(RoundsFired >= limitRounds)
        {
            Target = null;
            RoundsFired = 0;
            Destroy(Mark.gameObject);
        }
    }
}
