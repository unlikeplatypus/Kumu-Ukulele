using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameOverManager : MonoBehaviour
{
    [SerializeField] private RunnerGameManager _gm = null;
    [SerializeField] private InputManager _im = null;
    [SerializeField] private SceneChanger _changer = null;

    [SerializeField] private TextMeshProUGUI _scoreTxt = null;
    [SerializeField] private TextMeshProUGUI _highScoreTxt = null;

    void Update()
    {
        if (!gameObject.activeInHierarchy) return;
        _scoreTxt.text = $"Final Score: {string.Format("{0:D5}", _gm.Score)}";
        if (_gm.Score > PlayerPrefs.GetInt("HighScore")) PlayerPrefs.SetInt("HighScore", _gm.Score);
        _highScoreTxt.text = $"Best Score: {string.Format("{0:D5}", PlayerPrefs.GetInt("HighScore"))}";
        if (_im.InputedNote != "") _changer.ChangeScene();
    }
}
