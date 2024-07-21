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
    private Dictionary<int, List<GameObject>> _removeZDic;

    private Queue<GameObject> _removeQueue;

    private void Start()
    {
        GameSceneManager.Instance.GameSceneEvent.OnGameOver += GameOverEvent;
    }
    private void OnDestroy()
    {
        GameSceneManager.Instance.GameSceneEvent.OnGameOver -= GameOverEvent;
    }
    public void SetCube(Transform trs)
    {
        int x = (int)trs.position.x;
        int y = (int)trs.position.y;
        int z = (int)trs.position.z;
        if (x < 0 || x >= Constants.MAP_SIZE || y < 0 || y >= Constants.MAP_SIZE || z < 0 || z >= Constants.MAP_SIZE)
        {
            GameSceneManager.Instance.GameSceneEvent.CallGameOver();
        }
        else
        {
            trs.SetParent(_cubes);
            if (_map[x, y, z])
                Debug.Log("Bug");
            _map[x, y, z] = true;
            _cubeGobs[x, y, z] = trs.gameObject;
            GameSceneManager.Instance.GameSceneEvent.CallOnAddScore(ScoreSystem.SCORE_CUBE_BATCH);
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
            int y = (int)Math.Round(cube.transform.position.y);
            int z = (int)Math.Round(cube.transform.position.z);
            _cubeGobs[x, y, z] = cube.gameObject;
            _map[x, y, z] = true;
        }
    }

    private void GameOverEvent(GameSceneEvents gameSceneEvents)
    {
        _map = new bool[Constants.MAP_SIZE, Constants.MAP_SIZE, Constants.MAP_SIZE];
        _cubeGobs = new GameObject[Constants.MAP_SIZE, Constants.MAP_SIZE, Constants.MAP_SIZE];
    }
    public void CheckMatch()
    {
        int count = 0;
        _removeXDic = new Dictionary<int, List<GameObject>>();
        _removeYDic = new Dictionary<int, List<GameObject>>();
        _removeZDic = new Dictionary<int, List<GameObject>>();
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
                        _removeXDic[i].Clear();
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
            _removeYDic[i] = new List<GameObject>();

            for (int j = 0; j < Constants.MAP_SIZE; j++)
            {
                for (int k = 0; k < Constants.MAP_SIZE; k++)
                {
                    _removeYDic[i].Add(_cubeGobs[i, j, k]);
                    if (!_map[i, j, k])
                    {
                        matched = false;
                        _removeYDic[i].Clear();

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
            _removeZDic[i] = new List<GameObject>();

            for (int j = 0; j < Constants.MAP_SIZE; j++)
            {
                for (int k = 0; k < Constants.MAP_SIZE; k++)
                {
                    _removeZDic[i].Add(_cubeGobs[j, k, i]);
                    if (!_map[j, k, i])
                    {
                        matched = false;
                        _removeZDic[i].Clear();

                        break;
                    }
                }
                if (!matched)
                    break;
            }
            if (matched)
                count++;
        }

        if (count > 0)
        {
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
            foreach (var VARIABLE in _removeZDic)
            {
                foreach (var VAR in VARIABLE.Value)
                {
                    _removeQueue.Enqueue(VAR);
                }
            }
            int c = _removeQueue.Count;
            for (int i = 0; i < c; i++)
            {
                GameObject go = _removeQueue.Dequeue();

                int x = (int)Math.Round(go.transform.position.x);
                int y = (int)Math.Round(go.transform.position.y);
                int z = (int)Math.Round(go.transform.position.z);
                _map[x, y, z] = false;
                _cubeGobs[x, y, z] = null;
                go.GetComponent<CubeLandingPoint>().ExplodeCube();
                Destroy(go);
            }

            GameSceneManager.Instance.GameSceneEvent.CallOnAddScore(count * ScoreSystem.SCORE_CUBE_MATCH + (ScoreSystem.SCORE_CUBE_COMBO * (count - 1)));

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