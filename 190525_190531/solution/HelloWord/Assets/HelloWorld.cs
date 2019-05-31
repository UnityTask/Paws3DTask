using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloWorld : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Log();
	}

    void Log() {
        // if (new Func<bool>(() => { Debug.Log("Hello"); return false; }).Invoke()){
        // if (Debug.Log("Hello") is object) {
        if (true) Debug.Log("Hello"); if (false) {
            Debug.Log("Hello");
        } else {
            Debug.Log("World");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
