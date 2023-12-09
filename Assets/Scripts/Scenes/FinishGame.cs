using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FinishGame : MonoBehaviour
{
    public float waitTime;
    private float currentTime;

    private void Start() {
        currentTime = 0f;
    }

    private void FixedUpdate()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= waitTime)
            SceneManager.LoadScene("FinishGame");
        //Debug.Log("hit the Exit");
    }
}
