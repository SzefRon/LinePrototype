using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;



public class MinigameController : MonoBehaviour
{
    public bool isMinigameActive = false;
    public bool chocking = false;

    private GameObject Player1Rect;
    private GameObject Player2Rect;

    public Transform square1;
    public Transform square2;

    public float speed = 1.0f;
    private RectTransform Bar;



    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        Player1Rect = GameObject.Find("Player1");
        Player2Rect = GameObject.Find("Player2");

        Bar = GetComponent<RectTransform>();

        //Player1Rect.GetComponent<BoxCollider2D>().enabled = true;
        //Player2Rect.GetComponent<BoxCollider2D>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMinigameActive)
        {
            StartMinigame();
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.M))
            {
                Player1Rect.GetComponent<BoxCollider2D>().enabled = true;
                Player2Rect.GetComponent<BoxCollider2D>().enabled = true;
            }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("choked!!!");
    }

    public void StartMinigame()
    {
        float position1 = Mathf.PingPong(Time.time * speed, 1); // Normalizowana wartoœæ od 0 do 1
        square1.position = new Vector3(Bar.rect.width * position1, square1.position.y, square1.position.z);

        float position2 = 1 - Mathf.PingPong(Time.time * speed, 1); // Normalizowana wartoœæ od 1 do 0
        square2.position = new Vector3(Bar.rect.width * position2, square2.position.y, square2.position.z);

    }

}
