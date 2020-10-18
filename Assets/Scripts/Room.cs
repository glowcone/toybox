using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject[] _walls = new GameObject[4];

    [SerializeField] private GameObject player;
    [SerializeField] private int MAXENEMIES = 1;
    [SerializeField] private GameObject[] enemyPrefabs;
    private GameObject[] enemies;

    private void Start()
    {
        foreach (var wall in _walls)
        {
            wall.SetActive(false);
        }
        var rand = Random.Range(0, _walls.Length);
        _walls[rand].SetActive(true);

        int numEnemies = (int)Random.Range(0, MAXENEMIES);
        enemies = new GameObject[numEnemies];
        for (int i = 0; i < numEnemies; i++) {
            GameObject enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length - 1)];
            enemies[i] = Instantiate(enemy, transform.position, Quaternion.identity);
        }
    }
}