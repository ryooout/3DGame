using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//DoTween���g�p���邽�߂̐錾
using DG.Tweening;
public class PlayerUiCanvas : MonoBehaviour
{
    //Hp�Q�[�W
    public Slider hpSlider;
    //�X�^�~�i�Q�[�W
    public Slider staminaSlider;
    /// <summary>
    /// Max��Hp��ݒ�
    /// </summary>
    /// <param name="playerController"></param>
    public void Init(PlayerController playerController)
    {
        hpSlider.maxValue = playerController.playerMaxHp;
        hpSlider.value = playerController.playerMaxHp;
        staminaSlider.maxValue = playerController.maxStamina;
        staminaSlider.value = playerController.maxStamina;
    }
    /// <summary>DoTween��p����Hp�̌���</summary>
    public void UpdateHp(float hp)
    {
        hpSlider.DOValue(hp, 0.5f);
    }
    /// <summary>DoTween��p�����X�^�~�i�̌���</summary>
    public void UpdateStamina(float sutamina)
    {
        staminaSlider.DOValue(sutamina, 0.5f);
    }
}

