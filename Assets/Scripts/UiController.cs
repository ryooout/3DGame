using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
public class UiController : MonoBehaviour
{
    /// <summary>�����A�C�e���e�L�X�g</summary>
    [SerializeField] Text havingInventry = default;
    /// <summary>���j���[���</summary>
    [SerializeField] GameObject menu = default;
    /// <summary>�C���x���g����ʂ����</summary>
    [SerializeField] Button closeButton = default;
    /// <summary>���ʐݒ�</summary>
    [SerializeField] GameObject audioVolumeMenu = default;
    public Text itemGet = default;
    /// <summary>�����e�L�X�g</summary>
    [SerializeField] Text exPlain = default;
    /// <summary>Ui���i�[����z��</summary>
    [SerializeField] Transform[]uiMove;
    void Start()
    {
        closeButton.gameObject.SetActive(false);
        havingInventry.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*enemyCountText.text = "�G�c��" + enemyCount + "��";
        if(enemyCount==0)
        {
            clearText.gameObject.SetActive(true);
        }*/
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //���j���[��ʂ̓���
            uiMove[3].transform.DOMoveY(200, 0.5f);
        }
    }
    /// <summary>�A�C�e�����j���[�J��</summary>
    public void ItemOpen()
    {
        menu.SetActive(false);
        closeButton.gameObject.SetActive(true);
        havingInventry.gameObject.SetActive(true);
        //�A�C�e���C���x���g���̓���
        uiMove[2].transform.DOLocalMoveY(250, 0.5f);
    }
    /// <summary>�A�C�e�����j���[����</summary>
    public void ItemClosed()
    {
        menu.SetActive(true);
        closeButton.gameObject.SetActive(false);
        havingInventry.gameObject.SetActive(false);
        //�A�C�e���C���x���g���̓���
        uiMove[2].transform.DOLocalMoveY(550,0.5f);
    }
    /// <summary>���j���[��ʕ���</summary>
    public void MenuClosed()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //���j���[��ʂ̓���
        uiMove[3].transform.DOMoveY(600, 0.5f);
    }
    /// <summary>�V���b�v��ʊJ��</summary>
    public void ShopOpen()
    {
        menu.SetActive(false);
        //�V���b�v���j���[�̓���
        uiMove[0].transform.DOLocalMove(new Vector3(-9, -15, 60), 0.5f);
    }
    /// <summary>�V���b�v��ʕ���</summary>
    public void ShopClose()
    {
        menu.SetActive(true);
        //�V���b�v���j���[�̓���
        uiMove[0].transform.DOLocalMoveX(-758, 0.5f);
    }
    /// <summary>�T�E���h���j���[�J��</summary>
    public void SoundSetting()
    {
        menu.SetActive(false);
        //�T�E���h���j���[�̓���
        uiMove[1].transform.DOLocalMoveX(0, 0.5f);
    }
    /// <summary>�T�E���h���j���[����</summary>
    public void SoundClose()
    {
        menu.SetActive(true);
        //�T�E���h���j���[�̓���
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
