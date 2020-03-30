using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyTargetPractice : MonoBehaviour
{
    public GameObject practiceTargetPrefab;
    public bool active;
    public float delay;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Practice());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Practice ()
    {
        do
        {
            GameObject dum = Instantiate(practiceTargetPrefab, transform.position, transform.rotation);
            Missile msl = dum.GetComponent<Missile>();
            msl.Target = GameObject.Find("Dummy Target");
            msl.Guided = true;
            msl.Damping = 0.2f;
            msl.MotorForce = 300;
            msl.gameObject.tag = "enemyArtillery";
            yield return new WaitForSeconds(delay);
        } while (active);
    }
}
