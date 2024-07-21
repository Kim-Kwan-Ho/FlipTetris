using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 0.5f;
    private void Update()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime < 0)
            Destroy(this.gameObject);
    }
}
