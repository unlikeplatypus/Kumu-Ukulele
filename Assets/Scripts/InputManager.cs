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
        var midis = RAPTPitchDetectorExtensions.HerzToMidi(pitchList);
        string[] notes = midis.NoteString().Split(new char[] {'0','1', '2', '3', '4', '5', '6', '7', '8', '9'});
        foreach(string s in notes)
        {
            switch (s)
            {
                case "C":
                    InputedNote = "C";
                    break;
                case "D":
                    InputedNote = "D";
                    break;
                case "B":
                    InputedNote = "B";
                    break;
                case "G":
                    InputedNote = "G";
                    break;
                case "A":
                    InputedNote = "A";
                    break;
                case "E":
                    InputedNote = "E";
                    break;
                case "F":
                    InputedNote = "F";
                    break;
                case "C#":
                    InputedNote = "C#";
                    break;
                case "D#":
                    InputedNote = "D#";
                    break;
                case "B#":
                    InputedNote = "B#";
                    break;
                case "G#":
                    InputedNote = "G#";
                    break;
                case "A#":
                    InputedNote = "A#";
                    break;
                case "E#":
                    InputedNote = "E#";
                    break;
                case "F#":
                    InputedNote = "F#";
                    break;
                default:
                    break;
            }
        }
    }
}
