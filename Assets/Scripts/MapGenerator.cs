using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : Singleton<MapGenerator>
{
    [Header("Map Parent")]
    [SerializeField] private Transform mapParent;
    [Header("Prefabs")]
    [SerializeField] private GameObject[] tilePrefabs;
    [SerializeField] private GameObject[] wallPrefabs;
    [SerializeField] private GameObject finishPrefab;

    public List<TileMap> GenerateMap(Level level)
    {
        List<TileMap> tileMaps = new List<TileMap>();

        int f = 1;
        foreach(Texture2D floorTex in level.GetFloors())
        {

            TileMap floorMap = new TileMap(floorTex.width, floorTex.height);
            GameObject floorParent = Instantiate(new GameObject("Floor " + f), mapParent);
            

            Color[] pixels = floorTex.GetPixels(0);
            for (int y = 0; y < floorTex.height; ++y)
            {
                for (int x = 0; x < floorTex.width; ++x)
                {
                    int index = x + floorTex.width * y;

                    Color pixel = pixels[index];

                    if (pixel != Color.black)
                    {
                        GameObject tilePrefab = tilePrefabs[Random.Range(0, tilePrefabs.Length)];
                        GameObject tileObject = Instantiate(tilePrefab, new Vector3(x, f*4+0, y), new Quaternion(0, 0, 0, 0), floorParent.transform);
                        floorMap.SetTileState(x, y, TileMap.TileState.TILE);
                        
                        tileObject.transform.RotateAround(tileObject.transform.position, tileObject.transform.up, 90f * Random.Range(0, 5));
                    }
                    if (pixel == Color.blue)
                    {
                        GameObject wallPrefab = wallPrefabs[Random.Range(0, wallPrefabs.Length)];
                        Instantiate(wallPrefab, new Vector3(x, f*4+0.5f, y), new Quaternion(0, 0, 0, 0), floorParent.transform);
                        floorMap.SetTileState(x, y, TileMap.TileState.WALL);
                    }
                    if (pixel == Color.green)
                    {
                        floorMap.SetTileState(x, y, TileMap.TileState.START);
                    }
                    if (pixel == Color.red)
                    {
                        Instantiate(finishPrefab, new Vector3(x, f*4+0.5f, y), new Quaternion(0, 0, 0, 0), floorParent.transform);
                        floorMap.SetTileState(x, y, TileMap.TileState.FINISH);
                    }
                }
            }
            tileMaps.Add(floorMap);
            f++;
        }

        return tileMaps;
    }

    public void ClearMap()
    {
        int childCount = mapParent.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            Destroy(mapParent.GetChild(i).gameObject);
        }
    }

}
