using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class NPCDialogueTrigger : MonoBehaviour
{
    private PlayerController playerController;
    private DialogueRunner dialogueRunner;
    public string node;

    private void Awake()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        playerController = other.GetComponent<PlayerController>();
        playerController.inputControl.Gameplay.InteractE.started += Talk;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        playerController = other.GetComponent<PlayerController>();
        playerController.inputControl.Gameplay.InteractE.started -= Talk;
    }

    private void Talk(InputAction.CallbackContext context)
    {
        if (node != null)
            dialogueRunner.StartDialogue(node);
    }
}
