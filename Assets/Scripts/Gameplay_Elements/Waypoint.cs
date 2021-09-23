using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] string fromPreviousCheckPosintDirection = "Right";

    public string GetFromPreviousWaypointDirection()
    {
        return fromPreviousCheckPosintDirection;
    }
}
