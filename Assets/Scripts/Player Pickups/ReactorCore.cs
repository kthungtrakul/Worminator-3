using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorCore : MonoBehaviour
{
    private new SphereCollider collider;
    private Rigidbody rb;

    protected virtual void Awake()
    {
        collider = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DelayCollisionDetector(1));
    }

    protected virtual void OnCollisionEnter()
    {
        rb.useGravity = false;
        rb.isKinematic = true;
        collider.isTrigger = true;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("This is a reactor core from a turret.");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual IEnumerator DelayCollisionDetector (int seconds)
    {
        yield return new WaitForSeconds(seconds);
        collider.isTrigger = false;
    }
}
