using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string CharacterName;
    public string UserID;

    public float PlayerHP = 100f;
    public float PlayExp = 1f;
    public int Coin = 0;

    private GameManager player;


    private void Start()
    {
        UserID = PlayerPrefs.GetString("ID");
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(Instance);
    }

    public GameObject SpawnPlyer(Transform spawnPos)
    {
        GameObject playerPrefab = Resources.Load<GameObject>("Character/" + GameManager.Instance.CharacterName);
        GameObject player = Instantiate(playerPrefab, spawnPos.position, spawnPos.rotation);

        return player;
    }
}
