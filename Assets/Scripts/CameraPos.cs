using UnityEngine;

public class CameraPos : MonoBehaviour
{
    private GameObject playerObj;

    void Update()
    {
        if (playerObj == null)
        {
            playerObj = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            transform.position = 
                new Vector3(playerObj.transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
