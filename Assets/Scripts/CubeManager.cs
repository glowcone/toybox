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

    public Room currRoom;

    private Room[,,] _rooms;
    private Vector3[] ROTATION_AXIS = new[] {Vector3.forward, Vector3.forward, Vector3.forward, Vector3.forward, Vector3.right, Vector3.right};
    private int[] ROTATION_ANGLES = new[] {0, 90, 180, -90, 90, -90};
    private const int FACES = 6;
    void Start()
    {
        INSTANCE = this;
        _rooms = new Room[FACES, rows, rows];
        var newpos = transform.position + new Vector3(1, -1, 1) * spacing * rows;
        for (var i = 0; i < FACES; i++)
        {
            for (var j = 0; j < rows; j++)
            {
                for (var k = 0; k < rows; k++)
                {
                    var rand = Random.Range(0, roomPrefabs.Length);
                    var space = (spacing * rows - 1) / 2;
                    var pos = transform.position + new Vector3(-space, space + spacing/2, -space);
                    _rooms[i, j, k] = Instantiate(roomPrefabs[rand], pos + new Vector3(spacing * j, 0, spacing * k), Quaternion.identity);
                    _rooms[i, j, k].transform.RotateAround(transform.position, ROTATION_AXIS[i], ROTATION_ANGLES[i]);
                }
            }
        }
        
    }

    public void ShiftCubes(Vector2 direction)
    {
        
    }
}