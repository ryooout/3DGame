using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
public class UiController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    /// <summary>所持アイテムテキスト</summary>
    [SerializeField]
    [Header("所持アイテムを表示する")]Text havingInventry = default;
    /// <summary>メニュー画面</summary>
    [SerializeField]
    [Header("メニュー画面")] GameObject menu = default;
    /// <summary>インベントリ画面を閉じる</summary>
    [SerializeField]
    [Header("インベントリの画面を閉じる用のボタン")] Button closeButton = default;
    /// <summary>所持金表示</summary>
    [SerializeField]
    [Header("所持金を表示する")] Text haveMoneyText;
    public Text itemGet = default;
    /// <summary>説明テキスト</summary>
    [SerializeField]
    [Header("SEについての説明")] Text exPlain = default;
    /// <summary>Uiを格納する配列</summary>
    [SerializeField]
    [Header("Uiを格納している")] Transform[]uiMove;
    PauseManager _pauseManager;
    /// <summary>ゲームオーバー時に呼び出される</summary>
    [SerializeField]
    [Header("ゲームオーバー時に呼び出される")] private Text gameOverText;
    private void Awake()
    {
        _pauseManager = GameObject.FindObjectOfType<PauseManager>();
    }
    void OnEnable()
    {
        // 呼んで欲しいメソッドを登録する。
        _pauseManager.OnPauseResume += PauseResume;
    }

    void OnDisable()
    {
        // OnDisable ではメソッドの登録を解除すること。さもないとオブジェクトが無効にされたり破棄されたりした後にエラーになってしまう。
        _pauseManager.OnPauseResume -= PauseResume;
    }

    void PauseResume(bool isPause)
    {
        if (isPause)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }
    /// <summary>メニュー画面開く</summary>
    public void Pause()
    {
        //メニュー画面の動き
        uiMove[3].GetComponent<RectTransform>().DOAnchorPos3DY(0, 0.5f);
        //sliderUIを閉じる
        uiMove[4].GetComponent<RectTransform>().DOAnchorPos3DX(-330,0.5f);
        //ステータスメニューを開く
        uiMove[5].GetComponent<RectTransform>().DOAnchorPos3DY(-400,0.5f);
    }

    /// <summary>メニュー画面閉じる</summary>
    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //メニュー画面の動き
        uiMove[3].GetComponent<RectTransform>().DOAnchorPosY(1050, 0.5f);
        //sliderUIを開く
        uiMove[4].GetComponent<RectTransform>().DOAnchorPosX(0,0.5f);
        //ステータスメニューを閉じる
        uiMove[5].GetComponent<RectTransform>().DOAnchorPos3DY(-525,0.5f);
    }
    void Start()
    {
        closeButton.gameObject.SetActive(false);
        havingInventry.gameObject.SetActive(false);
        haveMoneyText.text = "所持金:"+ GameManager.Instance.Money + "ゴールド";
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.IsDie)
        {
            gameOverText.gameObject.SetActive(true);
        }
    }
    /// <summary>アイテムメニュー開く</summary>
    public void ItemOpen()
    {
        menu.SetActive(false);
        closeButton.gameObject.SetActive(true);
        havingInventry.gameObject.SetActive(true);
        //アイテムインベントリの動き
        uiMove[2].GetComponent<RectTransform>().DOAnchorPos(new Vector3(-490, -190, 0), 0.5f);  
    }
    /// <summary>アイテムメニュー閉じる</summary>
    public void ItemClosed()
    {
        menu.SetActive(true);
        closeButton.gameObject.SetActive(false);
        havingInventry.gameObject.SetActive(false);
        //アイテムインベントリの動き
        uiMove[2].GetComponent<RectTransform>().DOAnchorPos(new Vector3(-490, 160, 0), 0.5f);
    }
    /// <summary>ショップ画面開く</summary>
    public void ShopOpen()
    {
        menu.SetActive(false);
        //ショップメニューの動き
        uiMove[0].GetComponent<RectTransform>().DOAnchorPos(Vector3.zero, 0.5f);
    }
    /// <summary>ショップ画面閉じる</summary>
    public void ShopClose()
    {
        menu.SetActive(true);
        //ショップメニューの動き
        uiMove[0].GetComponent<RectTransform>().DOAnchorPosX(-1330, 0.5f);
    }
    /// <summary>サウンドメニュー開く</summary>
    public void SoundSetting()
    {
        menu.SetActive(false);
        //サウンドメニューの動き
        uiMove[1].GetComponent<RectTransform>().DOAnchorPos(Vector3.zero, 0.5f);
    }
    /// <summary>サウンドメニュー閉じる</summary>
    public void SoundClose()
    {
        menu.SetActive(true);
        //サウンドメニューの動き
        uiMove[1].GetComponent<RectTransform>().DOAnchorPosX(-1200, 0.5f);
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
