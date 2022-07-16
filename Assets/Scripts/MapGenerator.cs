using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : Singleton<MapGenerator>
{
    [Header("Prefabs")]
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject finishPrefab;

    private List<TileMap> GenerateMap(Level level)
    {
        List<TileMap> tileMaps = new List<TileMap>();

        int f = 1;
        foreach(Texture2D floorTex in level.GetFloors())
        {

            TileMap floorMap = new TileMap(floorTex.width, floorTex.height);

            Color[] pixels = floorTex.GetPixels(0);
            for (int y = 0; y < floorTex.height; ++y)
            {
                for (int x = 0; x < floorTex.width; ++x)
                {
                    int index = x + floorTex.width * y;

                    Color pixel = pixels[index];
                    //Debug.Log(x + " " + y + " " + pixel);
                    if (pixel != Color.black)
                    {
                        Instantiate(tilePrefab, new Vector3(x, f*4+0, y), new Quaternion(0, 0, 0, 0));
                        floorMap.SetTileState(x, y, TileMap.TileState.TILE);
                    }
                    if (pixel == Color.blue)
                    {
                        Instantiate(wallPrefab, new Vector3(x, f*4+0.5f, y), new Quaternion(0, 0, 0, 0));
                        floorMap.SetTileState(x, y, TileMap.TileState.WALL);
                    }
                    if (pixel == Color.red)
                    {
                        Instantiate(finishPrefab, new Vector3(x, f*4+0.5f, y), new Quaternion(0, 0, 0, 0));
                        floorMap.SetTileState(x, y, TileMap.TileState.FINISH);
                    }
                }
            }
            tileMaps.Add(floorMap);
            f++;
        }

        return tileMaps;
    }

}
