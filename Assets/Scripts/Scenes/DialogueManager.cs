using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueManager : MonoBehaviour
{
    public GameObject player;
    public DialogueRunner dialogueRunner;

    private void Awake()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
            SkipDialogue();
    }

    // YarnSpinner中的对话开始时调用
    [YarnCommand("StartDialogue")]
    public void StartDialogue()
    {
        player.GetComponent<PlayerController>().inputControl.Disable();
        player.GetComponent<Character>().invulnerable = true;
        player.GetComponent<Character>().invulnerableCounter = 1000;
    }

    // YarnSpinner中的对话结束时调用
    [YarnCommand("EndDialogue")]
    public void EndDialogue()
    {
        player.GetComponent<PlayerController>().inputControl.Enable();
        player.GetComponent<Character>().invulnerable = false;
        player.GetComponent<Character>().invulnerableCounter = 0;
    }

    public void SkipDialogue()
    {
        Debug.Log("I Want to Skip!");
        dialogueRunner.Stop();
        EndDialogue();
    }
}
