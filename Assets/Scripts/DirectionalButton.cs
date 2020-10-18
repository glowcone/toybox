using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class DirectionalButton : MonoBehaviour
{
    private static Vector2[] DIRECTIONS = new []{Vector2.up, Vector2.down, Vector2.right, Vector2.left};

    private Vector2 _direction;
    private void Start()
    {
        _direction = DIRECTIONS[Random.Range(0, DIRECTIONS.Length)];
    }

    private void OnTriggerEnter(Collider other)
    {
        CubeManager.INSTANCE.ShiftCubes(0, 1);
    }
}