using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossNebulaV2 : MonoBehaviour
{
    public LayerMask GroundLayer;
    public float checkRadius;
    public Vector2 leftOffset, rightOffset;
    public float flyingSpeed;
    private Attack at;
    public float destroyDelay;
    private Vector3 dir;
    public LayerMask enemyLayer;
    public Vector2 targetPoint;
    public GameObject subBulletPrefab;
    public float subBulletSpeed;
    public float subBulletRange;

    public float maxTime;
    public float existTime;
    public float maxRotate;
    private float angle;
    private GameObject player;
    private Vector3 player_dir;

    private void OnEnable()
    {
        at = GetComponent<Attack>();
        dir = transform.right;
        existTime = 0f;
        
        player = GameObject.Find("player");
    }

    void Update()
    {

    }
    private void FixedUpdate()
    {
        existTime += Time.deltaTime;
        if (existTime > maxTime)
        {
            Split();
            Destroy(this.gameObject, destroyDelay);
        }
        
        player_dir = (player.transform.position + new Vector3(0, 0.99f, 0) - transform.position).normalized;
        angle = Vector3.SignedAngle(dir, player_dir, Vector3.forward);
        if(Math.Abs(angle) > maxRotate)
            if(angle < 0) angle = -maxRotate;
            else angle = maxRotate;
        dir = (Quaternion.AngleAxis(angle, Vector3.forward)*dir).normalized;
        
        GetComponent<Rigidbody2D>().velocity = flyingSpeed * Time.deltaTime * dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!(collision.gameObject.layer == LayerMask.NameToLayer("Player")) || !(collision.GetComponent<Character>().invulnerable))
        {
            Split();
            Destroy(this.gameObject, destroyDelay);//must has delay here so that the attack's OnTriggerStay2D can work
        }
    }

    /*
        @ Description: The big nebula bullet split into 4 sub bullets.
    */
    private void Split()
    {
        GameObject bullet1 = Instantiate(subBulletPrefab, transform.position, Quaternion.Euler(0, 0, 45));
        GameObject bullet2 = Instantiate(subBulletPrefab, transform.position, Quaternion.Euler(0, 0, 135));
        GameObject bullet3 = Instantiate(subBulletPrefab, transform.position, Quaternion.Euler(0, 0, 225));
        GameObject bullet4 = Instantiate(subBulletPrefab, transform.position, Quaternion.Euler(0, 0, 315));

        bullet1.GetComponent<Bullet>().flyingSpeed = subBulletSpeed;
        bullet2.GetComponent<Bullet>().flyingSpeed = subBulletSpeed;
        bullet3.GetComponent<Bullet>().flyingSpeed = subBulletSpeed;
        bullet4.GetComponent<Bullet>().flyingSpeed = subBulletSpeed;

        bullet1.GetComponent<Attack>().attackRange = subBulletRange;
        bullet2.GetComponent<Attack>().attackRange = subBulletRange;
        bullet3.GetComponent<Attack>().attackRange = subBulletRange;
        bullet4.GetComponent<Attack>().attackRange = subBulletRange;
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
