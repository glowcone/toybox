using System;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class DirectionalButton : MonoBehaviour
{
    private static Vector2[] DIRECTIONS = new []{Vector2.up, Vector2.down, Vector2.right, Vector2.left};
    [SerializeField] private DirectionalButton twin;
    [SerializeField] private bool primary;

    private Vector2 _direction;
    private void Start()
    {
        if (primary)
        {
            _direction = DIRECTIONS[Random.Range(0, DIRECTIONS.Length)];
            transform.localRotation =
                Quaternion.LookRotation(new Vector3(_direction.x, 0, _direction.y));
            twin._direction = -_direction;
            twin.transform.localRotation =
                Quaternion.LookRotation(new Vector3(_direction.y, 0, _direction.x));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !PlayerController.INSTANCE.frozen)
        {
            CubeManager.INSTANCE.ShiftCubes((int) _direction.x, (int) _direction.y);
            gameObject.SetActive(false);
            twin.gameObject.SetActive(true);
        }
    }
}