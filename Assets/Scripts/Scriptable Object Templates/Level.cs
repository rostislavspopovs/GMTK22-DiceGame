using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level")]
[SerializeField]
public class Level : ScriptableObject
{
    [Header("Levels")]
    [SerializeField] private List<Texture2D> floors = new List<Texture2D>();

    public int FloorNum => floors.Count;
    public List<Texture2D> GetFloors() => floors;
    public Texture2D GetFloor(int floor) => floors[floor];
    
}
