using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject wallPrefab;

    [Header("Level Scriptable Object")]
    [SerializeField] private Level level;

    private Color COLOR_TILE = Color.white;
    private Color COLOR_WALL = new Color(0, 0, 1);
    private Color COLOR_START = new Color(0, 1, 0);
    private Color COLOR_FINISH = new Color(1, 0, 0);

    void Start()
    {
        GenerateMap();
    }

    void Update()
    {
        
    }

    private void GenerateMap()
    {
        foreach(Texture2D floorTex in level.GetFloors())
        {

            Color[] pixels = floorTex.GetPixels(0);
            for (int y = 0; y < floorTex.height; ++y)
            {
                for (int x = 0; x < floorTex.width; ++x)
                {
                    int index = x + floorTex.width * y;

                    Color pixel = pixels[index];
                    Debug.Log(x + " " + y + " " + pixel);
                    if (pixel == Color.white && pixel == Color.green)
                    {
                        Instantiate(tilePrefab, new Vector3(x, 0, y), new Quaternion(0, 0, 0, 0));
                    }
                    else if (pixel == Color.blue)
                    {
                        Instantiate(tilePrefab, new Vector3(x, 0, y), new Quaternion(0, 0, 0, 0));
                        Instantiate(wallPrefab, new Vector3(x, 0.5f, y), new Quaternion(0, 0, 0, 0));
                    }
                }
            }


        }
    }

}
