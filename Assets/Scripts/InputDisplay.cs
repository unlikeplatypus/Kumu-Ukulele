using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDisplay : MonoBehaviour
{
    [SerializeField] private Animator _string1, _string2, _string3, _string4 = null;

    [SerializeField] private InputManager _im = null;


    // Update is called once per frame
    void Update()
    {

        switch (_im.InputedNote)
        {
            case "G":
                _string1.SetTrigger("Move");
                
                break;
            case "C":
                _string2.SetTrigger("Move");
                break;
            case "E":
                _string3.SetTrigger("Move");
                break;
            case "A":
                _string4.SetTrigger("Move");
                break;
            default:
                break;
        }
    }
}
