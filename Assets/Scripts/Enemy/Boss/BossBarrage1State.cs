using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*1.进入此状态时，BOSS瞬移到指定位置。
2.动作1完成后，BOSS以一定时间间隔向玩家发射可穿墙的直线射弹。射弹可被弹反。射弹碰到玩家/移动一定距离后消失。
3.发射一定数量的射弹后，BOSS生成一道向上发射的激光，并检测玩家此时在左侧或右侧；若玩家在左侧则逆时针旋转激光，反之亦然。激光不能被弹反。（旋转激光即在新的角度上再次发射激光）
4.激光旋转360度后，退出此状态。
参数包括：
瞬移位置；
发射射弹的时间间隔；
发射射弹的总数；
射弹的移动速度；
射弹的最大移动距离；
激光的宽度；
激光的长度；
激光的旋转速率（旋转时需乘以deltatime）。*/
public class BossBarrage1State : BaseState
{
    Boss boss;
    int stage;
    BossBarrage1Parameters parameters;
    float shotTimeCountdown;
    int shotCount;
    LineRenderer laser;
    bool isPlayerToTheLeft;
    float laserAngle;
    GameObject player;
    Vector3 playerOffset;
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        boss = (Boss)enemy;
        stage = 0;
        parameters = boss.GetComponent<BossBarrage1Parameters>();
        shotCount = 0;
        shotTimeCountdown = parameters.shotInterval;
        laser = boss.GetComponent<LineRenderer>();
        laserAngle = 0;
        player = GameObject.Find("player");
        playerOffset = player.GetComponent<CapsuleCollider2D>().offset;
    }
    public override void LogicUpdate()
    {
        //Debug.Log("b1 stage" + stage);
        switch (stage)
        {
            case 0:
                boss.Teleport(parameters.teleportPosition);
                stage++;
                break;
            case 1:
                if (!boss.isTeleport)
                {
                    stage++;
                }
                break;
            case 2:
                if (shotCount < parameters.shotNumber)
                {
                    shotTimeCountdown = Mathf.Max(0, shotTimeCountdown - Time.deltaTime);
                    if (shotTimeCountdown == 0)
                    {
                        ShootBarrage1();
                        shotCount++;
                        shotTimeCountdown = parameters.shotInterval;
                    }
                }
                else
                {
                    stage++;
                }
                break;
            case 3:
                InitLaser();
                stage++;
                float angle = getAnticlockwiseAngleTowardsPlayerFromUp();
                isPlayerToTheLeft = angle < 180;
                laserAngle = isPlayerToTheLeft ? 0 : 360;
                LaserAttack();
                break;
            case 4:
                if (isPlayerToTheLeft)
                {
                    laserAngle += Time.deltaTime * parameters.rotationRate;
                    if (laserAngle > 360)
                    {
                        stage++;break;
                    }
                }
                else
                {
                    laserAngle -= Time.deltaTime * parameters.rotationRate;
                    if (laserAngle < 0)
                    {
                        stage++;break;
                    }
                }
                    UpdateLaser();
                    LaserAttack();
                break;
            case 5:
                laser.enabled = false;
                stage++;
                break;
            default: boss.SwitchBossState(BossState.Wait); break;
        }
    }

    public override void OnExit()
    {
        boss.lastAttackState = BossState.Barrage1;

    }

    public override void PhysicsUpdate()
    {
    }
    void ShootBarrage1()
    {
        float angle = getAnticlockwiseAngleTowardsPlayerFromUp()+90;//because bullet by default face right
        GameObject barrage = Object.Instantiate(boss.barrage2, boss.transform.position,Quaternion.Euler(0,0,angle));
         Bullet bullet=barrage.GetComponent<Bullet>();
        bullet.flyingSpeed = parameters.shotSpeed;
        barrage.GetComponent<Attack>().attackRange = parameters.shotRange;
    }
    void InitLaser()
    {
        Vector3 endPoint = boss.transform.position + Vector3.up * parameters.laserLength;
        laser.SetPosition(0, boss.transform.position);
        laser.SetPosition(1, endPoint);
        laser.enabled = true;
        laser.endWidth = parameters.laserWidth;
        laser.startWidth = parameters.laserWidth;
    }
    void UpdateLaser()
    {
        float angleFromX = (laserAngle + 90) * Mathf.PI /180 ;
        Vector3 dir = new(Mathf.Cos(angleFromX), Mathf.Sin(angleFromX), 0);
        Vector3 endPoint = boss.transform.position + dir * parameters.laserLength;
        laser.SetPosition(0, boss.transform.position);
        laser.SetPosition(1, endPoint);
    }
    void LaserAttack()
    {
        float angleFromX = (laserAngle + 90) * Mathf.PI / 180;
        Vector3 dir = new(Mathf.Cos(angleFromX), Mathf.Sin(angleFromX), 0) ;
        RaycastHit2D[] hits = Physics2D.RaycastAll(boss.transform.position, dir, parameters.laserLength,boss.attackLayer);
        foreach (RaycastHit2D hit in hits)
        {
            GameObject hitObject = hit.transform.gameObject;
            hitObject.GetComponent<Character>().TakeDamage(parameters.laserAttack);
        }
    }
    float getAnticlockwiseAngleTowardsPlayerFromUp()
    {
        Vector3 playerPosition = playerPositionWithOffset();
        Vector3 updir =new(0, 1, 0);
        Vector3 dir = (playerPosition - boss.transform.position).normalized;
        // 计算两个向量之间的夹角
        float angle = Vector3.Angle(updir, dir);
        //since angle < 180,so we need to judge the rotation dir from the cross product
        // 使用Cross方法来计算两个向量的叉积，以确定旋转轴
        Vector3 cross = Vector3.Cross(updir, dir);
        if (cross.z < 0)
        {
            angle = 360 - angle;
        }
        // 现在，将夹角表示为Vector3欧拉角
        //Vector3 eulerAngle = new Vector3(0, 0, angle);
        return angle;
    }
    Vector3 playerPositionWithOffset()
    {
        return playerOffset + player.transform.position;
    }
}
