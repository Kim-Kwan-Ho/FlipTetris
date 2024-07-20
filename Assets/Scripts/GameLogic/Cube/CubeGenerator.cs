using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : BaseBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private GameObject _cube;
    [SerializeField] private Vector3 _cubePos;

    private void GenerateCubeParent()
    {
        if (_cube == null)
        {
            _cube = new GameObject("Cube");
            _cube.transform.position = Vector3.zero;
            _cube.AddComponent<Cube>();
        }
    }
    private void GenerateCube()
    {
        GameObject go = Instantiate(_cubePrefab, _cubePos, Quaternion.identity);
        go.transform.SetParent(_cube.transform);
        go.AddComponent<CubeLandingPoint>();
    }


#if UNITY_EDITOR
    protected override void OnBindField()
    {
        base.OnBindField();
        _cubePrefab = Resources.Load<GameObject>("Prefabs/Cube");
    }

    protected override void OnButtonField()
    {
        base.OnButtonField();
        GenerateCubeParent();
        GenerateCube();
    }   
#endif
}
