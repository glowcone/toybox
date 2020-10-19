using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private const int MAXHEALTH = 10;
    private int health;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Animator anim;
    [SerializeField] private PlayerController pc;
    GameObject hurtSound, playerDieSound;

    bool died = false;

    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        health = MAXHEALTH;
        healthBar.maxValue = MAXHEALTH;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        hurtSound = GameObject.Find("hurt sound");
        playerDieSound = GameObject.Find("player die sound");
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = health;
        CheckDeath();
    }

    void CheckDeath()
    {
        if (!died && health <= 0)
        {
            died = true;
            //Debug.Log("game over");
            StartCoroutine(dieSound());
            anim.SetTrigger("Player Dead");
            pc.controller.enabled = false;
            gm.Lose();
        }
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
        hurtSound.GetComponent<AudioSource>().Play();
        //Debug.Log("taking damage");
        health--;
        anim.SetBool("Get Hurt", true);
    }

    IEnumerator dieSound()
    {
        yield return new WaitForSeconds(0.3f);
        playerDieSound.GetComponent<AudioSource>().Play();
    }
}
