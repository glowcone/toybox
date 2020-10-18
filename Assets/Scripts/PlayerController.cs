using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // https://docs.unity3d.com/ScriptReference/CharacterController.Move.html

    [SerializeField] private CharacterController controller;
    private Vector3 playerVelocity;
    // private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    // private float jumpHeight = 1.0f;
    // private float gravityValue = -9.81f;

    public Animator anim;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        // controller = gameObject.AddComponent<CharacterController>();
        // controller.skinWidth = 0.03f;
        // controller.height = 0.005f;
        BulletController.OnFire += TurnPlayer;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        transform.SetParent(CubeManager.INSTANCE.currRoom.transform);
    }

    private void TurnPlayer()
    {
        gameObject.transform.eulerAngles = transform.up * _camera.transform.rotation.eulerAngles.y;
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
}