using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class TitleUiManager : MonoBehaviour
{
    [SerializeField, Header("AudioMenu")]
    Transform audioPanel;
    /// <summary>�^�C�g����ʂ̃I�[�f�B�I���j���[���J��</summary>
    public void AudioManuOpen()
    {
        audioPanel.GetComponent<RectTransform>().DOAnchorPosX(0, 0.5f);
    }
    /// <summary>�^�C�g����ʂ̃I�[�f�B�I���j���[�����</summary>
    public void AudioManuClose()
    {
        audioPanel.GetComponent<RectTransform>().DOAnchorPosX(-470, 0.5f);
    }
}
