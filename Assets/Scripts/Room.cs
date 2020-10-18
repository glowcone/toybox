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
    private Skeleton skeleton;
    private void Start()
    {
        Array.Sort(_walls, (x, y) => Random.Range(-1, 2));
        // var obj = Instantiate(_doorPrefab, transform);
        // obj.GetComponentInChildren<Door>().wall = _walls[0];
        // obj.transform.position = _walls[0].transform.position;
        // obj.transform.rotation = _walls[0].transform.rotation;
        for (int i=0; i<_numDoors; i++)
        {
            _walls[i].gameObject.SetActive(false);
        }
        // _walls[0].gameObject.SetActive(true);

        var s = transform.Find("Skeleton (1)");
        if (s)
        {
            skeleton = s.gameObject.GetComponent<Skeleton>();
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
        if (other.CompareTag("Player") && !PlayerController.INSTANCE.frozen)
        {
            CubeManager.INSTANCE.currRoom = this;
            var playerTransform = PlayerController.INSTANCE.transform;
            playerTransform.SetParent(this.transform);
            if (skeleton)
            {
                skeleton.GetAgro();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player"))
        {
            if (skeleton)
            {
                skeleton.StopAgro();
            }
        }
    }
}