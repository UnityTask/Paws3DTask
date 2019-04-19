using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public int id;

    public string playerName;
    public string description;

    public float HP;
    public float damage;

    public Vector3 playerPosition;
    public float playerWeight;

    public string petName;
    public int petAge;
    public string petType;

    public void HPRecovered()
    {
        HP = 255;
    }
}
