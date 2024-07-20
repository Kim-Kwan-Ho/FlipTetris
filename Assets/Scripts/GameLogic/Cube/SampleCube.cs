using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleCube : MonoBehaviour
{
    [SerializeField] private GameObject _pivot;

    public void SetRotation(Quaternion quaternion)
    {
        transform.rotation = quaternion;
    }

    public Vector3 GetPivotOffSet()
    {
        return _pivot.transform.localPosition;
    }

}
