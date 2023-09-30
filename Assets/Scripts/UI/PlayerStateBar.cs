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
    /// 血量随百分比变化
    /// </summary>
    /// <param name="percentage">(current/max)health</param>
    public void OnHealthChange(float percentage)
    {
        healthImage.fillAmount = percentage;
    }

    /// <summary>
    /// 假如delay慢于生命值变化，则显示延迟更新效果，如果血量回复，立即让delay保持一致
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
