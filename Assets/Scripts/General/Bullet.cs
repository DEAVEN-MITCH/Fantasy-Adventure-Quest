using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool touchLeftWall;
    public bool touchRightWall;
    public LayerMask GroundLayer;
    public float checkRadius;
    public Vector2 leftOffset, rightOffset;
    public float flyingSpeed;
    public bool isShotToLeft;
    public float powerConsumption;
    public Vector2 originalOffset;
    private Vector3 originalPosition;
    private GameObject player;
    private Attack at;
    public float destroyDelay;
    // Start is called before the first frame update
    private void OnEnable()
    {
        player = GameObject.Find("player");
        isShotToLeft = player.GetComponent<SpriteRenderer>().flipX;
        originalOffset = new (isShotToLeft ? -originalOffset.x : originalOffset.x, originalOffset.y);
        originalPosition=transform.position = player.transform.position + (Vector3)originalOffset;
        Check();
        at = GetComponent<Attack>();
        //Debug.Log("enable!");
    }
    private void Check() { 
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRadius, GroundLayer);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRadius, GroundLayer);
    }
    // Update is called once per frame
    void Update()
    {
       
        Check();
    }
    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, originalPosition) >= at.attackRange||touchLeftWall||touchRightWall) { /*Debug.Log("destroy!");*/ Destroy(this.gameObject); }
        Vector3 dir = new(isShotToLeft ? -1 : 1, 0, 0);
        GetComponent<Rigidbody2D>().velocity = flyingSpeed * Time.deltaTime * dir;
    
        //Debug.Log(player.GetComponent<Rigidbody2D>().velocity + "player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Destroy in 0.05s!");
        Destroy(this.gameObject,destroyDelay);//must has delay here so that the attack's OnTriggerStay2D can work
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRadius);
    }
}
