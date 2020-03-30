using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormRadar : MonoBehaviour
{

    public GameObject Player { get; set; }
    public GameObject PlayerArrow { get; set; }

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        PlayerArrow = GameObject.FindWithTag("Player Arrow");
    }

    void LateUpdate()
    {
        Vector3 newPosition = Player.transform.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
        Quaternion newArrowRotation = Quaternion.Euler(0, 0, -Player.transform.eulerAngles.y);
        PlayerArrow.transform.rotation = newArrowRotation;
    }
}
