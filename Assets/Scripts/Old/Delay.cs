using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delay : NoteType
{
    [SerializeField] private float time;

    public float Time
    {
        get { return time; }
        set { time = value; }
    }

}
