using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("�¼�����")]
    public CharacterEventSO healthEvent;
    public PlayerStateBar playerStateBar;
    // �����������ò����ڻ״̬ʱ���ô˺���
    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;
    }

    private void OnHealthEvent(Character arg0)
    {
        var percentage = arg0.currentHealth / arg0.maxHealth;
        playerStateBar.OnHealthChange(percentage);
    }

    // ����Ϊ�����û��ڷǻ״̬ʱ���ô˺���
    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
    }

}
