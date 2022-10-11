using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusController : MonoBehaviour
{
    [SerializeField] 
    [Header("�v���C���[�̃X�e�[�^�X")]private Text[] _playerStatusText;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Damager damager;
    [SerializeField] private Guard guard;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //���׃��\��
        _playerStatusText[0].text = "���x��\n" + "Lv."+GameManager.instance.PlayerLevel.ToString();
        //Hp�\��
        _playerStatusText[1].text = "Hp\n" + playerController.Hp.ToString() + "/" + playerController.MaxHp.ToString();
        //�X�^�~�i�\��
        _playerStatusText[2].text = "�X�^�~�i\n" + playerController.Stamina.ToString() + "/" + playerController.MaxStamina.ToString();
        //�U���͕\��
        _playerStatusText[3].text = "�U����\n" + damager.AttackDamage.ToString();
        //�h��͕\��
        _playerStatusText[4].text = "�h���\n" + guard.Deffend.ToString();
    }
}
