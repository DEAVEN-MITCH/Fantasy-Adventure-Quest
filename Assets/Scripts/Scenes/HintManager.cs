using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class HintManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float timer;

    private void Awake()
    {
        text.gameObject.SetActive(true);
    }
    private void Update()
    {
        if (timer < 5)
            timer += Time.deltaTime;
        else if (text.gameObject.activeSelf)
            text.gameObject.SetActive(false);
    }

    public void UpdateHint(TextMeshProUGUI newText)
    {
        if (text.gameObject.activeSelf)
        {
            text.gameObject.SetActive(false);
            timer = 0;
            text = newText;
            text.gameObject.SetActive(true);
        }
        else
        {
            timer = 0;
            text = newText;
            text.gameObject.SetActive(true);
        }
    }
}
