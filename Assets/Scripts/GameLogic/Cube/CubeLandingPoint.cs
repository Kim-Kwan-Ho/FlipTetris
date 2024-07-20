using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLandingPoint : BaseBehaviour
{
    public float CheckLandingPoint(float posX, float posZ)
    {
        RaycastHit hit;
        Physics.Raycast(transform.position - Vector3.down * 0.5f, Vector3.down, out hit);

        Debug.DrawRay(transform.position - Vector3.down * 0.5f, Vector3.down);

        if (hit.collider == null || hit.transform.gameObject.layer == Constants.CUBE_SIMUL_LAYER)
            return -20;
        else
        {
            return hit.point.y;
        }

    }
}
