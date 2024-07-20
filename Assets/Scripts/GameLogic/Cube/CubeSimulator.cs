using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSimulator : BaseBehaviour
{
    [SerializeField] private GameObject[] _cubes;
    private Cube _cube;
    [SerializeField] private bool[,] _cubeBatch = new bool[3, Constants.CUBE_MAXSIZE];
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
        ChangeCube();
    }

    private void ChangeCube()
    {
        int c = Random.Range(0, _cubes.Length);
        _cube = Instantiate(_cubes[c]).GetComponent<Cube>();
    }

#if UNITY_EDITOR

    protected override void OnBindField()
    {
        base.OnBindField();
        _cubes = Resources.LoadAll<GameObject>("Prefabs/Cubes/");
        _map = GameObject.FindObjectOfType<Map>();
    }

    protected override void OnButtonField()
    {
        base.OnButtonField();
        ChangeCube();
    }
#endif
}
