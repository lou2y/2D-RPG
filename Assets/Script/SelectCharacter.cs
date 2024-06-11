using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
    [Header("Info")]
    public Text NameTxt;
    public Text FeatureTxt;
    public Image CharImage;

    [Header("Character")]
    public GameObject[] Characters;
    public CharacterIno[] characterInfos;
    private int charIndex = 0;

    [Header("GameStart")]
    public GameObject GameStart;
    public Text GameCountTxt;
    public bool isPlayButtonCliked = false;
    private float gameCount = 5f;

    //public static string CharacterName;

    private void Update()
    {
        if (isPlayButtonCliked)
        {
            gameCount -= Time.deltaTime;
            if (gameCount <= 0)
            {
                SceneManager.LoadScene("MainScene");
            }
            GameCountTxt.text = $"곧 게임이 시작됩니다. \n {gameCount:F1}";
        }
    }

    public void PlayBtn()
    {
        GameStart.SetActive(true);
        isPlayButtonCliked = true;

        DEfine.Player player = (DEfine.Player)Enum.Parse(typeof(DEfine.Player), Characters[charIndex].name);

        GameManager.Instance.SelectedPlayer = player;
        //CharacterName = Characters[charIndex].name;
    }
    public void SelectCharacterBtn(string btnName)
    {
        Characters[charIndex].SetActive(false);
        if (btnName == "Next")
        {
            charIndex++;
            charIndex = charIndex % Characters.Length;
        }
        if (btnName == "Prev")
        {
            charIndex--;
            charIndex = charIndex % Characters.Length;
            charIndex = charIndex < 0 ? charIndex + Characters.Length : charIndex;
        }
        Debug.Log($"CharIndex : {charIndex}");
        Characters[charIndex].SetActive(true);
        SetPanelInfo();
    }

    private void SetPanelInfo()
    {
        NameTxt.text = characterInfos[charIndex].Name;
        FeatureTxt.text = characterInfos[charIndex].Feature;
        CharImage.sprite = Characters[charIndex].GetComponent<SpriteRenderer>().sprite;
    }

    private void Start()
    {
        SetPanelInfo();
    }

}

