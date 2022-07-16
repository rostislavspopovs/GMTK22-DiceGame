using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [SerializeField] private List<Level> levels;
    [SerializeField] private GameObject dicePrefab;

    public enum Direction { XPlus, XMinus, ZPlus, ZMinus}

    private Dice dice;
    private (int,int) dicePos;
    private int diceFloor;

    private List<TileMap> levelTileMaps;

    void Start()
    {
        StartLevel(0);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            dice.Move(Direction.XPlus);
        }

    }

    private void StartLevel(int level)
    {
        levelTileMaps = MapGenerator.Instance.GenerateMap(levels[level]);
        SpawnDie(levelTileMaps[0].GetStartTile(),1);
    }

    private void SpawnDie((int,int) spawnPos, int floor)
    {
        GameObject diceObj = Instantiate(dicePrefab, new Vector3(spawnPos.Item1, floor * 4 + 0.5f, spawnPos.Item2), new Quaternion(0,0,0,1));
        dice = diceObj.GetComponent<Dice>();
        dicePos = spawnPos;
        diceFloor = floor;
    }

}
