using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject roomPrefab;
    [SerializeField] GameObject corridorPrefab;
    [SerializeField] float sprawl;
    [SerializeField] float roomSize;
    [SerializeField] float connectivity;
    [SerializeField] Vector3 startingPosition;
    [SerializeField] int width;
    [SerializeField] int depth;

    [SerializeField] GameObject[] enemiesPrefabs;

    HashSet<Vector3> roomsPositions = new();
    HashSet<Vector3> horizontalCorridorPosition = new();

    List<Vector3>[] depths;
    List<Vector3>[] corridorDepths;

    Vector3 roomAdditionDirection = new Vector3(-1.0f, 0.0f, 0.0f);
    Vector3 sprawlDirection = new Vector3(0.0f, 0.0f, 1.0f);

    List<GameObject> rooms = new();

    GameObject Level;

    private void Start()
    {
        depth++;
        depths = new List<Vector3>[depth];
        corridorDepths = new List<Vector3>[depth];

        for (int i = 0; i < depth; i++)
        {
            depths[i] = new List<Vector3>();
            corridorDepths[i] = new List<Vector3>();
        }

        Level = new GameObject();

        Generate();
        CreateCorridorsHorizontal();
        ConnectDepths();

        roomsPositions.Clear(); 
        horizontalCorridorPosition.Clear(); 
        for(int i = 0; i < depth; i++)
        {
            depths[i].Clear();
            corridorDepths[i].Clear();
        }

        Level.transform.eulerAngles = new Vector3(0.0f, 225.0f, 0.0f);
    }

    void NewRoom(int sign, Vector3 previousPosition, int currentWidth, int depth)
    {
        if(currentWidth < width) 
        {
            Vector3 newPosition = previousPosition + sign * sprawlDirection * 2 * roomSize;
            roomsPositions.Add(newPosition);
            depths[depth].Add(newPosition); 
            currentWidth++;
            float next = Random.Range(0.0f, 1.0f);
            if(next > sprawl) 
            {
                NewRoom(sign, newPosition, currentWidth, depth);
            }
        }
    }
    
    void CreateCorridorsHorizontal()
    {
        for (int i = 0; i < depths.Length; i++) 
        {
            Vector3 currentPosition = depths[i][0];
            
            while(roomsPositions.Contains(currentPosition + 2 * sprawlDirection * roomSize)) 
            {
                Vector3 corridorPosition = currentPosition + sprawlDirection * roomSize;
                horizontalCorridorPosition.Add(corridorPosition);
                currentPosition = currentPosition + 2 * sprawlDirection * roomSize;
            }

            currentPosition = depths[i][0];

            while (roomsPositions.Contains(currentPosition - 2 * sprawlDirection * roomSize))
            {
                Vector3 corridorPosition = currentPosition - sprawlDirection * roomSize;
                horizontalCorridorPosition.Add(corridorPosition);
                currentPosition = currentPosition - 2 * sprawlDirection * roomSize;
            }
        }

        foreach (var a in horizontalCorridorPosition)
        {
            GameObject go = Instantiate(corridorPrefab);
            go.transform.position = a;
            go.transform.eulerAngles = new Vector3(0.0f, -90.0f, 0.0f);
            go.transform.parent = Level.transform;
        }
    }

    void ConnectDepths()
    {
        for (int i = 0; i < depths.Length; i++)
        {
            for(int j = 0; j < depths[i].Count; j++)
            {
                var bottomRoom = depths[i][j] + roomAdditionDirection * 2 * roomSize;
                if (roomsPositions.Contains(bottomRoom))
                {
                    corridorDepths[i].Add(depths[i][j] + roomAdditionDirection * roomSize);
                }
            }
        }

        for(int i =0; i < corridorDepths.Length; i++)
        {
            int len = corridorDepths[i].Count;  
            int amount = Random.Range(1, len);
                
            for(int j = 0; j < amount; j++) 
            {
                GameObject go = Instantiate(corridorPrefab);
                go.transform.position = corridorDepths[i][j];
                go.transform.parent = Level.transform;
            }
        }
    }

    void Generate()
    {
        roomsPositions.Add(startingPosition);
        depths[0].Add(startingPosition);
        var previous = startingPosition + roomAdditionDirection * 2 * roomSize;
        roomsPositions.Add(previous);

        for(int i = 1; i < depth; i++)
        {
            int tempWidth = 0;
            float goLeft = Random.Range(0.0f, 1.0f);
            float goRight = Random.Range(0.0f, 1.0f);
            depths[i].Add(previous);

            if (goLeft > goRight)
            {
                if (goLeft > sprawl)
                {
                    NewRoom(-1, previous, tempWidth, i);
                }

                if (goRight > sprawl)
                {
                    NewRoom(1, previous, tempWidth, i);
                }
            }
            else
            {
                if (goRight > sprawl)
                {
                    NewRoom(1, previous, tempWidth, i);
                }

                if (goLeft > sprawl)
                {
                    NewRoom(-1, previous, tempWidth, i);
                }   
            }

            previous = previous + roomAdditionDirection * 2 * roomSize;
            roomsPositions.Add(previous);
        }



        foreach(var a in roomsPositions)
        {
            GameObject go = Instantiate(roomPrefab);
            go.transform.position = a;
            go.transform.parent = Level.transform;
            rooms.Add(go);
        }
    }
}
