﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skeleton : MonoBehaviour
{
    private const int MAXHEALTH = 3;
    private int health;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Animator anim;
    Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        health = MAXHEALTH;
        healthBar.maxValue = MAXHEALTH;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = health;
        CheckDeath();
    }

    void CheckDeath()
    {
        if (health <= 0)
        {
            Destroy(healthBar.gameObject);
            // gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            health--;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            playerPos = other.gameObject.transform.position;
            Attack();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            anim.SetBool("Attack", false);
    }

    void Attack()
    {
        //transform.LookAt(playerPos);
        anim.SetBool("Attack", true);
    }
}
