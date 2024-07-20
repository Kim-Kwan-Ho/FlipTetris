//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;

//public class SolarSystem : MonoBehaviour
//{
//    [HideInInspector]
//    public Planet[] planets;
//    private float[] deg;
//    [SerializeField] private float scale = 0.00002f;
//    [SerializeField] private float dist = 0.02f;
//    [SerializeField] private float rotSpeed = 1.0f;
//    [SerializeField] private float revSpeed = 1.0f;


//    private void Start()
//    {
//        planets = GetComponentsInChildren<Planet>();
//        deg = new float[planets.Length];
//        for (int i = 1; i < planets.Length; i++)
//        {
//            planets[i].transform.localScale = new Vector3(1, 1, 1) * planets[i].info.Size * scale;
//            planets[i].transform.position = planets[i].info.Target.position + (new Vector3(planets[i].info.Dist, 0, 0)) * dist;
//        }

//    }


//    private void FixedUpdate()
//    {
//        for (int i = 0; i < planets.Length; i++)
//        {
//            planets[i].transform.Rotate(0, planets[i].info.Rot * rotSpeed, 0);
//            deg[i] += Time.deltaTime * planets[i].info.Rev * revSpeed ;
//            deg[i] = Mathf.Repeat(deg[i], 360);
//            float _rad = Mathf.Rad2Deg * deg[i];
//            float _x = planets[i].info.Dist * Mathf.Sin(_rad) * dist;
//            float _z = planets[i].info.Dist * Mathf.Cos(_rad) * dist;
//            planets[i].transform.position = planets[i].info.Target.position + new Vector3(_x, 0, _z);

//        }
//    }

//}
