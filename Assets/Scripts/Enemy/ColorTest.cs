using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTest : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer sr;
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        Debug.Log("wake");
    }

    // Update is called once per frame
    public void ChangeAlpha()
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - 0.2f);
    }
}
