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
            if(pitchList[i] > 385 && pitchList[i] < 400)
            {
                InputedNote = "G";
                InputedPitch = pitchList[i];

            }
            else if(pitchList[i] > 250 && pitchList[i] < 265)
            {
                InputedNote = "C";
                InputedPitch = pitchList[i];

            }
            else if (pitchList[i] > 325 && pitchList[i] < 340)
            {
                InputedNote = "E";
                InputedPitch = pitchList[i];

            }
            else if (pitchList[i] > 435 && pitchList[i] < 450)
            {
                InputedNote = "A";
                InputedPitch = pitchList[i];

            }
            else
            {
                InputedNote = "";
            }
        }
    }
}
