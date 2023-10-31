using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Attack Attributes")]
    public int damage;
    public float attackRange, attackRate;
    [Header("Status")]
    public AbnormalStatus[] statusApply;
    public float[] statusDuration;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.GetComponent<Character>()?.TakeDamage(this);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
