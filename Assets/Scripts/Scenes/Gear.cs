using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [Header("Check Attributes")]
    public Vector2 centerOffset;
    public Vector3 faceDir;
    public Vector2 checkSize;
    public float checkDistance;
    public LayerMask attackLayer;
    [Header("Attack Attributes")]
    public GameObject bulletPrefab;
    public float shootInterval;
    public float shootCount;
    public Vector3 bullutOffset;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        faceDir = new Vector3(spriteRenderer.flipX ? -1 : 1, 0, 0);

        if (shootCount > 0)
        {
            shootCount = Mathf.Max(shootCount - Time.deltaTime, 0);
        }
        // TODO:if the gear has found the player, it should start shooting bullets
        if (FoundPlayer())
        {
            Shoot();
        }
    }
    public bool FoundPlayer()
    {
        return Physics2D.BoxCast((Vector2)transform.position + centerOffset * faceDir, checkSize, 0, faceDir, checkDistance, attackLayer);
    }

    private void Shoot()
    {
        if (shootCount == 0)
        {
            Vector3 offset = new(spriteRenderer.flipX ? -bullutOffset.x : bullutOffset.x, bullutOffset.y, bullutOffset.z);
            Instantiate(bulletPrefab,transform.position+offset,transform.rotation);
            // Debug.Log("shot!");
            shootCount = shootInterval;
        }
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3)centerOffset, .2f);
        Gizmos.DrawWireSphere(transform.position + bullutOffset, .1f);
    }
}
