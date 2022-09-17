using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Button[] _shopButtons;
    [SerializeField] private Text[] _levelUpTexts;
    [SerializeField] GameObject _playerSword = default;
    [SerializeField] Damager damager;
    Vector3 swordScale;
    int[] level = new int[4] {1,1,1,1 };
    private void Awake()
    {
        swordScale = _playerSword.transform.localScale;
    }
    private void Start()
    {
        Generate();
        
    }
    private void Update()
    {
                
           
    }
   void Generate()
    {
        //射程増加ボタン
        _shopButtons[0].onClick.AddListener(() =>
        {
            swordScale.y *= 1.13f;
            _playerSword.transform.localScale = swordScale;
            level[0] += 1;
            _levelUpTexts[0].text = "Lv." + level[0];
        });
        //体力増加ボタン
        _shopButtons[1].onClick.AddListener(() =>
        {
            playerController.MaxHp *= 1.5f;
            Debug.Log(playerController.MaxHp);
            level[0] += 1;
            _levelUpTexts[1].text = "Lv." + level[1];
        });
        //攻撃力増加ボタン
        _shopButtons[2].onClick.AddListener(() =>
        {
            level[0] += 1;
            _levelUpTexts[1].text = "Lv." + level[1];
        });
        //防御力増加ボタン
        _shopButtons[3].onClick.AddListener(() =>
        {
            level[0] += 1;
            _levelUpTexts[1].text = "Lv." + level[1];
        });
    }
}
