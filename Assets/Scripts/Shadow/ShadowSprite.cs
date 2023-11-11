using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShadowSprite : MonoBehaviour
{
    private Transform player;

    private SpriteRenderer thisSprite;//当前的图像
    private SpriteRenderer playerSprite;//玩家图像

    private Color color;

    [Header("时间控制参数")]
    public float activeTime;//显示时间
    public float activeStart;//开始显示的时间点

    [Header("不透明度控制")]
    public float alpha;//根据时间不断改变的透明度
    public float alphaSet;//不透明度的初试值
    public float alphaMultiplier;//不透明度衰减系数

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        thisSprite = GetComponent<SpriteRenderer>();
        playerSprite = player.GetComponent<SpriteRenderer>();

        alpha = alphaSet;

        thisSprite.sprite = playerSprite.sprite;

        transform.position = player.position;
        transform.localScale = player.localScale;
        transform.rotation = player.rotation;

        activeStart = Time.time;
    }

    private void FixedUpdate()   //add
    {
        alpha *= alphaMultiplier;

        color = new Color(1, 1, 1, alpha);

        thisSprite.color = color;

        if(Time.time >= activeStart + activeTime)
        {
            //返回对象池
            ShadowPool.instance.ReturnPool(this.gameObject);
        }
    }
}
