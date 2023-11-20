using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossStar : MonoBehaviour
{
    public float checkRadius;
    public Vector2 leftOffset, rightOffset;
    private Vector3 dir;
    public LayerMask enemyLayer;

    [Header("time")]
    public float maxTime;
    public float existTime;
    private void OnEnable()
    {
        existTime = 0f;
        //Debug.Log("enable!");
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        existTime += Time.deltaTime;
        if (existTime > maxTime) Destroy(this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRadius);
    }

    public void ReboundedByPlayer()
    {
    }
}
