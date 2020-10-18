using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject[] _walls = new GameObject[4];
    private void Start()
    {
        foreach (var wall in _walls)
        {
            wall.SetActive(false);
        }
        var rand = Random.Range(0, _walls.Length);
        _walls[rand].SetActive(true);
    }
}