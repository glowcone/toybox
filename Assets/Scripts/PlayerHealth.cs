using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    int health = 10;
    [SerializeField] private Animator anim;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckDeath();
    }

    void CheckDeath()
    {
        if (health <= 0)
            Debug.Log("game over");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SkeletonSword"))
            TakeDamage();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("SkeletonSword"))
            anim.SetBool("Get Hurt", false);
    }

    void TakeDamage()
    {
        Debug.Log("taking damage");
        health--;
        anim.SetBool("Get Hurt", true);
    }
}
