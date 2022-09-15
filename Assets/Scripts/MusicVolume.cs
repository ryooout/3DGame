using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MusicVolume : MonoBehaviour
{
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider seSlider;
    void Start()
    {
        AudioManager.GetInstance().PlayBGM(0);
    }
    public void OnChangedBGMSlider()
    {
        AudioManager.GetInstance().BGMVolume = bgmSlider.value;
    }
    public void OnChangedSESlider()
    {
        AudioManager.GetInstance().SEVolume = seSlider.value;
    }
    public void OnTestSEbutton()
    {
        AudioManager.GetInstance().PlaySound(0);
    }
}
