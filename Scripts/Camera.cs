using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Transform target;
    public Vector3 offset;
    [Range(2, 10)]
    public float smoothFactor;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        follow();
    }

    void follow()
    {
        Vector3 targetPos = target.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, targetPos, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothedPos;
    }
}
