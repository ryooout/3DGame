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
        // 呼んで欲しいメソッドを登録する。
        _pauseManager.OnPauseResume += PauseResume;
    }

    void OnDisable()
    {
        // OnDisable ではメソッドの登録を解除すること。さもないとオブジェクトが無効にされたり破棄されたりした後にエラーになってしまう。
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
    /// <summary>敵出現のCoroutine</summary>
    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            //距離10のベクトル
            var distanceVector = new Vector3(20, 0);
            //プレイヤーの位置をベースにした敵の出現
            //y軸に対してベクトルをランダムに0度から360度回転させている
            var spawnPositionFromPlayer = Quaternion.Euler(0, Random.Range(0, 360), 0) * distanceVector;
            //敵を出現させたい位置決定
            var spawnPos = playerController.transform.position + spawnPositionFromPlayer;
            //指定座標から一番近いNavMeshの座標を探す
            NavMeshHit navMeshHit;
            //NavMesh外に出ないようにするための処理
            if (NavMesh.SamplePosition(spawnPos, out navMeshHit, 10, NavMesh.AllAreas))
            {
                Instantiate(enemyPrefab, navMeshHit.position, Quaternion.identity);
            }
            //10秒待つ
            yield return new WaitForSeconds(5.0f);
            //プレイヤーが死んだらループを抜ける
            if (playerController.Hp <= 0)
            {
                break;
            }
        }
    }
}
