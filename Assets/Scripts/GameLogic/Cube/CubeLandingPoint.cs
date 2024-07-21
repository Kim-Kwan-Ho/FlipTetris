using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLandingPoint : BaseBehaviour
{
    [SerializeField] private GameObject _explodeParticle;
    private void OnEnable()
    {
        GameSceneManager.Instance.GameSceneEvent.OnGameOver += GameOverEvent;
    }

    private void OnDisable()
    {
        GameSceneManager.Instance.GameSceneEvent.OnGameOver -= GameOverEvent;
    }

    private void GameOverEvent(GameSceneEvents gameSceneEvents)
    {
        ExplodeCube();
    }

    public void ExplodeCube()
    {
        Instantiate(_explodeParticle, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    public float CheckLandingPoint(float posX, float posZ)
    {
        RaycastHit hit;
        Physics.Raycast(transform.position + new Vector3(0.2f, -0.5f, 0.2f), Vector3.down, out hit);

        Debug.DrawRay(transform.position - Vector3.down * 0.5f, Vector3.down);

        if (hit.collider == null)
        {
            return -50;
        }
        else if (hit.transform.gameObject.layer == Constants.CUBE_SIMUL_LAYER)
        {
            return -20;
        }
        else
        {
            return hit.point.y;
        }

    }
}
