using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBase : MonoBehaviour {
    protected Vector3 speed;
    protected float destroyDistance;

    public virtual void Fire() {
        Debug.Log("Fire");
    }

    protected void Destroy() {
        for (int i = 0; i < transform.childCount; i++) {
            var bullet = transform.GetChild(i);
            if ((bullet.position - transform.position).magnitude > destroyDistance) {
                Destroy(bullet.gameObject);
            }
        }
    }
}
