using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (!(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerGun")))
        {
            Debug.Log(other.gameObject.name);
            Destroy(gameObject);
        }
    }
}
