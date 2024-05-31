using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            GameManager.Instance.Coin += 10;
            Debug.Log("Player Coin : " + GameManager.Instance.Coin);
            Destroy(gameObject);
        }
        else if (gameObject.tag == "HP")
        {
            GameManager.Instance.PlayerHP += 10;
            Debug.Log("Player Coin : " + GameManager.Instance.PlayerHP);
            Destroy(gameObject);
        }
    }
}
