using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Carrousel : MonoBehaviour
{
    private bool b_can_swipe;
    private Vector2 startPosition;
    private Vector2 endPosition;
    public float f_swipe_distance;


    private void Update()
    {
        if (b_can_swipe)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                endPosition = Input.mousePosition;
                if (Vector2.Distance(startPosition, endPosition) > 0)
                {

                }
                if (Vector2.Distance(startPosition, endPosition) < 0)
                {

                }
            }
        }
    }
}
