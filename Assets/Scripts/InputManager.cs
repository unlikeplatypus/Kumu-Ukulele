using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PitchDetector;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField] private MicrophonePitchDetector _micDetector = null;

    private string _inputedNote = "";

    public string InputedNote { get => _inputedNote; set => _inputedNote = value; }

    private void Start()
    {
        _micDetector.ToggleRecord();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _micDetector.ToggleRecord();
        }
    }
    public void ManagePitch(List<float> pitchList, int samples, float db)
    {
        for (int i = 0; i < pitchList.Count; i++)
        {
            if(pitchList[i] > 385 && pitchList[i] < 400)
            {
                InputedNote = "G";
            }
            else if(pitchList[i] > 256 && pitchList[i] < 260)
            {
                InputedNote = "C";
            }
            else if (pitchList[i] > 325 && pitchList[i] < 340)
            {
                InputedNote = "E";
            }
            else if (pitchList[i] > 435 && pitchList[i] < 450)
            {
                InputedNote = "A";
            }
        }
    }
}
