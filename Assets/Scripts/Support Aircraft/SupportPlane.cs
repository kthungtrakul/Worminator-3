using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportPlane : MonoBehaviour
{
    //A support plane flies around the entire gameplay scene, providing fire support to the player when called upon.
    public int flightVelocity;
    protected float Altitude { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        Altitude = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotate = Quaternion.LookRotation(new Vector3(0, Altitude, 0) - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * 0.1f);
        transform.Translate(0, 0, flightVelocity * Time.deltaTime, Space.Self);
    }
}
