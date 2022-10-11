using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager instance;
    PauseManager _pauseManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject enemyPrefab;
    public static EnemySpawnManager Instance => instance;
    private void Awake()
    {
        if (Instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        _pauseManager = GameObject.FindObjectOfType<PauseManager>();
        return;
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
    public void Pause()
    {
        StopCoroutine(SpawnLoop());
    }

    public void Resume()
    {
        StartCoroutine(SpawnLoop());
    }
    private void Start()
    {
        StartCoroutine(SpawnLoop());
        
    }
    /// <summary>�G�o����Coroutine</summary>
    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            //����10�̃x�N�g��
            var distanceVector = new Vector3(20, 0);
            //�v���C���[�̈ʒu���x�[�X�ɂ����G�̏o��
            //y���ɑ΂��ăx�N�g���������_����0�x����360�x��]�����Ă���
            var spawnPositionFromPlayer = Quaternion.Euler(0, Random.Range(0, 360), 0) * distanceVector;
            //�G���o�����������ʒu����
            var spawnPos = playerController.transform.position + spawnPositionFromPlayer;
            //�w����W�����ԋ߂�NavMesh�̍��W��T��
            NavMeshHit navMeshHit;
            //NavMesh�O�ɏo�Ȃ��悤�ɂ��邽�߂̏���
            if (NavMesh.SamplePosition(spawnPos, out navMeshHit, 10, NavMesh.AllAreas))
            {
                Instantiate(enemyPrefab, navMeshHit.position, Quaternion.identity);
            }
            //10�b�҂�
            yield return new WaitForSeconds(5.0f);
            //�v���C���[�����񂾂烋�[�v�𔲂���
            if (playerController.Hp <= 0)
            {
                break;
            }
        }
    }
}
