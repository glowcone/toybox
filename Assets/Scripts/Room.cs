using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject[] _walls = new GameObject[4];
    [SerializeField] private GameObject _doorPrefab;
    [SerializeField] private int _numDoors;
    private void Start()
    {
        Array.Sort(_walls, (x, y) => Random.Range(-1, 2));
        for (int i=0; i<_numDoors; i++)
        {
            var obj = Instantiate(_doorPrefab, transform);
            obj.transform.position = _walls[i].transform.position;
            obj.transform.rotation = _walls[i].transform.rotation;
        }
    }
}