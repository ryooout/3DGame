using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
public class UiController : MonoBehaviour
{
    /// <summary>所持アイテムテキスト</summary>
    [SerializeField] Text havingInventry = default;
    /// <summary>メニュー画面</summary>
    [SerializeField] GameObject menu = default;
    /// <summary>インベントリ画面を閉じる</summary>
    [SerializeField] Button closeButton = default;
    /// <summary>音量設定</summary>
    [SerializeField] GameObject audioVolumeMenu = default;
    public Text itemGet = default;
    /// <summary>説明テキスト</summary>
    [SerializeField] Text exPlain = default;
    /// <summary>Uiを格納する配列</summary>
    [SerializeField] Transform[]uiMove;
    void Start()
    {
        closeButton.gameObject.SetActive(false);
        havingInventry.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*enemyCountText.text = "敵残り" + enemyCount + "体";
        if(enemyCount==0)
        {
            clearText.gameObject.SetActive(true);
        }*/
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //メニュー画面の動き
            uiMove[3].transform.DOMoveY(200, 0.5f);
        }
    }
    /// <summary>アイテムメニュー開く</summary>
    public void ItemOpen()
    {
        menu.SetActive(false);
        closeButton.gameObject.SetActive(true);
        havingInventry.gameObject.SetActive(true);
        //アイテムインベントリの動き
        uiMove[2].transform.DOLocalMoveY(250, 0.5f);
    }
    /// <summary>アイテムメニュー閉じる</summary>
    public void ItemClosed()
    {
        menu.SetActive(true);
        closeButton.gameObject.SetActive(false);
        havingInventry.gameObject.SetActive(false);
        //アイテムインベントリの動き
        uiMove[2].transform.DOLocalMoveY(550,0.5f);
    }
    /// <summary>メニュー画面閉じる</summary>
    public void MenuClosed()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //メニュー画面の動き
        uiMove[3].transform.DOMoveY(600, 0.5f);
    }
    /// <summary>ショップ画面開く</summary>
    public void ShopOpen()
    {
        menu.SetActive(false);
        //ショップメニューの動き
        uiMove[0].transform.DOLocalMove(new Vector3(-9, -15, 60), 0.5f);
    }
    /// <summary>ショップ画面閉じる</summary>
    public void ShopClose()
    {
        menu.SetActive(true);
        //ショップメニューの動き
        uiMove[0].transform.DOLocalMoveX(-758, 0.5f);
    }
    /// <summary>サウンドメニュー開く</summary>
    public void SoundSetting()
    {
        menu.SetActive(false);
        //サウンドメニューの動き
        uiMove[1].transform.DOLocalMoveX(0, 0.5f);
    }
    /// <summary>サウンドメニュー閉じる</summary>
    public void SoundClose()
    {
        menu.SetActive(true);
        //サウンドメニューの動き
        uiMove[1].transform.DOLocalMoveX(-730, 0.5f);
    }
    public void MouseEnter()
    {
        exPlain.gameObject.SetActive(true);
    }
    public void MouseExit()
    {
        exPlain.gameObject.SetActive(false);
    }
}
