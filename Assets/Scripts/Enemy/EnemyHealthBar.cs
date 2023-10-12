using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyHealthBar : MonoBehaviour
{
    public SpriteRenderer healthImage;
    public SpriteRenderer healthBackgroundImage;
    // Start is called before the first frame update
    void Start()
    {
        healthImage.enabled = false;
        healthBackgroundImage.enabled = false;
    }
    public void OnHealthChange(float percentage)
    {
        healthImage.enabled = true;
        healthBackgroundImage.enabled = true;
        healthImage.transform.localScale =new Vector3( percentage,1,1);
    }
    public void OnHealthChange(Character character)
    {
        float percentage = character.currentHealth / character.maxHealth;
        OnHealthChange(percentage);
    }

}
