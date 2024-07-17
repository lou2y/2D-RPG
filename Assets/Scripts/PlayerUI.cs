using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Image CharacterImg;
    public Text IdText;

    public Slider HpSlider;
    public Slider MpSlider;
    public Slider ExpSlider;
    private GameObject player;

    void Start()
    {
        GameObject spawnPos = GameObject.FindGameObjectWithTag("initPos");
        IdText.text = GameManager.Instance.UserID;
        player = GameManager.Instance.SpawnPlayer(spawnPos.transform);
    }

    void Update()
    {
        display();
    }

    private void display()
    {
        CharacterImg.sprite = player.GetComponent<SpriteRenderer>().sprite;
        HpSlider.value = GameManager.Instance.PlayerHP;
        MpSlider.value = GameManager.Instance.PlayerMP;
        ExpSlider.value = GameManager.Instance.PlayerExp;
    }
}
