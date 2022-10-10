using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
public class UiController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    /// <summary>�����A�C�e���e�L�X�g</summary>
    [SerializeField]
    [Header("�����A�C�e����\������")]Text havingInventry = default;
    /// <summary>���j���[���</summary>
    [SerializeField]
    [Header("���j���[���")] GameObject menu = default;
    /// <summary>�C���x���g����ʂ����</summary>
    [SerializeField]
    [Header("�C���x���g���̉�ʂ����p�̃{�^��")] Button closeButton = default;
    /// <summary>�������\��</summary>
    [SerializeField]
    [Header("��������\������")] Text haveMoneyText;
    public Text itemGet = default;
    /// <summary>�����e�L�X�g</summary>
    [SerializeField]
    [Header("SE�ɂ��Ă̐���")] Text exPlain = default;
    /// <summary>Ui���i�[����z��</summary>
    [SerializeField]
    [Header("Ui���i�[���Ă���")] Transform[]uiMove;
    PauseManager _pauseManager;
    /// <summary>�Q�[���I�[�o�[���ɌĂяo�����</summary>
    [SerializeField]
    [Header("�Q�[���I�[�o�[���ɌĂяo�����")] private Text gameOverText;
    private void Awake()
    {
        _pauseManager = GameObject.FindObjectOfType<PauseManager>();
    }
    void OnEnable()
    {
        // �Ă�ŗ~�������\�b�h��o�^����B
        _pauseManager.OnPauseResume += PauseResume;
    }

    void OnDisable()
    {
        // OnDisable �ł̓��\�b�h�̓o�^���������邱�ƁB�����Ȃ��ƃI�u�W�F�N�g�������ɂ��ꂽ��j�����ꂽ�肵����ɃG���[�ɂȂ��Ă��܂��B
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
    /// <summary>���j���[��ʊJ��</summary>
    public void Pause()
    {
        //���j���[��ʂ̓���
        uiMove[3].GetComponent<RectTransform>().DOAnchorPos3DY(0, 0.5f);
        //sliderUI�����
        uiMove[4].GetComponent<RectTransform>().DOAnchorPos3DX(-330,0.5f);
        //�X�e�[�^�X���j���[���J��
        uiMove[5].GetComponent<RectTransform>().DOAnchorPos3DY(-400,0.5f);
    }

    /// <summary>���j���[��ʕ���</summary>
    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //���j���[��ʂ̓���
        uiMove[3].GetComponent<RectTransform>().DOAnchorPosY(1050, 0.5f);
        //sliderUI���J��
        uiMove[4].GetComponent<RectTransform>().DOAnchorPosX(0,0.5f);
        //�X�e�[�^�X���j���[�����
        uiMove[5].GetComponent<RectTransform>().DOAnchorPos3DY(-525,0.5f);
    }
    void Start()
    {
        closeButton.gameObject.SetActive(false);
        havingInventry.gameObject.SetActive(false);
        haveMoneyText.text = "������:"+ GameManager.Instance.Money + "�S�[���h";
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.IsDie)
        {
            gameOverText.gameObject.SetActive(true);
        }
    }
    /// <summary>�A�C�e�����j���[�J��</summary>
    public void ItemOpen()
    {
        menu.SetActive(false);
        closeButton.gameObject.SetActive(true);
        havingInventry.gameObject.SetActive(true);
        //�A�C�e���C���x���g���̓���
        uiMove[2].GetComponent<RectTransform>().DOAnchorPos(new Vector3(-490, -190, 0), 0.5f);  
    }
    /// <summary>�A�C�e�����j���[����</summary>
    public void ItemClosed()
    {
        menu.SetActive(true);
        closeButton.gameObject.SetActive(false);
        havingInventry.gameObject.SetActive(false);
        //�A�C�e���C���x���g���̓���
        uiMove[2].GetComponent<RectTransform>().DOAnchorPos(new Vector3(-490, 160, 0), 0.5f);
    }
    /// <summary>�V���b�v��ʊJ��</summary>
    public void ShopOpen()
    {
        menu.SetActive(false);
        //�V���b�v���j���[�̓���
        uiMove[0].GetComponent<RectTransform>().DOAnchorPos(Vector3.zero, 0.5f);
    }
    /// <summary>�V���b�v��ʕ���</summary>
    public void ShopClose()
    {
        menu.SetActive(true);
        //�V���b�v���j���[�̓���
        uiMove[0].GetComponent<RectTransform>().DOAnchorPosX(-1330, 0.5f);
    }
    /// <summary>�T�E���h���j���[�J��</summary>
    public void SoundSetting()
    {
        menu.SetActive(false);
        //�T�E���h���j���[�̓���
        uiMove[1].GetComponent<RectTransform>().DOAnchorPos(Vector3.zero, 0.5f);
    }
    /// <summary>�T�E���h���j���[����</summary>
    public void SoundClose()
    {
        menu.SetActive(true);
        //�T�E���h���j���[�̓���
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
