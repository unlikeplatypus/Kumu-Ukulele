using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteType : MonoBehaviour
{
    [SerializeField]private bool isNote;

    public bool IsNote
    {
        get { return isNote; }
        set { isNote = value; }
    }

}
