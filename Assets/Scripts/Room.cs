using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject[] _walls = new GameObject[4];

    [SerializeField] private GameObject _doorPrefab;
    [SerializeField] private int _numDoors;

    [SerializeField] private GameObject player;
    [SerializeField] private int MAXENEMIES = 1;
    [SerializeField] private GameObject[] enemyPrefabs;
    private GameObject[] enemies;

    private void Start()
    {
        Array.Sort(_walls, (x, y) => Random.Range(-1, 2));
        for (int i=0; i<_numDoors; i++)
        {
            var obj = Instantiate(_doorPrefab, transform);
            obj.transform.position = _walls[i].transform.position;
            obj.transform.rotation = _walls[i].transform.rotation;
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