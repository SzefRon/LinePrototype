using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameController : MonoBehaviour
{
    private bool isMinigameActive = false;

    private Image Player1Rect;
    private Image Player2Rect;




    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        Player1Rect = GameObject.Find("Player1").GetComponent<Image>();
        Player2Rect = GameObject.Find("Player2").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMinigameActive)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Player1Rect.fillAmount += 0.1f;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Player1Rect.fillAmount -= 0.1f;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Player2Rect.fillAmount += 0.1f;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Player2Rect.fillAmount -= 0.1f;
            }
        }
    }
}
