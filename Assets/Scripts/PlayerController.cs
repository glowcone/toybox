using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // https://docs.unity3d.com/ScriptReference/CharacterController.Move.html

    [SerializeField] private CharacterController controller;
    [SerializeField] private CinemachineBrain brain;
    private Vector3 playerVelocity;
    // private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    // private float jumpHeight = 1.0f;
    // private float gravityValue = -9.81f;

    public static PlayerController INSTANCE;
    public Animator anim;
    private Camera _camera;

    private List<GameObject> _artifacts;
    [SerializeField] private Text artifactCount;

    private void Awake()
    {
        INSTANCE = this;
    }

    private void Start()
    {
        _camera = Camera.main;
        // controller = gameObject.AddComponent<CharacterController>();
        // controller.skinWidth = 0.03f;
        // controller.height = 0.005f;
        BulletController.OnFire += TurnPlayer;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _artifacts = new List<GameObject>();
        brain.m_WorldUpOverride = this.transform;
    }

    private void TurnPlayer()
    {
        transform.LookAt(transform.position + Vector3.ProjectOnPlane(_camera.transform.forward, transform.up), transform.up);
    }

    void Update()
    {
        // MOVE

        // groundedPlayer = controller.isGrounded;
        // if (groundedPlayer && playerVelocity.y < 0)
        // {
        //     playerVelocity.y = 0f;
        // }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Vector3 move = new Vector3(horizontal, 0, vertical);
        
        if (move != Vector3.zero)
        {
            TurnPlayer();
            controller.Move((transform.right * horizontal + transform.forward * vertical) * Time.deltaTime * playerSpeed);
        }

        // JUMP

        // if (Input.GetButtonDown("Jump") && groundedPlayer)
        // {
        //     playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        // }

        // playerVelocity.y += gravityValue * Time.deltaTime;
        // controller.Move(playerVelocity * Time.deltaTime);

        anim.SetFloat("Vertical", vertical);
        anim.SetFloat("Horizontal", horizontal);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "artifact")
        {
            Debug.Log("artifact");
            _artifacts.Add(other.gameObject);
            artifactCount.text = "ARTIFACTS: " + _artifacts.Count + "/4";
            Destroy(other.gameObject);
        }
    }
}