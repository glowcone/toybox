using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject[] _walls = new GameObject[4];
    [SerializeField] private GameObject _doorPrefab;
    [SerializeField] private int _numDoors;
    public GameObject _spawnPoint;
    private void Start()
    {
        Array.Sort(_walls, (x, y) => Random.Range(-1, 2));
        for (int i=0; i<_numDoors; i++)
        {
            // var obj = Instantiate(_doorPrefab, transform);
            // obj.transform.position = _walls[i].transform.position;
            // obj.transform.rotation = _walls[i].transform.rotation;
            _walls[i].gameObject.SetActive(false);
        }
    }

    public void SpawnPlayer()
    {
        var playerTransform = PlayerController.INSTANCE.transform;
        playerTransform.SetParent(this.transform);
        playerTransform.position = _spawnPoint.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CubeManager.INSTANCE.currRoom = this;
            var playerTransform = PlayerController.INSTANCE.transform;
            playerTransform.SetParent(this.transform);
        }
    }
}