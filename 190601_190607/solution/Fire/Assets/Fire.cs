using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit myHit;
        if (Physics.Raycast(myRay, out myHit)) {
            if (Input.GetMouseButtonDown(0)) {
                Destroy(myHit.collider.gameObject);
            }
        }
    }
}
