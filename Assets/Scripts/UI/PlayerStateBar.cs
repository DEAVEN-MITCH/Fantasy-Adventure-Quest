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
    /// 
    /// </summary>
    /// <param name="percentage">(current/max)health</param>
    public void OnHealthChange(float percentage)
    {
        healthImage.fillAmount = percentage;
    }
    public void OnPowerChange(float percentage)
    {
        powerImage.fillAmount = percentage;
    }
    /// <summary>
    /// 
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
