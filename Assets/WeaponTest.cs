using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTest : MonoBehaviour {

    public GameObject Shot;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if( Input.GetMouseButtonUp(0) )
        {
            GameObject tmp = Instantiate(Shot, Shot.transform.position, Shot.transform.rotation );
            tmp.SetActive(true);
        }

	}
}
