using Assets.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Text _highScore = null;
    [SerializeField] private Text _title;
    [SerializeField] private Text _version;
    // Start is called before the first frame update
    void Start()
    {
        _highScore.text = PlayerPrefs.HasKey("highScore") ? PlayerPrefs.GetInt("highScore").ToString() : 0.ToString();
        _title.text = Config.instance.GetTitle();
        _version.text = Config.instance.GetVersionNumber();
    }


    public void loadGame()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
}
