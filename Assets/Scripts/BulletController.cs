using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    public Rigidbody projectile;
    public Transform Spawnpoint;
    public static event Action OnFire;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject gun;

    // Use this for initialization
    void Start ()
    {
        gun.SetActive(false);
    }

    // Update is called once per frame
    void Update ()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            GetComponent<AudioSource>().Play();
            StopCoroutine(ShootAnimation());
            StartCoroutine(ShootAnimation());
            Rigidbody clone;
            OnFire?.Invoke();
            clone = (Rigidbody)Instantiate(projectile, Spawnpoint.position, projectile.rotation);
            clone.velocity = Spawnpoint.TransformDirection(Vector3.forward*20);
        }
    }

    IEnumerator ShootAnimation()
    {
        if (!anim.GetBool("Shoot"))
            anim.SetBool("Shoot", true);
        gun.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        anim.SetBool("Shoot", false);
        gun.SetActive(false);
    }
}