using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bee : Enemy
{
    [Header("Bee Parameters")]
    // This is the bound of this enemy's movement range.
    public BoxCollider2D beeBound;
    // The detection circle. When player is within this circle, the bee finds it.
    public CircleCollider2D detectionCircle;
    // We use this filter to find player.
    public ContactFilter2D contactFilter2D = new();
    // The bullet it shoots
    public GameObject bulletPrefab;
    // All the players the bee finds
    public Collider2D[] players = new Collider2D[1];
    // An offset to modify the bullet's original position.
    public Vector3 bulletOffset;
    // Attack component
    public Attack attack;
    // Counter. when drops to 0, generate a bullet
    public float shootCount;
    // A fixed period of time as the interval between every two bullets
    public float shootInterval;
    /// <summary>
    /// A limitation for random move. 
    /// If the distance between the chosen point and original position is shorter than this distance,
    /// then this chosen point is not available.
    /// </summary>
    public float minimumDistance;
    // used for recording time
    public float lastTime;
    // The interval for each random movement
    public float moveInterval;
    // The interval when gets hit
    public float stopInterval;
    // This is used for drawing out the destination
    public Vector2 targetPoint;
    // indicating whether arrives
    public bool arrived;
    // indicating whether stops (after hurt)
    public bool isStop;
    // A time counter recording when start stopping
    public float stopTime;

    public LayerMask groundLayer;
    override protected void Awake()
    {
        patrolState = new BeePatrolState();
        chaseState = new BeeChaseState();
        base.Awake();
        contactFilter2D.SetLayerMask(attackLayer);
        attack = GetComponent<Attack>();
        lastTime = Time.time;
        arrived = true;
        character = GetComponent<Character>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (isStop)
        {
            if (Time.time - stopTime > stopInterval)
            {
                isStop = false;
                if (!arrived)
                    rb.velocity = (targetPoint - (Vector2)transform.position).normalized * currentSpeed;
            }
        }
        if (Vector2.Distance(targetPoint, transform.position) < 0.1 && rb.velocity != Vector2.zero)
        {
            arrived = true;
            rb.velocity = Vector2.zero;
            // TODO: when the bee arrives, reset to count time
            lastTime = Time.time;
        }
    }

    public override void Move()
    {
        if (Time.time - lastTime < moveInterval || !arrived || isStop)
            return;
        else
            lastTime = Time.time;       // This is essential. Otherwise, each frame will arbitrarily pick a target point.
        // Debug.Log("Move!");
        Vector2 center = (Vector2)beeBound.transform.position + beeBound.offset;
        Vector2 size = beeBound.size;
        // point1 is the left bottom point, point2 is the right upper point.
        // we can use the two points to arbitrarily choose a point as destination.
        Vector2 point1 = center - 0.5f * size, point2 = center + 0.5f * size;
        int count = 0;
        while (count++ < 1000)
        {
            float randomX = Random.Range(point1.x, point2.x);
            float randomY = Random.Range(point1.y, point2.y);

            targetPoint = new(randomX, randomY);
            // TODO: check if the point is available
            // TODO: 1. not out of bounds (This is already ensured)
            // TODO: 2. is longer than minimum distance
            Vector2 direction = (targetPoint - (Vector2)transform.position).normalized;
            float distance = Vector2.Distance(targetPoint, transform.position);
            if (distance <= minimumDistance)
                continue;
            else
            {
                rb.velocity = direction * currentSpeed * character.speedCorrection;
                arrived = false;
                break;
            }
        }
    }

    public override void TakeDamage(Transform attackTrans)
    {
        attacker = attackTrans;
        anim.SetTrigger("hurt");

        // TODO: when gets hurt, immediately reset shoot counter.
        shootCount = shootInterval;
        // TODO: and stop at the original place
        rb.velocity = Vector2.zero;
        isStop = true;
        stopTime = Time.time;
    }

    public override bool FoundPlayer()
    {
        if (detectionCircle.OverlapCollider(contactFilter2D, players) > 0)
        {
            // TODO: Cast a ray from the bee to the player
            // TODO: The length of this ray should be the smaller of radius of detection circle,
            // TODO: and the distance between the player and the bee
            if (Physics2D.Raycast(
                transform.position,
                (players[0].transform.position - transform.position).normalized,
                Mathf.Min(detectionCircle.radius, Vector2.Distance(players[0].transform.position, transform.position)),
                groundLayer))
            {
                // Debug.Log("hits a wall!");
                return false;
            }
            return true;
        }
        return false;
    }

    // used to help find the minimum distance of movement
    override protected void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, minimumDistance);
        Gizmos.DrawWireSphere(targetPoint, 1);
        if (players[0] != null)
        {
            Vector2 hitWallPoint = Physics2D.Raycast(
                transform.position,
                (players[0].transform.position - transform.position).normalized,
                Mathf.Min(detectionCircle.radius, Vector2.Distance(players[0].transform.position, transform.position)),
                groundLayer).point;

            if(Vector2.Distance(players[0].transform.position, transform.position) <= detectionCircle.radius)
            {
                Vector2 hitPoint = (hitWallPoint == Vector2.zero) ? players[0].transform.position : hitWallPoint;
                Gizmos.DrawLine(transform.position, hitPoint);
            }
        }
    }
}
