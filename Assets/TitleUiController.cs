using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TitleUiController : MonoBehaviour
{
    [SerializeField,Header("タイトル内のボタンの動きを管理する変数")] Transform soundUi;
    /// <summary>音量設定の画面を表示するボタン</summary>
    public void SoundSettingButton()
    {
        soundUi.GetComponent<RectTransform>().DOAnchorPos3DX(0, 0.5f);
    }
    /// <summary>音量設定の画面を閉じるボタン</summary>
    public void SoundCloseSettingButton()
    {
        soundUi.GetComponent<RectTransform>().DOAnchorPos3DX(-500, 0.5f);
    }
}
