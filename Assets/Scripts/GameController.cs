using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [SerializeField] private List<Level> levels;
    [SerializeField] private GameObject dicePrefab;

    [SerializeField] private CameraFollower mainCameraFollower;

    public delegate void OnStepAction(int stepsleft);
    public event OnStepAction OnStepEvent;

    public enum Direction { XPlus, XMinus, ZPlus, ZMinus}

    private Dice dice;
    private (int,int) dicePos;
    private int diceFloor;

    private List<TileMap> levelTileMaps;

    private int stepsLeft = 1;

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
            //if (Input.GetKeyDown(KeyCode.W))
            //{
            //    BeginMoveSequence(Direction.XPlus);
            //}
            //if (Input.GetKeyDown(KeyCode.S))
            //{
            //    BeginMoveSequence(Direction.XMinus);
            //}
            //if (Input.GetKeyDown(KeyCode.A))
            //{
            //    BeginMoveSequence(Direction.ZPlus);
            //}
            //if (Input.GetKeyDown(KeyCode.D))
            //{
            //    BeginMoveSequence(Direction.ZMinus);
            //}
        }
    }

    public void AddOnStepEventAction(OnStepAction action)
    {
        OnStepEvent += action;
    }

    public void ResetSteps(bool restarting = false)
    {
        stepsLeft = restarting ? 1 : GetCurrentNumberOnTop();
        inMoveSequence = false;
        EffectsAndOverlaysManager.Instance.CreateHighlights(GetCurrentTileMap(), diceFloor, dicePos.Item1, dicePos.Item2, Direction.XPlus, stepsLeft);
        EffectsAndOverlaysManager.Instance.CreateHighlights(GetCurrentTileMap(), diceFloor, dicePos.Item1, dicePos.Item2, Direction.XMinus, stepsLeft);
        EffectsAndOverlaysManager.Instance.CreateHighlights(GetCurrentTileMap(), diceFloor, dicePos.Item1, dicePos.Item2, Direction.ZPlus, stepsLeft);
        EffectsAndOverlaysManager.Instance.CreateHighlights(GetCurrentTileMap(), diceFloor, dicePos.Item1, dicePos.Item2, Direction.ZMinus, stepsLeft);

        if (restarting) DiceIndicatorController.Instance.Toggle(true, stepsLeft);
        else DiceIndicatorController.Instance.ResetDots(stepsLeft, 0.1f);

        OnStepEvent?.Invoke(stepsLeft);
        Debug.Log($"Steps reset! Steps left: {stepsLeft}");
    }

    public void BeginMoveSequence(Direction dir)
    {
        if (!inMoveSequence)
        {
            EffectsAndOverlaysManager.Instance.ClearHighlights();
            CallNextStep(dir);
        }
        else Debug.LogWarning("Cannot begin new move sequence while current one is in motion!");
    }

    public void CallNextStep(Direction dir)
    {
        DiceIndicatorController.Instance.DecrementDot(stepsLeft,dir);
        if (stepsLeft == 0 || interruptSequence)
        {
            inMoveSequence = false;
            interruptSequence = false;
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
                        ResetSteps(); 
                        Debug.Log("Hit Wall"); 
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
                        ResetSteps();
                        Debug.Log("Hit Wall");
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
                        ResetSteps();
                        Debug.Log("Hit Wall");
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
                        ResetSteps();
                        Debug.Log("Hit Wall");
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
        OnStepEvent?.Invoke(stepsLeft);
    }

    public bool CanMoveInDir(Direction dir)
    {

        int newX = dicePos.Item1;
        int newZ = dicePos.Item2;

        switch (dir)
        {
            case Direction.XPlus:   newX++; break;
            case Direction.XMinus:  newX--; break;
            case Direction.ZPlus:   newZ++; break;
            case Direction.ZMinus:  newZ--; break;
        }
        try 
        {
            //Debug.Log(GetCurrentTileMap().GetTileState(newX, newZ));
            if (GetCurrentTileMap().IsVoid(newX, newZ)) SteppedOnVoid(dir);
            return !GetCurrentTileMap().IsWall(newX, newZ);   
        } 
        catch (System.IndexOutOfRangeException e)     
        {
            Debug.Log("Tile " + newX + "," + newZ + " throws " + e + " assuming void.");
            SteppedOnVoid(dir);
            return true;
        }  
    }

    public TileMap GetCurrentTileMap()
    {
        return levelTileMaps[diceFloor-1];
    }

    public int GetCurrentNumberOnTop()
    {
        int x = Mathf.RoundToInt(dice.getDiceMesh().transform.eulerAngles.x) % 360;
        int z = Mathf.RoundToInt(dice.getDiceMesh().transform.eulerAngles.z) % 360;
        if (x == 90) { return 5; }
        else if (x == 270) { return 2; }
        switch ((x+z)%360) {
            case 0: return 1;
            case 90: return 3;  
            case 180: return 6;
            case 270: return 4;
        }
        return 100;
    }

    public Vector3 TileToWorldPosition(int floor, int tileX, int tileY)
    {
        return new Vector3(tileX, floor * 4 + 0.5f, tileY);
    }

    public Vector3 DirToVector(Direction dir)
    {
        switch (dir)
        {
            case Direction.XPlus: return new Vector3(1, 0, 0);
            case Direction.XMinus: return new Vector3(-1, 0, 0);
            case Direction.ZPlus: return new Vector3(0, 0, 1);
            case Direction.ZMinus: return new Vector3(0, 0, -1);
            default: return Vector3.zero;
        }

    }

    public void Restart(int level, float delay = 0)
    {
        StartCoroutine(RestartEnumerator(level, delay));
    }

    private void SteppedOnVoid(Direction fromDir)
    {
        interruptSequence = true;

        dice.Ragdoll(DirToVector(fromDir));

        DiceIndicatorController.Instance.Toggle(false);

        Restart(level: 0, delay: 2f);
    }

    private void StartLevel(int level)
    {
        levelTileMaps = MapGenerator.Instance.GenerateMap(levels[level]);
        SpawnDie(levelTileMaps[1].GetStartTile(), 2);
        mainCameraFollower.SetFocus(dice.transform);
        ResetSteps();
    }

    private void SpawnDie((int, int) spawnPos, int floor)
    {
        GameObject diceObj = Instantiate(dicePrefab, TileToWorldPosition(floor, spawnPos.Item1, spawnPos.Item2), new Quaternion(0, 0, 0, 1));
        dice = diceObj.GetComponent<Dice>();
        dicePos = spawnPos;
        diceFloor = floor;
    }

    private IEnumerator RestartEnumerator(int level, float delay = 0)
    {
        yield return new WaitForSeconds(delay);


        Destroy(dice.gameObject);
        EffectsAndOverlaysManager.Instance.ClearHighlights();
        SpawnDie(levelTileMaps[1].GetStartTile(), 2);
        mainCameraFollower.SetFocus(dice.transform);
        ResetSteps(restarting: true);
    }

}
