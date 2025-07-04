using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public LevelConfigList levelConfigList;
    public Transform player;
    [SerializeField] int currentLevel = 1;

    [SerializeField] private EnemyController currentEnemy;
    public GameObject allyPrefab;

    public GameObject victoryPanel;
    public GameObject finishPanel;
    public GameObject losePanel;

    public bool isGameOver = false;

    public TextMeshProUGUI levelText;

    private int aliveEnemyCount = 0;
    private int allyAliveCount = 0;
    private int playerAlive = 1;

    void Start()
    {
        LoadLevel(currentLevel);
    }

    public void LoadLevel(int levelIndex)
    {
        isGameOver = false;
        allyAliveCount = 0;
        playerAlive = 1;

        if (levelIndex - 1 >= levelConfigList.levels.datas.Count)
        {
            Debug.Log("Finish");
            finishPanel.SetActive(true);
            return;
        }

        AudioManager.Instance.PlayMusic(AudioManager.Instance.bgm);
        var datas = levelConfigList.levels.datas[levelIndex - 1];

        player.position = datas.playerSpawnPoint;
        player.GetComponent<PlayerHealth>().ResetHealth();

        if (GameConfig.SelectedMode == GameMode.TwoVsTwo)
        {
            Instantiate(allyPrefab, datas.allySpawnPoint, Quaternion.identity);
            allyAliveCount = 1;
        }


        int enemyCount = GameConfig.SelectedMode == GameMode.OneVsOne ? 1 : 2;

        for (int i = 0; i < enemyCount; i++)
        {
            var spawnPos = datas.enemySpawnPoint[i];
            var enemy = Instantiate(currentEnemy, spawnPos, Quaternion.identity);
            enemy.SetEnemyData(datas);
        }
        aliveEnemyCount = enemyCount;

        levelText.text = "Level " + levelIndex;
    }



    public void NextLevel()
    {
        currentLevel++;
        LoadLevel(currentLevel);
    }

    public void EnemyDied()
    {
        aliveEnemyCount--;

        if (aliveEnemyCount <= 0)
        {
            ShowVictory();
        }
    }
    private void ShowVictory()
    {
        isGameOver = true;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxWin);
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }
    }
    public void ShowLose()
    {
        isGameOver = true;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxLose);
        if (losePanel != null)
        {
            losePanel.SetActive(true);
        }
    }

    public void NotifyPlayerDied()
    {
        playerAlive = 0;

        if (allyAliveCount <= 0 && !isGameOver)
        {
            ShowLose();
        }
    }

    public void AllyDied()
    {
        allyAliveCount--;

        if (playerAlive <= 0 && allyAliveCount <= 0 && !isGameOver)
        {
            ShowLose();
        }
    }
}
