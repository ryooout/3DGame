using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class InGameHud : MonoBehaviour
{
    [SerializeField] Button _sliderMove = default;
    [SerializeField,Header("Hpのスライダー")] Slider _hpSlider = default;
    [SerializeField, Header("スタミナのスライダー")] Slider _staminaSlider = default;
    [SerializeField] PlayerController _playerController;
    public Slider HpSlider { get => _hpSlider; set => _hpSlider = value; }
    public Slider StaminaSlider { get => _staminaSlider; set => _staminaSlider = value; }
    public PlayerController PlayerController { get => _playerController; set => _playerController = value; }
    void Start()
    {
        _sliderMove.onClick.AddListener(() =>
        {

        });
    }
}
