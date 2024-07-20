using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapController : BaseBehaviour
{
    [SerializeField] private int _targetAngle = 90;
    private bool _rotatable;
    [SerializeField] private GameObject[] _walls;
    [SerializeField] private Map _map;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Initialize()
    {
        base.Initialize();
        _rotatable = true;
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

    private void Update()
    {
        if (!_rotatable)
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
        _rotatable = false;
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
        _rotatable = true;
    }

    public void StackCube(Cube cube)
    {

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
