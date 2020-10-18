﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeManager : MonoBehaviour
{
    [SerializeField] private int rows;
    [SerializeField] private float spacing;
    [SerializeField] private Room[] roomPrefabs;
    [SerializeField] private float[] roomProbability;
    [SerializeField] private float animFrameSecs, animTotalSecs;
    [SerializeField] private Animator cameraAnimator;

    public static CubeManager INSTANCE;

    public Room currRoom;

    private Room[,,] _rooms;
    private Vector3[] ROTATION_AXIS = new[] {Vector3.forward, Vector3.forward, Vector3.forward, Vector3.forward, Vector3.right, Vector3.right};
    private int[] ROTATION_ANGLES = new[] {0, 90, 180, -90, 90, -90};
    private const int FACES = 6;

    private void Awake()
    {
        INSTANCE = this;
    }

    void Start()
    {
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

        currRoom = _rooms[0, 0, 0];
        currRoom.SpawnPlayer();
    }

    private void Update()
    {
    }

    public void ShiftCubes(int x, int y)
    {
        var all = spacing * rows * 2;
        var hit = Physics.OverlapBox(currRoom.transform.position,
            new Vector3(Math.Abs(y) * all, all, Math.Abs(x) * all), currRoom.transform.rotation);
        foreach (var box in hit)
        {
            if (box.GetComponent<Room>())
            {
                StartCoroutine(RotateOverTime(box.gameObject, transform.position, currRoom.transform.right * x + currRoom.transform.forward * y, 90, animTotalSecs));
                cameraAnimator.SetTrigger("CubeMode");
            }
        }
    }

    IEnumerator RotateOverTime(GameObject obj, Vector3 pos, Vector3 axis, float angle, float secs)
    {
        float currAngle = 0;
        while (Mathf.Abs(currAngle) < Mathf.Abs(angle))
        {
            obj.transform.RotateAround(pos, axis, angle/(secs/animFrameSecs));
            currAngle += angle/(secs/animFrameSecs);
            yield return new WaitForSeconds(animFrameSecs);
        }
        cameraAnimator.SetTrigger("PlayerMode");
        currRoom.SpawnPlayer();
    }
}