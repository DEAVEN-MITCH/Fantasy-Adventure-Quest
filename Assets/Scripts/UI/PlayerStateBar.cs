using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStateBar : MonoBehaviour
{
    public Image healthImage;
    public Image healthDelayImage;
    public Image powerImage;
    /// <summary>
    /// Ѫ����ٷֱȱ仯
    /// </summary>
    /// <param name="percentage">(current/max)health</param>
    public void OnHealthChange(float percentage)
    {
        healthImage.fillAmount = percentage;
    }

    /// <summary>
    /// ����delay��������ֵ�仯������ʾ�ӳٸ���Ч�������Ѫ���ظ���������delay����һ��
    /// </summary>
    private void Update()
    {
        if (healthDelayImage.fillAmount > healthImage.fillAmount)
        {
            healthDelayImage.fillAmount -= Time.deltaTime;
        }
        if (healthDelayImage.fillAmount < healthImage.fillAmount)
        {
            healthDelayImage.fillAmount = healthImage.fillAmount;
        }
    }
}
