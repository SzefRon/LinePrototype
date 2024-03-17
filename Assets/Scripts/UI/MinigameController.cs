using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class MinigameController : MonoBehaviour
{
    public bool isMinigameActive = false;
    public bool chocking = false;

    private RectTransform Player1Rect;
    private RectTransform Player2Rect;
    private RectTransform BackGround;



    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        Player1Rect = GameObject.Find("Player1").GetComponent<RectTransform>();
        Player2Rect = GameObject.Find("Player2").GetComponent<RectTransform>();
        BackGround = GameObject.Find("Background").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMinigameActive)
        {
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.M))
            {
                if (checkForCollision())
                {
                    
                    chocking = true;
                }
            }
            
        }
    }

    bool checkForCollision()
    {
        //check for collision on X with two rect
        if (Player1Rect.position.x + Player1Rect.rect.width / 2 > Player2Rect.position.x - Player2Rect.rect.width / 2 &&
                       Player1Rect.position.x - Player1Rect.rect.width / 2 < Player2Rect.position.x + Player2Rect.rect.width / 2)
        {
            return true;
        }
        else
        {
            Debug.Log("No collision");
            return false;
        }
    }
}
