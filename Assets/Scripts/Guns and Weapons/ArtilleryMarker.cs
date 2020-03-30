using UnityEngine;
using System.Collections;

public class ArtilleryMarker : StrikeMarker
{
    //The following fields hide the inherited properties of the same name from StrikeMarker.cs
    public new GameObject MarkerObj;
    public new ParticleSystem ExpPrefab;
    public new ParticleSystem MarkerSmoke;

    protected ParticleSystem.MainModule ExpMain { get; set; }
    protected ParticleSystem.MainModule SmokeMain { get; set; }
    protected override string[] MarkerTags { get; } = { "Artillery Strike Marker", "Artillery Strike Point" };

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        ExpMain = ExpPrefab.main;
        SmokeMain = MarkerSmoke.main;
        Lifetime = SmokeMain.duration;
    }

    protected override IEnumerator SendTargetData()
    {
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        MarkerBody.constraints = RigidbodyConstraints.FreezePosition;
        SentMessage = "Locking...";
        yield return new WaitForSeconds(LockTime);
        foreach (MeshRenderer mr in renderers)
        {
            mr.enabled = false;
        }
        ExpPrefab.Play();
        AssignTarget();
        yield return null;
    }

    protected void AssignTarget()
    {
        SentMessage = "Target approved. Artillery inbound.";
        MarkerSmoke.Play();
        MarkerObj.tag = "Artillery Strike Marker";
        gameObject.tag = "Artillery Strike Point";
    }

    protected void LateUpdate ()
    {
        if(!MarkerSmoke) Destroy(gameObject);
    }
}
