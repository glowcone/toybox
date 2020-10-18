using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    [SerializeField] private int rows;
    [SerializeField] private float spacing;
    [SerializeField] private Room[] roomPrefabs;
    [SerializeField] private float[] roomProbability;

    public static CubeManager INSTANCE;

    private Room[,] _rooms;
    void Start()
    {
        INSTANCE = this;
        _rooms = new Room[rows, rows];
        var newpos = transform.position + new Vector3(1, -1, 1) * spacing * rows;
        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < rows; j++)
            {
                // wow this is terrible
                var rand = Random.Range(0, roomPrefabs.Length);
                _rooms[i, j] = Instantiate(roomPrefabs[rand], transform.position + Vector3.forward * spacing * i + Vector3.right * spacing * j, Quaternion.identity);
                rand = Random.Range(0, roomPrefabs.Length);
                Instantiate(roomPrefabs[rand],transform.position + Vector3.down * spacing * i + Vector3.right * spacing * j, Quaternion.Euler(Vector3.forward * -90 + Vector3.up * 90));
                rand = Random.Range(0, roomPrefabs.Length);
                Instantiate(roomPrefabs[rand],transform.position + Vector3.down * spacing * i + Vector3.forward * spacing * j, Quaternion.Euler(Vector3.right * 90 + Vector3.up * -90));
                rand = Random.Range(0, roomPrefabs.Length);
                Instantiate(roomPrefabs[rand], newpos + Vector3.back * spacing * (i+1) + Vector3.left * spacing * j, Quaternion.Euler(Vector3.forward * 180));
                rand = Random.Range(0, roomPrefabs.Length);
                Instantiate(roomPrefabs[rand],newpos + Vector3.up * spacing * (i+1) + Vector3.back * spacing * j, Quaternion.Euler(new Vector3(90, 90, 0)));
                rand = Random.Range(0, roomPrefabs.Length);
                Instantiate(roomPrefabs[rand], newpos + Vector3.up * spacing * (i + 1) + Vector3.left * spacing * j,
                    Quaternion.Euler(new Vector3(0, -90, -90)));
            }
        }
        
    }

    public void ShiftCubes(Vector3 position, Vector2 direction)
    {
    }
}