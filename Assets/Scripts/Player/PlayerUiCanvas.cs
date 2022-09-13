using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//DoTweenを使用するための宣言
using DG.Tweening;
public class PlayerUiCanvas : MonoBehaviour
{
    //Hpゲージ
    public Slider hpSlider;
    //スタミナゲージ
    public Slider staminaSlider;
    /// <summary>
    /// MaxのHpを設定
    /// </summary>
    /// <param name="playerController"></param>
    public void Init(PlayerController playerController)
    {
        hpSlider.maxValue = playerController.playerMaxHp;
        hpSlider.value = playerController.playerMaxHp;
        staminaSlider.maxValue = playerController.maxStamina;
        staminaSlider.value = playerController.maxStamina;
    }
    /// <summary>DoTweenを用いたHpの減少</summary>
    public void UpdateHp(float hp)
    {
        hpSlider.DOValue(hp, 0.5f);
    }
    /// <summary>DoTweenを用いたスタミナの減少</summary>
    public void UpdateStamina(float sutamina)
    {
        staminaSlider.DOValue(sutamina, 0.5f);
    }
}

