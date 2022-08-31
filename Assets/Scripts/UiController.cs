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
    [SerializeField] GameObject menu = default;
    [SerializeField] GameObject item = default;
    [SerializeField] Button closeButton = default;
    [SerializeField] GameObject audioVolumeMenu = default;
    public Text itemGet = default;
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
    }

    // Update is called once per frame
    void Update()
    {
        /*enemyCountText.text = "“GŽc‚è" + enemyCount + "‘Ì";
        if(enemyCount==0)
        {
            clearText.gameObject.SetActive(true);
        }*/
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(true);
        }
    }
    public void HealButton()
    {
        PlayerController.heal_flag = true;     
    }
    public void IsClosed()
    {
        closeButton.gameObject.SetActive(false);
        item.SetActive(false);
        menu.SetActive(true);
    }
    public void MenuClosed()
    {
        menu.SetActive(false);
    }
    public void ItemOpen()
    {
        closeButton.gameObject.SetActive(true);
        item.SetActive(true);
        menu.SetActive(false);
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
}
