using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusController : MonoBehaviour
{
    [SerializeField] 
    [Header("プレイヤーのステータス")]private Text[] _playerStatusText;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Damager damager;
    [SerializeField] private Guard guard;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //レべル表示
        _playerStatusText[0].text = "レベル\n" + "Lv."+GameManager.instance.PlayerLevel.ToString();
        //Hp表示
        _playerStatusText[1].text = "Hp\n" + playerController.Hp.ToString() + "/" + playerController.MaxHp.ToString();
        //スタミナ表示
        _playerStatusText[2].text = "スタミナ\n" + playerController.Stamina.ToString() + "/" + playerController.MaxStamina.ToString();
        //攻撃力表示
        _playerStatusText[3].text = "攻撃力\n" + damager.AttackDamage.ToString();
        //防御力表示
        _playerStatusText[4].text = "防御力\n" + guard.Deffend.ToString();
    }
}
