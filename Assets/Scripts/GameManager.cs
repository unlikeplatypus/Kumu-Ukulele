using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Song _song = null;
    [SerializeField] private float _startDelay = 0f;
    [SerializeField] private InputManager _im = null;


    private bool _start = false;

    private int _currentIndex = 0;

    private bool _pauseNotes = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayStart());
    }

    // Update is called once per frame
    void Update()
    {
        if (!_start) return;
        if(!_pauseNotes) StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        _pauseNotes = true;
        if (_currentIndex >= _song.Notes.Length)
        {
            _start = false;
        }
        else
        {
            if (_song.Notes[_currentIndex].IsNote)
            {
                SpawnNote();
            }
            else
            {
                yield return new WaitForSeconds((_song.Notes[_currentIndex] as Delay).Time);
            }
            _currentIndex++;
            _pauseNotes = false;
        }
    }
    private void SpawnNote()
    {
        int stringNum = (_song.Notes[_currentIndex] as Note).String;
        int fretNum = (_song.Notes[_currentIndex] as Note).Fret;
        int x = 0, y = 0;

        switch (stringNum)
        {
            case 1:
                y = -2;
                break;
            case 2:
                y = 0;
                break;
            case 3:
                y = 2;
                break;
            case 4:
                y = 4;
                break;
            default:
                break;
        }
        switch (fretNum)
        {
            case 0:
                x = -9;
                break;
            case 1:
                x = -7;
                break;
            case 2:
                x = -5;
                break;
            case 3:
                x = -3;
                break;
            case 4:
                x = -1;
                break;
            case 5:
                x = 1;
                break;
            case 6:
                x = 3;
                break;
            case 7:
                x = 5;
                break;
            case 8:
                x = 7;
                break;
            default:
                break;
        }
        Note note = Instantiate((_song.Notes[_currentIndex] as Note), new Vector3(x, y, 0), Quaternion.identity);
        note.Im = _im;
    }
    private IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(_startDelay);
        _start = true;
    }
}
