using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleCubeView : MonoBehaviour
{
    [SerializeField] private Vector3 _offSet;


    public void SetPosition(Vector3 position)
    {
        transform.position = position + _offSet;
    }
}