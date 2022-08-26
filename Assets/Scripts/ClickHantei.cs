using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHantei : MonoBehaviour
{
    UiContoroller uiContoroller;
    private void Start()
    {
        uiContoroller = GameObject.Find("UiObj").GetComponent<UiContoroller>();
        uiContoroller.itemGet.gameObject.SetActive(false);
    }
    private void OnMouseUp()
    {
        uiContoroller.itemGet.gameObject.SetActive(true);
        if (gameObject.CompareTag("Atk"))
        {
            uiContoroller.itemGet.text = name + "を入手した";
        }
        else if (gameObject.CompareTag("Def"))
        {
            uiContoroller.itemGet.text = name + "を入手した";
        }
        else
        {
            uiContoroller.itemGet.text = name + "を入手した";
        }
        Invoke(nameof(RemoveText), 2.0f);
    }
    void RemoveText()
    {
        uiContoroller.itemGet.gameObject.SetActive(false);
    }
}
