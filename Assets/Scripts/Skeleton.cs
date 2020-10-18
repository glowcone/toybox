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

    bool died = false;

    private AudioSource growl;
    // private AudioSource die;

    // Start is called before the first frame update
    void Start()
    {
        // health
        health = MAXHEALTH;
        healthBar.maxValue = MAXHEALTH;
        canvas.SetActive(false);

        // audio
        // var audio = GetComponents<AudioSource>();
        // Debug.Log(audio[0], audio[1]);
        growl = GetComponent<AudioSource>();
        // die = GetComponent<AudioSource>();
        // growl = audio[0];
        // die = audio[1];
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
        if (!died && health <= 0)
        {
            died = true;
            //Debug.Log("die");
            // growl.FadeOut(0.1f);
            // die.Play();
            StartCoroutine(DieAnimation());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            health--;
            StartCoroutine(DamageAnimation());
            // canvas.SetActive(true);
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
        anim.SetBool("Attack", true);
    }

    public void GetAgro()
    {
        growl.Play();
        canvas.SetActive(true);
    }

    public void StopAgro()
    {
        growl.FadeOut(0.5f);
        canvas.SetActive(false);
    }

    IEnumerator DamageAnimation()
    {
        anim.SetBool("Damaged", true);
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("Damaged", false);
    }

    IEnumerator DieAnimation()
    {
        anim.SetTrigger("Dead");
        yield return new WaitForSeconds(3.0f);
        Destroy(healthBar.gameObject);
        Destroy(gameObject);
    }
}
