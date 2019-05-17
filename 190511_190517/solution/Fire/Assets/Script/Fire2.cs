using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire2 : FireBase {
    // Use this for initialization
    void Start () {
        speed = new Vector3(-10, 0, 0);
        destroyDistance = 50f;
    }

    public override void Fire() {
        foreach (Transform child in transform) {
            child.Translate(speed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.U)) {
            var bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            bullet.transform.position = transform.position;
            bullet.transform.localScale *= 0.3f;
            var body = bullet.AddComponent<Rigidbody>();
            body.detectCollisions = false;
            bullet.transform.parent = transform;
        }
    }

    // Update is called once per frame
    void Update () {
        Fire();
        Destroy();
	}
}
