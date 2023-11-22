using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*1.进入此状态时，BOSS瞬移到指定位置。
2.动作1完成后，BOSS以一定时间间隔在玩家的某个相对位置处（y方向相对位置固定；x方向相对位置在一定范围内随机选择）生成一块岩石（属于不可弹反、可穿墙的射弹）。
3.岩石在生成后一定时间内保持静止，随后向下移动（速度固定），即使撞击到玩家也不消失；y坐标小于一定数值时才消失。
4.岩石在生成时有一定概率使其移动速度减半，也有一定概率使其移动速度翻倍。两者互斥。
5.生成一定数量的岩石后，BOSS停止行动一段时间，随后退出此状态。
参数包括：
瞬移位置；
生成岩石的时间间隔；
生成岩石的总数；
岩石到玩家的相对高度；
岩石到玩家的x轴相对距离的范围；
岩石的静止时间；
岩石的基础移动速度；
岩石速度减半的概率；
岩石速度翻倍的概率；
生成岩石结束后的休息时间；*/
public class BossRockFallParameters : MonoBehaviour
{
    public float rockInterval,rockRelativeHeight,horizontalLeftBound,horizontalRightBound,stillTime,baseSpeed,halfSpeedPrbability,restTime;
    public int rockNumber;
    public Vector2 teleportPosition;
}
