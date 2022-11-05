using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static GameManager Instance { get=>instance;set=>instance = value; } 
    [Header("レベル管理")]
    [SerializeField] private int playerLevel = 1;
    [Header("所持金")]
    [SerializeField] private int money = 3000;
    [Header("経過時間")]
    [SerializeField] private float time;
    [Header("ゲームがスタートしているか"),]
    private bool _isStarted;
    [SerializeField, Header("カウントダウンを表示させるテキスト")]
    private Text _countTimer;
    [SerializeField,Header("経過時間のテキスト")]
    private Text _passsedTime;
    //private UiController _uiController;
    /// <summary>プレイヤーのレベル</summary>
    public int PlayerLevel => playerLevel;
    /// <summary>所持金</summary>
    public int Money {get=>money;set=> money = value;}
    /// <summary>スタートフラグのプロパティ</summary>
    public bool IsStarted { get => _isStarted; set => _isStarted = value; }
    private void Awake()
    {
        if(Instance == null)
        {
            instance = this;
            
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        //_uiController = GameObject.Find("UiObj").GetComponent<UiController>();
        _passsedTime.gameObject.SetActive(false);
        StartCoroutine(CountDownTimer());
    }
    private void Update()
    {
        if(_isStarted)
        {
            time += Time.deltaTime;
            _passsedTime.text = $"経過時間:{(int)time / 60:00}:{time%60:00}";
        }
    }
    IEnumerator CountDownTimer()
    {
        _isStarted = false;
        yield return new WaitForSeconds(1.0f);
        _countTimer.text = "3";
        yield return new WaitForSeconds(1.0f);
        _countTimer.text = "2";
        yield return new WaitForSeconds(1.0f);
        _countTimer.text = "1";
        yield return new WaitForSeconds(1.0f);
        _countTimer.text = "Start!";
        yield return new WaitForSeconds(1.0f);
        _countTimer.gameObject.SetActive(false);
        _passsedTime.gameObject.SetActive(true);
        _isStarted = true;
    }
}
