using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HintTrigger : MonoBehaviour
{
    public TextMeshProUGUI hintText;
    public bool isTriggered;
    public UnityEvent<TextMeshProUGUI> onHintTrigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isTriggered)
        {
            isTriggered = true;
            onHintTrigger.Invoke(hintText);
        }
    }
}
