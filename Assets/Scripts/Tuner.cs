using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using PitchDetector;

public class Tuner : MonoBehaviour
{
    [SerializeField] private InputManager _im = null;

    [SerializeField] private float _targetPitch = 0;

    [SerializeField] private TextMeshProUGUI _pitchTxt = null;
    [SerializeField] private TextMeshProUGUI _targetPitchTxt = null;
    [SerializeField] private TextMeshProUGUI _differenceTxt = null;
    [SerializeField] private TextMeshProUGUI _noteTxt = null;

    private bool _tuning = false;

    // Update is called once per frame
    void Update()
    {
        switch(_im.InputedNote)
        {
            case "G":
                TuneTo(392);
                _noteTxt.text = "G";
                break;
            case "C":
                TuneTo(261.6f);
                _noteTxt.text = "C";
                break;
            case "E":
                TuneTo(329.6f);
                _noteTxt.text = "E";
                break;
            case "A":
                TuneTo(440);
                _noteTxt.text = "A";
                break;
        }
        if (!_tuning) return;
        float difference = _targetPitch - _im.InputedPitch;
        _pitchTxt.text = _im.InputedPitch.ToString("0.##");
        _differenceTxt.text = difference.ToString("0.##");
        _differenceTxt.color = difference < -1 || difference > 1 ? Color.red : Color.green;
    }

    public void TuneTo(float targetPitch)
    {
        _tuning = true;
        _targetPitch = targetPitch;
        _targetPitchTxt.text = _targetPitch.ToString();
    }
}
