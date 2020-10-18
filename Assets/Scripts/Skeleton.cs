using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skeleton : MonoBehaviour
{
    private const int MAXHEALTH = 4;
    private int health;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Animator anim;
    Vector3 playerPos;
    [SerializeField] GameObject canvas;

    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        // health
        health = MAXHEALTH;
        healthBar.maxValue = MAXHEALTH;
        canvas.SetActive(false);

        // audio
        audio = GetComponent<AudioSource>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (canvas.active)
        {
            healthBar.value = health;
            CheckDeath();
        }

        // if (Random.Range(0.0f, 1.0f) < 0.001 && !audio.isPlaying)
        // {
        //     audio.Play();
        // }

        // https://gamedev.stackexchange.com/questions/114406/how-can-i-locate-gameobjects-near-the-player-in-unity
        // Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10);
        // if (hitColliders.Length > 0)
        // {
        //     var log = "";
        //     for (int i = 0; i < hitColliders.Length; i++)
        //     {
        //         log += hitColliders[i] + "\n";
        //     }
        //     Debug.Log(log);
        // }
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
            canvas.SetActive(true);
            //Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            //playerPos = other.gameObject.transform.position;
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

    public void GetAgro()
    {
        audio.Play();
    }

    public void StopAgro()
    {
        audio.FadeOut(0.5f);
    }
}
