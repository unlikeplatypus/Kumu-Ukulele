using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using PitchDetector;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private InputManager _Im = null;


    [SerializeField] private GameObject _titleScreen = null;
    [SerializeField] private GameObject _menuScreen = null;
    [SerializeField] private GameObject _settingsScreen = null;
    [SerializeField] private GameObject _tunerScreen = null;

    [SerializeField] private GameObject[] _menuButtons = null;
    [SerializeField] private GameObject[] _settingButtons = null;

    private int _index = 0;

    private bool _titleScreenPressed = false;

    private bool _delayed = false;

    void Start()
    {
        Time.timeScale = 1;
        _titleScreen.SetActive(true);
        _menuScreen.SetActive(false);
        _settingsScreen.SetActive(false);
        _tunerScreen.SetActive(false);
    }

    void Update()
    {
        if(!_titleScreenPressed && _Im.InputedNote != "")
        {
            _titleScreen.SetActive(false);
            _menuScreen.SetActive(true);
            _titleScreenPressed = true;
        }
        if(_menuScreen.activeInHierarchy)
        {
            MenuNav(_menuButtons);
        }
        else if(_settingsScreen.activeInHierarchy)
        {
            MenuNav(_settingButtons);
        }
    }

    private void MenuNav(GameObject[] items)
    {
        if (_Im.InputedNote == "G")
        {
            if (_index - 1 < 0)
            {
                _index = items.Length - 1;
            }
            else
            {
                _index--;
            }
            EventSystem.current.SetSelectedGameObject(items[_index]);
            StartCoroutine(DelayInput(0.5f));
        }
        else if (_Im.InputedNote == "A")
        {
            if (_index + 1 >= items.Length)
            {
                _index = 0;
            }
            else
            {
                _index++;
            }
            EventSystem.current.SetSelectedGameObject(items[_index]);
            StartCoroutine(DelayInput(0.5f));
        }
        else if (_Im.InputedNote == "C")
        {
            if(items[_index].GetComponent<Button>() != null)
            {
                items[_index].GetComponent<Button>().onClick.Invoke();
                StartCoroutine(DelayInput(0.5f));
            }
            else if(items[_index].GetComponent<Slider>() != null)
            {
                items[_index].GetComponent<Slider>().value++;
                StartCoroutine(DelayInput(0.1f));
            }
        }
        else if (_Im.InputedNote == "E")
        {
            if (items[_index].GetComponent<Slider>() != null)
            {
                items[_index].GetComponent<Slider>().value--;
            }
            StartCoroutine(DelayInput(0.1f));
        }
    }
    public void ToggleSettings()
    {
        _index = 0;
        _menuScreen.SetActive(!_menuScreen.activeInHierarchy);
        _settingsScreen.SetActive(!_settingsScreen.activeInHierarchy);
    }
    public void ToggleTuner()
    {
        _menuScreen.SetActive(!_menuScreen.activeInHierarchy);
        _tunerScreen.SetActive(!_tunerScreen.activeInHierarchy);
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    private IEnumerator DelayInput(float delay)
    {
        _delayed = true;
        _Im.GetComponent<MicrophonePitchDetector>().ToggleRecord();
        _Im.InputedNote = "";
        yield return new WaitForSeconds(delay);
        _Im.GetComponent<MicrophonePitchDetector>().ToggleRecord();
        _delayed = false;
    }
}
