using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSimulator : BaseBehaviour
{
    [SerializeField] private GameObject[] _cubes;
    private Cube _cube;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private Map _map;
    [SerializeField] private bool _gamePlaying;
    [SerializeField] private GameObject _sampleViewPoint;
    protected override void Initialize()
    {
        base.Initialize();
        _gamePlaying = true;
    }


    private void Start()
    {
        GameSceneManager.Instance.GameSceneEvent.OnGameOver += GameOverEvent;
        GameSceneManager.Instance.GameSceneEvent.OnGameStart += GameStartEvent;
    }

    private void OnDestroy()
    {
        GameSceneManager.Instance.GameSceneEvent.OnGameOver -= GameOverEvent;
        GameSceneManager.Instance.GameSceneEvent.OnGameStart -= GameStartEvent;
    }

    private void Update()
    {
        if (!_gamePlaying || !MapController.Rotatable)
            return;

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

        }
        if (Input.GetMouseButtonDown(0))
        {
            DropCube();
        }
    }

    private void DropCube()
    {
        if (!_cube.Dropable())
            return;

        _cube.DropCube(_map);
        _cube = null;
        if (_gamePlaying)
        {
            _map.CheckMatch();
            SpawnCube();
        }
    }

    private void SpawnCube()
    {
        int c = Random.Range(0, _cubes.Length);
        _cube = Instantiate(_cubes[c]).GetComponent<Cube>();
        _cube.SetCubeView(_sampleViewPoint.transform.position);
    }

    private void GameOverEvent(GameSceneEvents gameSceneEvents)
    {
        _gamePlaying = false;
        if (_cube == null)
            return;
        _cube.DestroyCube();
        _cube = null;
    }

    private void GameStartEvent(GameSceneEvents gameSceneEvents)
    {
        SpawnCube();
        _gamePlaying = true;
    }

#if UNITY_EDITOR

    protected override void OnBindField()
    {
        base.OnBindField();
        _cubes = Resources.LoadAll<GameObject>("Prefabs/Cubes/");
        _map = GameObject.FindObjectOfType<Map>();
        _sampleViewPoint = GameObject.FindGameObjectWithTag("SampleViewPoint");
    }
#endif
}
