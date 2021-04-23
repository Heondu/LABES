using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRotate : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed = 10;

    private float rotateTemp = 0;

    private void Awake()
    {
        rotateSpeed *= Time.deltaTime;
    }


    void Update()
    {
        GetComponent<Movement>().Rotate(rotateTemp);
        rotateTemp += rotateSpeed;
    }
}
