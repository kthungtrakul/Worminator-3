using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeMarker : MonoBehaviour
{
    protected float Lifetime { get; set; } //Markers that are active for a set time, e.g. for calling in waves of ordinance
    protected int LockTime { get; } = 5; //Markers take this long to send target location data
    public int ExpulsionForce { get; set; }
    protected GameObject MarkerObj { get; set; }
    protected Rigidbody MarkerBody { get; set; }
    protected ParticleSystem MarkerSmoke { get; set; }
    protected ParticleSystem ExpPrefab { get; set; }
    protected virtual string[] MarkerTags { get; }
    protected string SentMessage { get; set; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        MarkerBody = GetComponent<Rigidbody>();
        MarkerBody.AddForce(transform.forward * ExpulsionForce);
    }

    protected virtual void OnCollisionEnter()
    {
        StartCoroutine(SendTargetData());
    }

    protected virtual IEnumerator SendTargetData ()
    {
        MarkerBody.constraints = RigidbodyConstraints.FreezePosition;
        SentMessage = "Locking...";
        yield return new WaitForSeconds(LockTime);
        //override this coroutine with the appropriate target assignment operations for each fire support element.
    }
}
