using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorBall : MonoBehaviour
{
    private Transform mirrorBall;

    private void Start()
    {
        mirrorBall = GetComponent<Transform>();
    }
    private void Update()
    {
        mirrorBall.Rotate(Vector3.up, 300 * Time.deltaTime);
    }
}
