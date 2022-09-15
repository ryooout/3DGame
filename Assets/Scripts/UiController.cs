using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UiController : MonoBehaviour
{
    public static UiController uiController;
    public  int enemyCount = 1;
    [SerializeField] Text enemyCountText = default;
    [SerializeField] Text clearText = default;
    /// <summary>�����A�C�e���e�L�X�g</summary>
    [SerializeField] Text havingInventry = default;
    /// <summary>���j���[���</summary>
    [SerializeField] GameObject menu = default;
    /// <summary>�A�C�e���C���x���g����� </summary>
    [SerializeField] GameObject item = default;
    /// <summary>�C���x���g����ʂ����</summary>
    [SerializeField] Button closeButton = default;
    /// <summary>���ʐݒ�</summary>
    [SerializeField] GameObject audioVolumeMenu = default;
    /// <summary>�V���b�v���</summary>
    [SerializeField] GameObject shop = default;
    [SerializeField] Button shopClose = default;
    public Text itemGet = default;
    [SerializeField] Text exPlain = default;
    void Start()
    {
        if(uiController ==null)
        {
            uiController = this;
        }
        menu.SetActive(false);
        closeButton.gameObject.SetActive(false);
        item.SetActive(false);
        audioVolumeMenu.SetActive(false);
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
            menu.SetActive(true);
        }
    }
    public void ItemOpen()
    {
        closeButton.gameObject.SetActive(true);
        item.SetActive(true);
        havingInventry.gameObject.SetActive(true);
        menu.SetActive(false);
    }
    public void ItemClosed()
    {
        closeButton.gameObject.SetActive(false);
        item.SetActive(false);
        menu.SetActive(true);
        havingInventry.gameObject.SetActive(false);
    }
    public void MenuClosed()
    {
        menu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void ShopOpen()
    {
        shop.gameObject.SetActive(true);
        menu.gameObject.SetActive(false);
    }
    public void ShopClose()
    {
        shop.gameObject.SetActive(false);
        menu.gameObject.SetActive(true);
    }
    public void SoundSetting()
    {
        audioVolumeMenu.SetActive(true);
        menu.SetActive(false);
    }
    public void SoundClose()
    {
        audioVolumeMenu.SetActive(false);
        menu.SetActive(true);
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
