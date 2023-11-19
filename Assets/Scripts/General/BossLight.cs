using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossLight : MonoBehaviour
{
    public float checkRadius;
    public Vector2 leftOffset, rightOffset;
    private Vector3 dir;
    public LayerMask enemyLayer;

    public float flyingSpeed;
    public float angle;
    public float rotateSpeed;
    public float rotateReductionRate;

    public float maxTime;
    public float existTime;
    public float rotateTime;
    private void OnEnable()
    {
        existTime = 0f;
        rotateSpeed = 0f;
        dir = new Vector3((float)Math.Sin(angle), (float)Math.Cos(angle), 0);
        //Debug.Log("enable!");
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        existTime += Time.deltaTime;
	Debug.Log(existTime);
        if (existTime > maxTime) Destroy(this.gameObject);
        GetComponent<Rigidbody2D>().velocity = flyingSpeed * Time.deltaTime * dir;
        
        //rotate
        angle += rotateSpeed * Time.deltaTime;
        dir = new Vector3((float)Math.Sin(angle), (float)Math.Cos(angle), 0);
        rotateTime += Time.deltaTime;
        if (rotateTime > 0.1f)
        {
            rotateTime -= 0.1f;
            rotateSpeed *= rotateReductionRate;
        }
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
