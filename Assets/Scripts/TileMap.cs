using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap 
{
    public enum TileState { VOID, TILE, START, FINISH, WALL}

    private TileState[,] map;
    private int width;
    private int depth;

    public TileMap(int width, int depth)
    {
        map = new TileState[width, depth];
        this.width = width;
        this.depth = depth;
    }

    public void SetTileState(int x, int y, TileState state)
    {
        map[x, y] = state;
    }

    public bool IsVoid(int x, int y)
    {
        if(x < 0 || x > width || y < 0 || y > width) return true;
        return map[x, y] == TileState.VOID;
    }

    public bool IsWall(int x, int y)
    {
        return map[x, y] == TileState.WALL;
    }

}
