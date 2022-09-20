using UnityEngine;

/// <summary>
/// �ꎞ��~�E�ĊJ�𐧌䂷��B
/// </summary>
public class PauseManager : MonoBehaviour
{
    /// <summary>true �̎��͈ꎞ��~�Ƃ���</summary>
    bool _pauseFlg = false;
    /// <summary>�ꎞ��~�E�ĊJ�𐧌䂷��֐��̌^�i�f���Q�[�g�j���`����</summary>
    public delegate void Pause(bool isPause);
    /// <summary>�f���Q�[�g�����Ă����ϐ�</summary>
    Pause _onPauseResume = default;

    /// <summary>
    /// �ꎞ��~�E�ĊJ������f���Q�[�g�v���p�e�B
    /// </summary>
    public Pause OnPauseResume
    {
        get { return _onPauseResume; }
        set { _onPauseResume = value; }
    }

    void Update()
    {
        // ESC �L�[�������ꂽ��ꎞ��~�E�ĊJ��؂�ւ���
        if (Input.GetButtonDown("Cancel"))
        {
            PauseResume();
        }
    }

    /// <summary>
    /// �ꎞ��~�E�ĊJ��؂�ւ���
    /// </summary>
    void PauseResume()
    {
        _pauseFlg = !_pauseFlg;
        _onPauseResume(_pauseFlg);  // ����ŕϐ��ɑ�������֐����i�S�āj�Ăяo����
    }
}

