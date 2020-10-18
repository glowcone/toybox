using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    public Rigidbody projectile;
    public Transform Spawnpoint;
    public static event Action OnFire;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        if(Input.GetButtonDown("Fire1")){
            Rigidbody clone;
            OnFire?.Invoke();
            clone = (Rigidbody)Instantiate(projectile, Spawnpoint.position, projectile.rotation);

            clone.velocity = Spawnpoint.TransformDirection(Vector3.forward*20);
        }
    }

    void OnTriggerEnter()
    {
        // the bullet goes through walls smh
    }
}