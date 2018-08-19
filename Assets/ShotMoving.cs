using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotMoving : MonoBehaviour {

    public AnimationCurve Shot_AC;
    private float AccelTimer;


    // Use this for initialization
    void Start () {
        AccelTimer = 0;
    }
	
	// Update is called once per frame
	void Update () {

        AccelTimer += Time.deltaTime;
        transform.position += transform.forward * Shot_AC.Evaluate(AccelTimer);
    }


    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.name);
    }
}
