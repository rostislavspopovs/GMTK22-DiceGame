using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [SerializeField] private List<Level> levels;
    [SerializeField] private GameObject dicePrefab;
    private GameObject dice;
    private (int,int) dicePos;
    private int diceFloor;

    private List<TileMap> levelTileMaps;

    void Start()
    {
        StartLevel(0);
    }

    void Update()
    {
        
    }

    private void StartLevel(int level)
    {
        levelTileMaps = MapGenerator.Instance.GenerateMap(levels[level]);
        SpawnDie(levelTileMaps[0].GetStartTile(),1);
    }

    private void SpawnDie((int,int) spawnPos, int floor)
    {
        Instantiate(dicePrefab, new Vector3(spawnPos.Item1, floor * 4 + 0.5f, spawnPos.Item2), new Quaternion(0,0,0,1));
    }

}
