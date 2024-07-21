using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleCube : BaseBehaviour
{
    [SerializeField] private MeshRenderer[] _cubesMesh;
    [SerializeField] private Material _normalMaterial;
    [SerializeField] private Material _warningMaterial;


    private void Update()
    {
        foreach (var VARIABLE in _cubesMesh)
        {
            VARIABLE.enabled = MapController.Rotatable;
        }
        foreach (var VARIABLE in _cubesMesh)
        {
            if (VARIABLE.transform.position.y >= Constants.MAP_SIZE - 0.5f)
            {
                VARIABLE.material = _warningMaterial;
            }
            else
            {
                VARIABLE.material = _normalMaterial;
            }
        }
    }


#if UNITY_EDITOR
    protected override void OnBindField()
    {
        base.OnBindField();
        _cubesMesh = GetComponentsInChildrenExceptThis<MeshRenderer>().ToArray();
    }
#endif

}
