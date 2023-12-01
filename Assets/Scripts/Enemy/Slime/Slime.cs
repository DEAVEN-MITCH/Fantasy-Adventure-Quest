using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slime : Enemy
{
    [Header("Slime Bound")]
    // This is the bound of this enemy's movement range.
    public Collider2D slimeBound;
    // last time jumped
    [Header("Jump Parameters")]
    public float jumpTime;
    // The interval between each two jumps
    // notice this interval starts from when slime lands from a jump
    public float jumpInterval;
    // This is current jump force
    public float jumpForce;
    // normal jump force(Patrol State)
    public float jumpNormalForce;
    // enhanced jump force(Chase State)
    public float jumpEnhancedForce;
    // This is a helper boolean variable for jump controlling
    public bool isLand;
    public bool newBorn;

    [Header("Prefabs")]
    // This sub slime should jump higher
    public GameObject blueSlimePrefab;
    public Vector2 blueSlimeOffset;
    // This one should move faster but jump slower
    public GameObject greenSlimePrefab;
    public Vector2 greenSlimeOffset;
    public bool isSplitable;

    override protected void Awake()
    {
        patrolState = new SlimePatrolState();
        chaseState = new SlimeChaseState();
        isLand = true;
        newBorn = true;
        jumpTime = Time.time;
        base.Awake();
    }

    public override void Move()
    {
        // ? fixed: cannot overturn itself
        transform.rotation = Quaternion.identity;
        // ? fixed: cannot jump and move after dying
        if (isDead)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        // TODO: move horizontally before landing
        /* 
            ? fixed: when big slime killed in jumping
            ? fixed: newborn sub slimes will not be treated as jumping
        */
        if (newBorn)
            rb.velocity = new Vector2(0, rb.velocity.y);
        if (pc.isGround)
            newBorn = false;
        
        if (!pc.isGround)
            rb.velocity = new Vector2(character.speedCorrection * currentSpeed * faceDir.x * Time.deltaTime, rb.velocity.y);
        else if (!isLand)
        {
            isLand = true;
            jumpTime = Time.time;
            // ? fixed: now cannot slip after jumping
            rb.velocity = Vector2.zero;
        }

        if (Time.time - jumpTime <= jumpInterval)
            return;
        else if (isLand)
        {
            isLand = false;
            jumpTime = Time.time;
            // TODO: jump upforward
            Vector2 direction = Vector2.up;
            rb.AddForce(direction * jumpForce, ForceMode2D.Impulse);
        }
    }

    public override void Die()
    {
        // TODO: generate two sub slimes
        if (isSplitable)
        {
            GameObject blue = Instantiate(blueSlimePrefab, transform.position + (Vector3)blueSlimeOffset, transform.rotation);
            GameObject green = Instantiate(greenSlimePrefab, transform.position + (Vector3)greenSlimeOffset, transform.rotation);

            Slime blueSlime = blue.GetComponent<Slime>();
            Slime greenSlime = green.GetComponent<Slime>();
            // TODO: now inherit parent slime's bound correctly
            blueSlime.slimeBound = slimeBound;
            greenSlime.slimeBound = slimeBound;

            // ? fixed: new born slimes should be invulnerable for a period of time
            blueSlime.GetComponent<Character>().TriggerInvulnerable();
            greenSlime.GetComponent<Character>().TriggerInvulnerable();
        }
        base.Die();
    }
}
