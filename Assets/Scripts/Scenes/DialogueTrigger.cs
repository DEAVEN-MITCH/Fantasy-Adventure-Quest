using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueTrigger : MonoBehaviour
{
    public string node;
    private DialogueRunner dialogueRunner;
    private bool isTriggered;

    private void Awake()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isTriggered)
        {
            isTriggered = true;
            if (node != null)
                dialogueRunner.StartDialogue(node);
        }
    }
}
