using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour
{
    public GameObject DialogueUI;

    private GameObject playerObj;
    private float distance;

    public GameObject[] DialogueTextObj;
    private int dIndex = 0;

    void Update()
    {
        if (playerObj == null) playerObj = GameManager.Instance.player;
        if (playerObj == null) return;

        distance = Vector2.Distance(transform.position, playerObj.transform.position);

        if (distance <= 3)
            DialogueUI.SetActive(true);
        else
            DialogueUI.SetActive(false);
    }

    public void NextBtn(string name)
    {
        DialogueTextObj[dIndex].SetActive(false);
        if (name == "Next")
        {
            if (dIndex < DialogueTextObj.Length - 1) dIndex++;
        }
        else if (name == "Prev")
        {
            if (dIndex > 0) dIndex--;
        }
        DialogueTextObj[dIndex].SetActive(true);
    }

    public void TownBtn()
    {
        SceneManager.LoadScene("TownScene");
    }
}
