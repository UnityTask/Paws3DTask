using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformSample : MonoBehaviour {
    public float moveSpeed = 1f;
    public float rotateSpeed = 100f;
    public float scaleSpeed = 1f;

    public float scale = 1f;

    // Use this for initialization
    void Start () {
		
	}

    private void Move() {
        if (Input.GetKey(KeyCode.W)) {
            gameObject.transform.Translate(moveSpeed * Time.deltaTime * Vector3.forward);
        }
        if (Input.GetKey(KeyCode.A)) {
            gameObject.transform.Translate(moveSpeed * Time.deltaTime * Vector3.left);
        }
        if (Input.GetKey(KeyCode.S)) {
            gameObject.transform.Translate(moveSpeed * Time.deltaTime * Vector3.back);
        }
        if (Input.GetKey(KeyCode.D)) {
            gameObject.transform.Translate(moveSpeed * Time.deltaTime * Vector3.right);
        }
        if (Input.GetKey(KeyCode.Q)) {
            gameObject.transform.Translate(moveSpeed * Time.deltaTime * Vector3.up);
        }
        if (Input.GetKey(KeyCode.E)) {
            gameObject.transform.Translate(moveSpeed * Time.deltaTime * Vector3.down);
        }
    }
    private void Rotate () {
        if (Input.GetKey(KeyCode.Alpha1)) {
            gameObject.transform.Rotate(rotateSpeed * Time.deltaTime * new Vector3(1, 0, 0));
        }
        if (Input.GetKey(KeyCode.Alpha2)) {
            gameObject.transform.Rotate(rotateSpeed * Time.deltaTime * new Vector3(0, 1, 0));
        }
        if (Input.GetKey(KeyCode.Alpha3)) {
            gameObject.transform.Rotate(rotateSpeed * Time.deltaTime * new Vector3(0, 0, 1));
        }
    }

    private void Scale() {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) {
            scale += scaleSpeed * Time.deltaTime;
            gameObject.transform.localScale = scale * Vector3.one;
        }
        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) {
            scale -= scaleSpeed * Time.deltaTime;
            gameObject.transform.localScale = scale * Vector3.one;
        }
    }

    // Update is called once per frame
    void Update () {
        Move();
        Rotate();
        Scale();
	}
}
