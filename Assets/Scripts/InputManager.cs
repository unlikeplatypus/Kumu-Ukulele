using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PitchDetector;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField] private MicrophonePitchDetector _micDetector = null;
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
                    print("A C was played");
                    break;
                case "D":
                    print("A D was played");
                    break;
                case "B":
                    print("A B was played");
                    break;
                case "G":
                    print("A G was played");
                    break;
                case "A":
                    print("An A was played");
                    break;
                case "E":
                    print("An E was played");
                    break;
                case "F":
                    print("An F was played");
                    break;
                case "C#":
                    print("A C# was played");
                    break;
                case "D#":
                    print("A D# was played");
                    break;
                case "#B":
                    print("A B# was played");
                    break;
                case "G#":
                    print("A G# was played");
                    break;
                case "A#":
                    print("An A# was played");
                    break;
                case "E#":
                    print("An E# was played");
                    break;
                case "F#":
                    print("An F# was played");
                    break;
                default:
                    break;
            }
        }
    }
}
