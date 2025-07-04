using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Configs/LevelData")]
public class LevelData : ScriptableObject
{
    public List<LevelDataInfo> datas;

}
[System.Serializable]
public class LevelDataInfo
{
    public int levelNumber;

    //public GameObject enemyPrefab;

    public float tillingX;
    public float tillingY;


    public Vector3 playerSpawnPoint;
    public List<Vector3> enemySpawnPoint;
}