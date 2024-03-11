using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    bool isOpen = false;

    public List<GameObject> enemies = new List<GameObject>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen)
            foreach (GameObject enemy in enemies)
            {
                enemy.SetActive(true);
            }
        else
            foreach (GameObject enemy in enemies)
            {
                enemy.SetActive(false);
            }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isOpen = true;
            Debug.Log("Room is open");
        }
    }
}
