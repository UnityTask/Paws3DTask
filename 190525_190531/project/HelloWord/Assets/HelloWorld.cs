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
        if (/* 修改这里 */) {
            Debug.Log("Hello");
        } else {
            Debug.Log("World");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
