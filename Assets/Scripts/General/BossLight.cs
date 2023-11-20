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
    public GameObject starPrefab;

    [Header("speed and rotation")]
    public float flyingSpeed;
    public float angle;
    public float rotateSpeed;
    public float rotateReductionRate;

    [Header("time")]
    public float maxTime;
    public float generateTime;
    public float existTime;
    public float rotateTimer;
    public float generateTimer;
    private void OnEnable()
    {
        existTime = 0f;
        rotateTimer = 0f;
        generateTimer = 0f;
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
        if (existTime > maxTime) Destroy(this.gameObject);
        GetComponent<Rigidbody2D>().velocity = flyingSpeed * Time.deltaTime * dir;
        
        //rotate
        angle += rotateSpeed * Time.deltaTime;
        dir = new Vector3((float)Math.Sin(angle), (float)Math.Cos(angle), 0);
        rotateTimer += Time.deltaTime;
        if (rotateTimer > 0.1f)
        {
            rotateTimer -= 0.1f;
            rotateSpeed *= rotateReductionRate;
        }

        //generateStar
        generateTimer += Time.deltaTime;
        if (generateTimer > generateTime)
        {
            UnityEngine.Object.Instantiate(starPrefab, this.transform.position, Quaternion.identity);
            generateTimer -= generateTime;
            generateTime += 0.25f;
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
