using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap 
{
    public enum TileState { VOID, TILE, START, FINISH, WALL}

    private TileState[,] map;
    private int width;
    private int depth;

    private (int, int) startTile;
    private (int, int) finishTile;

    public TileMap(int width, int depth)
    {
        map = new TileState[width, depth];
        this.width = width;
        this.depth = depth;
    }

    public void SetTileState(int x, int y, TileState state)
    {
        map[x, y] = state;
        if (state == TileState.START) { startTile = (x, y); Debug.Log("Start Tile: " + x + " " + y); }
        else if (state == TileState.FINISH) { finishTile = (x, y); Debug.Log("Finish Tile: " + x + " " + y); }
    }

    public TileState GetTileState(int x, int y)
    {
        return map[x, y];
    }

    public bool IsVoid(int x, int y)
    {
        if(x < 0 || x >= width || y < 0 || y >= width) return true;
        return map[x, y] == TileState.VOID;
    }

    public bool IsWall(int x, int y)
    {
        return map[x, y] == TileState.WALL;
    }

    public bool IsSteppable(int x, int y) => !(IsVoid(x, y) || IsWall(x, y));

    public bool IsAtFinish(int x, int y) => (x, y) == finishTile;

    public (int, int) GetStartTile() => startTile;

    public (int, int) GetFinishTile() => finishTile;
 
}
