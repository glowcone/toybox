using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    int health = 3;
    [SerializeField] private Animator anim;
    Vector3 playerPos;

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
            gameObject.SetActive(false);
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
