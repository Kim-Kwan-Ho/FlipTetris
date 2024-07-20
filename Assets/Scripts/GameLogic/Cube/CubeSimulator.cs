using System;
using System.Collections.Generic;
using UnityEngine;

public class CubeSimulator : BaseBehaviour
{

    [SerializeField] private GameObject _cubePrefab;
    private Cube _cube;
    [SerializeField] private bool[,] _cubeBatch = new bool[3, Constants.CUBE_MAXSIZE];
    [SerializeField] private List<GameObject> _sampleCubeList;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private Map _map;
    protected override void Awake()
    {
        GetNextCube();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _cube == null)
        {
            GetNextCube();
        }
        if (_cube == null)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, _layer))
        {
            Vector3 spawnPoint = new Vector3((float)Math.Round(hit.point.x), (hit.point.y), (float)Math.Round(hit.point.z));
            _cube.CheckLandingPoint(spawnPoint);
            if (Input.GetMouseButtonDown(0))
            {
                DropCube(spawnPoint);
                _cube = null;
            }
        }

    }

    private void DropCube(Vector3 position)
    {
        _cube.DropCube(_map);
        _map.CheckMatch();

    }

    private void GetNextCube()
    {
        _cube = Instantiate(_sampleCubeList[0]).GetComponent<Cube>();
    }

    private void ChangeCube()
    {
        for (int i = 0; i < _cubeBatch.GetLength(0); i++)
        {
            for (int j = 0; j < Constants.CUBE_MAXSIZE; j++)
            {
                if (!_cubeBatch[i, j])
                {
                    continue;
                }

                for (int k = 0; k < Constants.CUBE_MAXSIZE; k++)
                {
                    if (!_cubeBatch[i, k])
                    {
                        continue;
                    }
                    for (int y = 0; y < Constants.CUBE_MAXSIZE; y++)
                    {
                        if (_cubeBatch[i, y])
                        {
                            _sampleCubeList.Add(Instantiate(_cubePrefab, new Vector3(j, k, y), Quaternion.identity));
                        }
                    }
                }
            }

        }


    }

#if UNITY_EDITOR

    protected override void OnBindField()
    {
        base.OnBindField();
        _cubePrefab = Resources.Load<GameObject>("Prefabs/Cube");
        _map = GameObject.FindObjectOfType<Map>();
    }

    protected override void OnButtonField()
    {
        base.OnButtonField();
        ChangeCube();
    }
#endif
}
