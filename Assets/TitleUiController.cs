using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TitleUiController : MonoBehaviour
{
    [SerializeField,Header("�^�C�g�����̃{�^���̓������Ǘ�����ϐ�")] Transform soundUi;
    /// <summary>���ʐݒ�̉�ʂ�\������{�^��</summary>
    public void SoundSettingButton()
    {
        soundUi.GetComponent<RectTransform>().DOAnchorPos3DX(0, 0.5f);
    }
    /// <summary>���ʐݒ�̉�ʂ����{�^��</summary>
    public void SoundCloseSettingButton()
    {
        soundUi.GetComponent<RectTransform>().DOAnchorPos3DX(-500, 0.5f);
    }
}
