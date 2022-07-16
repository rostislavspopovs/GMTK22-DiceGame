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
            if (CanMoveInDir(Direction.XPlus))
            {
                if (dice.Move(Direction.XPlus)) dicePos = (dicePos.Item1 + 1, dicePos.Item2);
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (CanMoveInDir(Direction.XMinus))
            {
                if (dice.Move(Direction.XMinus)) dicePos = (dicePos.Item1 - 1, dicePos.Item2);
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (CanMoveInDir(Direction.ZPlus))
            {
                if (dice.Move(Direction.ZPlus)) dicePos = (dicePos.Item1, dicePos.Item2 + 1);
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (CanMoveInDir(Direction.ZMinus))
            { 
                if (dice.Move(Direction.ZMinus)) dicePos = (dicePos.Item1, dicePos.Item2 - 1);
            }
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

    public bool CanMoveInDir(Direction dir)
    {
        switch (dir)
        {
            case Direction.XPlus:
            {
                    Debug.Log(GetCurrentTileMap().GetTileState(dicePos.Item1 + 1, dicePos.Item2));
                    return !GetCurrentTileMap().IsWall(dicePos.Item1 + 1, dicePos.Item2);             
            }
            case Direction.XMinus:
            {
                    Debug.Log(GetCurrentTileMap().GetTileState(dicePos.Item1 - 1, dicePos.Item2));
                    return !GetCurrentTileMap().IsWall(dicePos.Item1 - 1, dicePos.Item2);
            }
            case Direction.ZPlus:
            {
                    Debug.Log(GetCurrentTileMap().GetTileState(dicePos.Item1, dicePos.Item2 + 1));
                    return !GetCurrentTileMap().IsWall(dicePos.Item1, dicePos.Item2 + 1);
            }
            case Direction.ZMinus:
            {
                    Debug.Log(GetCurrentTileMap().GetTileState(dicePos.Item1, dicePos.Item2 - 1));
                    return !GetCurrentTileMap().IsWall(dicePos.Item1, dicePos.Item2 - 1);
            }
        }
        return false;
    }

    public TileMap GetCurrentTileMap()
    {
        return levelTileMaps[diceFloor-1];
    }

}
