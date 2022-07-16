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

    private int stepsLeft;

    private bool inMoveSequence;

    private bool interruptSequence;

    void Start()
    {
        StartLevel(0);
    }

    void Update()
    {
        if (!inMoveSequence)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                CallNextStep(Direction.XPlus);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                CallNextStep(Direction.XMinus);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                CallNextStep(Direction.ZPlus);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                CallNextStep(Direction.ZMinus);
            }
        }
        else { Debug.Log("Key blocked -- already in sequence"); }
    }

    public void ResetSteps()
    {
        stepsLeft = GetCurrentNumberOnTop();
        inMoveSequence = false;
    }

    public void CallNextStep(Direction dir)
    {
       
        if(stepsLeft == 0 || interruptSequence)
        {
            inMoveSequence = false;
            ResetSteps();
            return;
        }

        inMoveSequence = true;
        switch (dir)
        {
            case Direction.XPlus:
                {
                    if (!CanMoveInDir(Direction.XPlus))
                    {
                        ResetSteps(); Debug.Log("Hit Wall"); 
                    }
                    else 
                    { 
                        if (dice.TryMove(Direction.XPlus))
                        {
                            dicePos = (dicePos.Item1 + 1, dicePos.Item2);
                            stepsLeft--;
                        }
                    }
                    break;
                }
            case Direction.XMinus:
                {
                    if (!CanMoveInDir(Direction.XMinus))
                    {
                        ResetSteps(); Debug.Log("Hit Wall");
                    }
                    else
                    {
                        if (dice.TryMove(Direction.XMinus))
                        {
                            dicePos = (dicePos.Item1 - 1, dicePos.Item2);
                            stepsLeft--;
                        }
                    }
                    break;
                }
            case Direction.ZPlus:
                {
                    if (!CanMoveInDir(Direction.ZPlus))
                    {
                        ResetSteps(); Debug.Log("Hit Wall");
                    }
                    else
                    {
                        if (dice.TryMove(Direction.ZPlus))
                        {
                            dicePos = (dicePos.Item1, dicePos.Item2 + 1);
                            stepsLeft--;
                        }
                    }
                    break;
                }
            case Direction.ZMinus:
                {
                    if (!CanMoveInDir(Direction.ZMinus))
                    {
                        ResetSteps(); Debug.Log("Hit Wall");
                    }
                    else
                    {
                        if (dice.TryMove(Direction.ZMinus))
                        {
                            dicePos = (dicePos.Item1, dicePos.Item2 - 1);
                            stepsLeft--;
                        }
                    }
                    break;
                }
        }
    }

    private void SteppedOnVoid()
    {
        interruptSequence = true;
        Debug.Log("Oops you died");
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
                    if (GetCurrentTileMap().IsVoid(dicePos.Item1 + 1, dicePos.Item2)) SteppedOnVoid();
                    return !GetCurrentTileMap().IsWall(dicePos.Item1 + 1, dicePos.Item2);             
            }
            case Direction.XMinus:
            {
                    Debug.Log(GetCurrentTileMap().GetTileState(dicePos.Item1 - 1, dicePos.Item2));
                    if (GetCurrentTileMap().IsVoid(dicePos.Item1 - 1, dicePos.Item2)) SteppedOnVoid();
                    return !GetCurrentTileMap().IsWall(dicePos.Item1 - 1, dicePos.Item2);
            }
            case Direction.ZPlus:
            {
                    Debug.Log(GetCurrentTileMap().GetTileState(dicePos.Item1, dicePos.Item2 + 1));
                    if (GetCurrentTileMap().IsVoid(dicePos.Item1, dicePos.Item2 + 1)) SteppedOnVoid();
                    return !GetCurrentTileMap().IsWall(dicePos.Item1, dicePos.Item2 + 1);
            }
            case Direction.ZMinus:
            {
                    Debug.Log(GetCurrentTileMap().GetTileState(dicePos.Item1, dicePos.Item2 - 1));
                    if (GetCurrentTileMap().IsVoid(dicePos.Item1, dicePos.Item2 - 1)) SteppedOnVoid();
                    return !GetCurrentTileMap().IsWall(dicePos.Item1, dicePos.Item2 - 1);
            }
        }
        return false;
    }

    public TileMap GetCurrentTileMap()
    {
        return levelTileMaps[diceFloor-1];
    }

    public int GetCurrentNumberOnTop()
    {
        return 3;
    }

}
