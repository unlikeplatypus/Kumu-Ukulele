using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PitchDetector;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField] private MicrophonePitchDetector _micDetector = null;

    private string _inputedNote = "";
    private float _inputedPitch = 0;

    public string InputedNote { get => _inputedNote; set => _inputedNote = value; }
    public float InputedPitch { get => _inputedPitch; set => _inputedPitch = value; }

    private void Start()
    {
        _micDetector.StartMic();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _micDetector.ToggleRecord();
            print(_micDetector.Record);
        }
    }
    public void ManagePitch(List<float> pitchList, int samples, float db)
    {
        for (int i = 0; i < pitchList.Count; i++)
        {
            print(pitchList[i]);
            if(pitchList[i] > 390 && pitchList[i] < 394)
            {
                InputedNote = "G";
                InputedPitch = pitchList[i];
                break;

            }
            else if(pitchList[i] > 259 && pitchList[i] < 263)
            {
                InputedNote = "C";
                InputedPitch = pitchList[i];
                break;

            }
            else if (pitchList[i] > 327 && pitchList[i] < 331)
            {
                InputedNote = "E";
                InputedPitch = pitchList[i];
                break;

            }
            else if (pitchList[i] > 430 && pitchList[i] < 450)
            {
                InputedNote = "A";
                InputedPitch = pitchList[i];
                break;

            }
            else
            {
                InputedNote = "";
            }
        }
    }
}
