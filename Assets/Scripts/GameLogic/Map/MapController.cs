using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapController : BaseBehaviour
{
    [SerializeField] private int _targetAngle = 90;
    public static bool Rotatable;
    [SerializeField] private GameObject[] _walls;
    [SerializeField] private Map _map;


    protected override void Initialize()
    {
        base.Initialize();
        Rotatable = true;
        foreach (var VARIABLE in _walls)
        {
            if (VARIABLE.transform.position.y > -0.4f)
            {
                VARIABLE.GetComponent<Collider>().enabled = false;
            }
            else
            {
                VARIABLE.GetComponent<Collider>().enabled = true;
            }
        }
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
    private void GameOverEvent(GameSceneEvents gameSceneEvents)
    {
        Rotatable = false;
    }

    private void GameStartEvent(GameSceneEvents gameSceneEvents)
    {
        Initialize();
    }
    private void Update()
    {
        if (!Rotatable)
            return;


        if (Input.GetKeyDown(PlayerKey.RotateFront))
        {
            StartCoroutine(CoRotateMap(Vector3.right));
        }
        else if (Input.GetKeyDown(PlayerKey.RotateRight))
        {
            StartCoroutine(CoRotateMap(Vector3.forward));
        }
        else if (Input.GetKeyDown(PlayerKey.RotateLeft))
        {
            StartCoroutine(CoRotateMap(-Vector3.forward));
        }
        else if (Input.GetKeyDown(PlayerKey.RotateBack))
        {
            StartCoroutine(CoRotateMap(-Vector3.right));
        }
    }

    private IEnumerator CoRotateMap(Vector3 axis)
    {
        Rotatable = false;
        int curAngle = 0;

        while (curAngle < _targetAngle)
        {
            transform.Rotate(axis, 1, Space.World);
            curAngle++;
            yield return null;
        }
        _map.ChangeMapDirection();
        yield return new WaitForSeconds(Constants.VIEW_CHANGE_TIME);

        foreach (var VARIABLE in _walls)
        {
            if (VARIABLE.transform.position.y > -0.4f)
            {
                VARIABLE.GetComponent<Collider>().enabled = false;
            }
            else
            {
                VARIABLE.GetComponent<Collider>().enabled = true;
            }
        }
        Rotatable = true;
    }


#if UNITY_EDITOR
    protected override void OnBindField()
    {
        base.OnBindField();
        _walls = GameObject.FindGameObjectsWithTag("Wall");
        _map = FindObjectOfType<Map>();
    }
#endif

}
