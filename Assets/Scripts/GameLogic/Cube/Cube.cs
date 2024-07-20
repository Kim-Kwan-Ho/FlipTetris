using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Cube : BaseBehaviour
{
    [SerializeField] private GameObject _cubeSample;
    [SerializeField] private CubeLandingPoint[] _childrens;
    private GameObject _instSample;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
    }


    protected override void Initialize()
    {
        base.Initialize();
        transform.position += new Vector3(0, Constants.CUBE_DROP_HEIGHT, 0);
        foreach (var VARIABLE in _childrens)
        {
            VARIABLE.gameObject.layer = Constants.CUBE_SIMUL_LAYER;
        }

        _instSample = Instantiate(_cubeSample, new Vector3(0, Constants.CUBE_DROP_HEIGHT, 0), Quaternion.identity);
    }


    public void CheckLandingPoint(Vector3 position)
    {
        float yPos = float.MinValue;

        transform.position = new Vector3(position.x, Constants.CUBE_DROP_HEIGHT, position.z);
        foreach (var VARIABLE in _childrens)
        {
            if (VARIABLE.CheckLandingPoint(position.x, position.z) <= -40)
            {
                yPos = -50;
                break;
            }
            if (VARIABLE.CheckLandingPoint(position.x, position.z) > -10f)
            {
                yPos = Math.Max(yPos, VARIABLE.CheckLandingPoint(position.x, position.z));
            }
        }

        if (yPos <= -20)
        {
            return;
        }
        yPos = (float)Math.Round(yPos, 2);
        _instSample.transform.position = new Vector3(transform.position.x, yPos + 0.5f, transform.position.z);
    }
    public void DropCube(Map map)
    {
        transform.position = _instSample.transform.position;
        foreach (var VARIABLE in _childrens)
        {
            VARIABLE.gameObject.layer = 0;
            map.SetCube(VARIABLE.transform);
        }
        Destroy(_instSample);
        Destroy(this.gameObject);
    }


#if UNITY_EDITOR
    protected override void OnBindField()
    {
        base.OnBindField();
        _childrens = GetComponentsInChildrenExceptThis<CubeLandingPoint>().ToArray();
    }
#endif

}