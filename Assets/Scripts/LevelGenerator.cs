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
    [SerializeField] int maxEnemiesInRoom;

    [SerializeField] GameObject[] pillars;
    [SerializeField] int maxPillarsInRoom;

    [SerializeField] GameObject altarPrefab;

    [SerializeField] GameObject playerPrefab1;
    [SerializeField] GameObject playerPrefab2;

    HashSet<Vector3> roomsPositions = new();
    HashSet<Vector3> horizontalCorridorPosition = new();

    List<Vector3>[] depths;
    List<Vector3>[] corridorDepths;

    Vector3 roomAdditionDirection = new Vector3(-1.0f, 0.0f, 0.0f);
    Vector3 sprawlDirection = new Vector3(0.0f, 0.0f, 1.0f);

    List<GameObject> rooms = new();

    GameObject Level;
    GameObject Rooms;
    GameObject Corridors;

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
        Rooms = new();
        Corridors = new();

        Level.name = "Level";
        Rooms.name = "Rooms";
        Corridors.name = "Corridors";

        Rooms.transform.parent = Level.transform;
        Corridors.transform.parent = Level.transform;

        GenerateRooms();
        CreateCorridorsHorizontal();
        ConnectDepths();

        roomsPositions.Clear();
        horizontalCorridorPosition.Clear();
        for (int i = 0; i < depth; i++)
        {
            depths[i].Clear();
            corridorDepths[i].Clear();
        }

        Populate();

        Level.transform.eulerAngles = new Vector3(0.0f, 225.0f, 0.0f);
    }

    void NewRoom(int sign, Vector3 previousPosition, int currentWidth, int depth)
    {
        if (currentWidth < width)
        {
            Vector3 newPosition = previousPosition + sign * sprawlDirection * 2 * roomSize;
            roomsPositions.Add(newPosition);
            depths[depth].Add(newPosition);
            currentWidth++;
            float next = Random.Range(0.0f, 1.0f);
            if (next > sprawl)
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

            while (roomsPositions.Contains(currentPosition + 2 * sprawlDirection * roomSize))
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
            go.transform.parent = Corridors.transform;
        }
    }

    void ConnectDepths()
    {
        for (int i = 0; i < depths.Length; i++)
        {
            for (int j = 0; j < depths[i].Count; j++)
            {
                var bottomRoom = depths[i][j] + roomAdditionDirection * 2 * roomSize;
                if (roomsPositions.Contains(bottomRoom))
                {
                    corridorDepths[i].Add(depths[i][j] + roomAdditionDirection * roomSize);
                }
            }
        }

        for (int i = 0; i < corridorDepths.Length; i++)
        {
            int len = corridorDepths[i].Count;
            int amount = Random.Range(1, len);

            for (int j = 0; j < amount; j++)
            {
                GameObject go = Instantiate(corridorPrefab);
                go.transform.position = corridorDepths[i][j];
                go.transform.parent = Corridors.transform;
            }
        }
    }

    void GenerateRooms()
    {
        roomsPositions.Add(startingPosition);
        depths[0].Add(startingPosition);
        var previous = startingPosition + roomAdditionDirection * 2 * roomSize;
        roomsPositions.Add(previous);

        for (int i = 1; i < depth; i++)
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


        foreach (var a in roomsPositions)
        {
            GameObject go = Instantiate(roomPrefab);
            go.transform.position = a;
            go.transform.parent = Rooms.transform;
            rooms.Add(go);
        }
    }

    void Populate()
    {
        GameObject startingAltar = Instantiate(altarPrefab);
        startingAltar.transform.position = new Vector3(-5.0f, 0.0f, 0.0f);

        bool first = true;
        foreach (var room in rooms)
        {
            var roomManager = room.GetComponent<RoomManager>();
            if (!first)
            {

                int enemiesNum = Random.Range(0, maxEnemiesInRoom);
                int pillarNum = Random.Range(0, maxPillarsInRoom);

                for (int i = 0; i < pillarNum; i++)
                {
                    Vector3 upBound = roomManager.bounds[0].transform.position;
                    Vector3 botBound = roomManager.bounds[1].transform.position;

                    float x = Random.Range(upBound.x, botBound.x);
                    float z = Random.Range(upBound.z, botBound.z);

                    int enemyType = Random.Range(0, pillars.Length);

                    GameObject pillar = Instantiate(pillars[enemyType]);
                    pillar.transform.position = new Vector3(x, 0, z);
                    pillar.transform.parent = room.transform;
                    roomManager.pillars.Add(pillar);
                }

                roomManager.pillarNum = roomManager.pillars.Count;

                if (enemiesNum == 0)
                {
                    GameObject altar = Instantiate(altarPrefab);
                    altar.transform.position = roomManager.gameObject.transform.position;
                    altar.transform.parent = room.transform;

                }
                else
                {
                    Vector3 upBound = roomManager.bounds[0].transform.position;
                    Vector3 botBound = roomManager.bounds[1].transform.position;

                    for (int i = 0; i < enemiesNum; i++)
                    {
                        float x = Random.Range(upBound.x, botBound.x);
                        float z = Random.Range(upBound.z, botBound.z);

                        int enemyType = Random.Range(0, enemiesPrefabs.Length);

                        GameObject enemy = Instantiate(enemiesPrefabs[enemyType]);
                        enemy.transform.position = new Vector3(x, 0, z);
                        enemy.transform.parent = room.transform;
                        enemy.GetComponent<EnemyScript>().dropRate = roomManager.pillarNum;
                        roomManager.enemies.Add(enemy);
                    }

                    roomManager.enemyNum = roomManager.enemies.Count;
                }
            }
            else
            {
                first = false;
                print(room.transform.position);
                room.transform.name = "Starting room";
            }
        }
    }
}
