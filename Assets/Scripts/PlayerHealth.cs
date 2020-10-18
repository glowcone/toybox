using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private const int MAXHEALTH = 10;
    private int health;
    [SerializeField] private Slider healthBar;

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
            Debug.Log("game over");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SkeletonSword"))
            health--;
    }
}
