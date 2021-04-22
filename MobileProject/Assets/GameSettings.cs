using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Data/Settings", order = 3)]
public class GameSettings : ScriptableObject
{
    public bool paused;

    public Area area;
    public int stage;
}
