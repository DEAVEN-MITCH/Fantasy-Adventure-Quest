using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEye : Enemy
{
    public float attackInterval;
    public float attackCount;
    public float preAttackInterval;
    public float preAttackCount;
    public bool isPreAttack;
    public float attackDistance;//attack ray length the same as detection radius
    public bool hurtSignal;//to trigger some steps once hurt , in comparison with isHurt,isHurt is a lasting state
    Collider2D[] players = new Collider2D[1];//the players in detection
    ContactFilter2D contactFilter2D = new();
    public CircleCollider2D detectionCircle;
    public BoxCollider2D bounds;//boundary of the center of the RandomMove destination
    [Header("Attack Attributes")]
    public Color lineColor = Color.red; //color of the indicative line 
    private LineRenderer lineRenderer;
    private Vector3 attackDir;
    private Attack attack;
    public float indicativeLineWidth,attackLineWidth;
    public float maxHeight, minHeight, minRandomDistanceToWall;
    public  LayerMask groundLayer;
    public Vector2[] pointsForMovememt;
    public Attack rayAttack;
    public float attackRayDiaplayDuration;

    protected override void Awake()
    {
        patrolState = new FloatingEyePatrolState();
        base.Awake();
        BoxCollider2D b2 = GetComponent<BoxCollider2D>();
        CapsuleCollider2D c2 = GetComponent<CapsuleCollider2D>();
        b2.offset = new Vector2(0, c2.offset.y - c2.size.y / 2 + b2.size.y / 2);
        skillState = new FloatingEyeSkillState();
        patrolState = new FloatingEyePatrolState();
        currentSpeed = 0;
        //attackDistance = ci2.radius;
        contactFilter2D.SetLayerMask(attackLayer);
        attack = GetComponent<Attack>();
    }
    void Start()
    {
        // create LineRenderer component
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.startWidth =indicativeLineWidth; // set the line width
        lineRenderer.endWidth = indicativeLineWidth;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
        lineRenderer.enabled = false;
       
    }
    public override void TakeDamage(Transform attackTrans)
    {
        attacker = attackTrans;
        isHurt = true;
       anim.SetTrigger("hurt");
        hurtSignal = true;
        //no force considering the randommove
        //Vector3 dir = (transform.position-attackTrans.position).normalized;
        //rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        
        //RandomMove();not called here in case the Attack ray moves before it is destroyed

    }

    public void RandomMove()
    {
        //
        //StartCoroutine(AsynRandomMove());
        RandomMoveAmongPoints();
        
    }

    internal void DePreAttack()
    {
        lineRenderer.enabled = false ;
        //destory the indicative ray
    }

    public void PreAttack()
    {
        Vector3 playerPosition = players[0].GetComponent<Transform>().position;
        playerPosition += new Vector3(0, 0.95f, 0);//人物中心点偏移
        attackDir = (playerPosition - transform.position).normalized;
        // 设置线的位置
        Vector3 endPoint = transform.position + attackDir * attackDistance;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, endPoint);
        lineRenderer.enabled = true;
        //Debug.Log("preattack!");
        //locate current player's position and make a ray to indicate the attack
        //the attack range is attackDistance
    }

    public override bool FoundPlayer()
    {

          return detectionCircle.OverlapCollider(contactFilter2D, players)>0;//OverlapCollider return the number of colliders 
    }
    public void Attack()
    {
        StartCoroutine(displayAttack());
        //make an attack
        //collision.GetComponent<Character>()?.TakeDamage(this);
    }
    IEnumerator displayAttack()
    {
        //lineRenderer.enabled = false;
        lineRenderer.endWidth = lineRenderer.startWidth = attackLineWidth;
        //yield return null;
        //lineRenderer.enabled = true;

        // 发射射线

        //RaycastHit[] hits = Physics.SphereCastAll(transform.position, attackLineWidth/2, attackDir, attackDistance, attackLayer);
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, attackDir, attackDistance, attackLayer);
        //Debug.Log(transform.position);
        //Debug.Log(hits.Length.ToString()+attackDir.ToString()+attackDistance.ToString()+attackLayer.ToString());
        // 遍历接触到的物体
        foreach (RaycastHit2D hit in hits)
        {
            GameObject hitObject = hit.transform.gameObject;
            // 在这里处理接触到的物体，例如打印名称或执行其他操作
            //Debug.Log("接触到物体：" + hitObject.name);
            hitObject.GetComponent<Character>().TakeDamage(rayAttack);
        }
        yield return new WaitForSeconds(attackRayDiaplayDuration);
        lineRenderer.endWidth = lineRenderer.startWidth=indicativeLineWidth;
        //Debug.Log("reset linerenderer");
        lineRenderer.enabled = false;
        yield break;
    }
    IEnumerator AsynRandomMove()
    {
        Vector2 center = (Vector2)bounds.transform.position+bounds.offset;
        Vector2 size = bounds.size;
        Vector2 point1 = center - 0.5f* size,point2=center+0.5f*size;
        // 生成随机的x坐标和y坐标，确保它们在point1和point2范围内
        int count = 0;
        //max loop=1000 to avoid dead loop
        Vector2 checkArea = new Vector2(4, 4);//the rectangular size of the destination check area
        while (++count < 1000)
        {
            float randomX = UnityEngine.Random.Range(point1.x, point2.x);
            float randomY = UnityEngine.Random.Range(point1.y, point2.y);

            // 创建Vector2表示随机点
            Vector2 randomPoint = new Vector2(randomX, randomY);
            var curHeight = currentHeightAboveTheGround(randomPoint);
            //check the distance to the ground
            if (curHeight > maxHeight || curHeight < minHeight) continue;
            //check whether up to the minRandomDistanceToWall
            if (distanceToTheNearestWall(randomPoint) < minRandomDistanceToWall) continue;
            //check whether the destination will overlap with Ground
            if ( Physics2D.OverlapArea(randomPoint-.5f*checkArea, randomPoint+.5f*checkArea, groundLayer)){
                continue;
            }
            //every check is fine ,let's move;
            transform.position = randomPoint;
            break;
        }
        yield break;
    }
    public  float currentHeightAboveTheGround(Vector2 position)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(position, Vector3.down, 100, groundLayer);//current detection set distance to 100 which is enough now
        float distance = 100;//current set to 100 which is enough
        foreach (RaycastHit2D hit in hits)
        {
            distance = Mathf.Min(distance, hit.distance);
        }
        //Debug.Log(String.Format("position : {1}the currentHeight{0},hits size{2}", distance,position,hits.Length));
        return distance;
    }
    float distanceToTheNearestWall(Vector2 position)
    {
        float distance = 100;//current set to 100 which is enough

        Collider2D[] results= Physics2D.OverlapCircleAll(position, distance, groundLayer);
        

            foreach(Collider2D co in results)
            {
            Vector2 closestPoint = co.ClosestPoint(position);
            distance = Mathf.Min(distance, Vector2.Distance(closestPoint, position));
            }
        return distance;
    }
    private void RandomMoveAmongPoints()
    {
        int size = pointsForMovememt.Length;
        int randomIndex = UnityEngine.Random.Range(0, size);
        transform.position = pointsForMovememt[randomIndex];
        return;
    }
}
