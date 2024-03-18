using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    //public List<GameObject> doors;
    public List<GameObject> enemies;
    public List<GameObject> bounds;

    int enemyNum;

    private void Awake()
    {
        //OpenDoors(); 
    }

   /* void OpenDoors()
    {
        *//*foreach (GameObject door in doors)
        {
            door.GetComponent<MeshRenderer>().enabled = false;
            door.GetComponent<BoxCollider>().enabled = false;
        }*//*
    }

    void CloseDoors()
    {
        foreach (GameObject door in doors)
        {
            door.GetComponent<MeshRenderer>().enabled = true;
            door.GetComponent<BoxCollider>().enabled = true;
        }
    }*/
}
