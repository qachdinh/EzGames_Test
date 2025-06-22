using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public LevelConfigList levelConfigList;
    public Transform player;
    [SerializeField] int currentLevel = 1;

    [SerializeField] private EnemyController currentEnemy;

    public GameObject victoryPanel;



    void Start()
    {
        LoadLevel(currentLevel);
    }

    public void LoadLevel(int levelIndex)
    {
        if (levelIndex - 1 >= levelConfigList.levels.datas.Count)
        {
            Debug.Log("Finish");
            return;
        }

        var datas = levelConfigList.levels.datas[levelIndex - 1];

        player.position = datas.playerSpawnPoint;
        player.GetComponent<PlayerHealth>().ResetHealth();

        var enemySpawn = Instantiate(currentEnemy, datas.enemySpawnPoint, Quaternion.identity);
        enemySpawn.SetEnemyData(datas);
    }



    public void NextLevel()
    {
        currentLevel++;
        LoadLevel(currentLevel);
    }

}
