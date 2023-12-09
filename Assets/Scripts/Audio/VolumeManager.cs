using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VolumeManager : MonoBehaviour
{
    private void Update()
    {
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value * 2;
        Debug.Log(AudioListener.volume);
    }
}
