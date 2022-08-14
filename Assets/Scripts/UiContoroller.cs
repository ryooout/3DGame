using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UiContoroller : MonoBehaviour
{
    public static int enemyCount = 1;
    [SerializeField] Text enemyCountText = default;
    [SerializeField] Text clearText = default;
    [SerializeField] GameObject inventry = default;
    [SerializeField] Button closeButton = default;
    void Start()
    {
        inventry.SetActive(false);
        closeButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCountText.text = "“GŽc‚è" + enemyCount + "‘Ì";
        if(enemyCount==0)
        {
            clearText.gameObject.SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            inventry.SetActive(true);
            closeButton.gameObject.SetActive(true);
        }
    }
    public void HealButton()
    {
        PlayerController.heal_flag = true;     
    }
    public void IsClosed()
    {
        closeButton.gameObject.SetActive(false);
        inventry.SetActive(false);
    }
}
