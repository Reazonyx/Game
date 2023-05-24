using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider _musicSlider;

    public void MusicVolume()
    {
        AudioManager.AudioInstance.MusicVolume(_musicSlider.value);
    }
}
