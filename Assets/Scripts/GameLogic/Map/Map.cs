using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : BaseBehaviour
{
    [SerializeField] private Transform _cubes;
    private bool[,,] _map = new bool[Constants.MAP_SIZE, Constants.MAP_SIZE, Constants.MAP_SIZE];
    private GameObject[,,] _cubeGobs = new GameObject[Constants.MAP_SIZE, Constants.MAP_SIZE, Constants.MAP_SIZE];
    private Dictionary<int, List<GameObject>> _removeXDic;
    private Dictionary<int, List<GameObject>> _removeYDic;
    private Queue<GameObject> _removeQueue;


    public void SetCube(Transform trs)
    {
        int x = (int)trs.position.x;
        int y = (int)trs.position.y;
        int z = (int)trs.position.z;
        if (x < 0 || x >= Constants.MAP_SIZE || y < 0 || y >= Constants.MAP_SIZE || z < 0 || z >= Constants.MAP_SIZE)
        {

        }
        else
        {
            trs.SetParent(_cubes);
            _map[x, y, z] = true;
            _cubeGobs[x, y, z] = trs.gameObject;
        }

    }

    public void ChangeMapDirection()
    {
        _map = new bool[Constants.MAP_SIZE, Constants.MAP_SIZE, Constants.MAP_SIZE];
        _cubeGobs = new GameObject[Constants.MAP_SIZE, Constants.MAP_SIZE, Constants.MAP_SIZE];



        CubeLandingPoint[] cubes = GetComponentsInChildren<CubeLandingPoint>();
        foreach (var cube in cubes)
        {
            int x = (int)Math.Round(cube.transform.position.x);
            int y = (int)cube.transform.position.y;
            int z = (int)cube.transform.position.z;
            _cubeGobs[x, y, z] = cube.gameObject;
            _map[x, y, z] = true;

        }
        Debug.Log(_map[2, 0, 0]);
    }



    public void CheckMatch()
    {
        int count = 0;
        _removeXDic = new Dictionary<int, List<GameObject>>();
        _removeYDic = new Dictionary<int, List<GameObject>>();
        _removeQueue = new Queue<GameObject>();

        for (int i = 0; i < Constants.MAP_SIZE; i++) // (y)아래에서 위로
        {
            bool matched = true;
            _removeXDic[i] = new List<GameObject>();
            for (int j = 0; j < Constants.MAP_SIZE; j++)
            {
                for (int k = 0; k < Constants.MAP_SIZE; k++)
                {
                    _removeXDic[i].Add(_cubeGobs[j, i, k]);
                    if (!_map[j, i, k])
                    {
                        matched = false;
                        _removeXDic.Remove(i);
                        break;

                    }
                }
                if (!matched)
                    break;
            }
            if (matched)
                count++;
        }
        for (int i = 0; i < Constants.MAP_SIZE; i++) // (y)아래에서 위로
        {
            bool matched = true;
            for (int j = 0; j < Constants.MAP_SIZE; j++)
            {
                _removeYDic[i] = new List<GameObject>();
                for (int k = 0; k < Constants.MAP_SIZE; k++)
                {
                    _removeYDic[i].Add(_cubeGobs[i, j, k]);
                    if (!_map[i, j, k])
                    {
                        matched = false;
                        _removeYDic.Remove(i);

                        break;
                    }
                }
                if (!matched)
                    break;
            }
            if (matched)
                count++;
        }

        foreach (var VARIABLE in _removeXDic)
        {
            foreach (var VAR in VARIABLE.Value)
            {
                _removeQueue.Enqueue(VAR);
            }
        }
        foreach (var VARIABLE in _removeYDic)
        {
            foreach (var VAR in VARIABLE.Value)
            {
                _removeQueue.Enqueue(VAR);
            }
        }

        int c = _removeQueue.Count;
        for (int i = 0; i < c; i++)
        {
            Destroy(_removeQueue.Dequeue());
        }
    }

#if UNITY_EDITOR
    protected override void OnBindField()
    {
        base.OnBindField();
        _cubes = FindGameObjectInChildren<Transform>("Cubes");
    }
#endif
}