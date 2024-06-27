using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEvent : MonoBehaviour
{
    
    void Update()
    {
        MouseClick();
    }

    private void MouseClick()
    {
        if(input.GetMouseButtenDown(0))
        {
            vector2 Pos = Camera.main.ScreenRoworldpoint(input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(Pos, vector2.zero, 0f);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.name == "PowerNPC")
                {
                    Debug.Log("Power NPC 선택");
                }
                else if (hit.collider.gameObject.name == "PotionNPC")
                {
                    Debug.Log("Potion NPC 선택");
                }
                else if (hit.collider.gameObject.name == "battleNPC")
                {
                    Debug.Log("battle NPC 선택");
                }
            }
        }
    }
}
