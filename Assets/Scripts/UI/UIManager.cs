using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("事件监听")]
    public CharacterEventSO healthEvent;
    public PlayerStateBar playerStateBar;
    // 当对象已启用并处于活动状态时调用此函数
    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;
    }

    private void OnHealthEvent(Character arg0)
    {
        var percentage = arg0.currentHealth / arg0.maxHealth;
        playerStateBar.OnHealthChange(percentage);
    }

    // 当行为被禁用或处于非活动状态时调用此函数
    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
    }

}
