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

    void Start()
    {
        StartLevel(0);
    }

    void Update()
    {
        
    }

    public void StartLevel(int level)
    {
        MapGenerator.Instance.GenerateMap(levels[level]);
    }
}
