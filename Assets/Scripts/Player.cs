using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator anim;
    GameObject artifactSound;
    
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        artifactSound = GameObject.Find("artifact sound");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("artifact"))
        {
            gm.AddPoint();
            artifactSound.GetComponent<AudioSource>().Play();
            other.gameObject.SetActive(false);
        }
    }
}
