using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {
    
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            maximum = max;
            minimum = min;
        }
    }

    public int columns = 12;
    public int rows = 8;

    public Count obstacleCount = new Count(5, 9);

    public GameObject[] floorTiles;
    public GameObject[] obstacles;
    public GameObject[] outerWallTiles;
    public GameObject[] enemies;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    void initializeList()
    {
        gridPositions.Clear();
        for(int x = 0; x<columns; x++)
        {
            for(int y = 0; y<rows; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;

        for (int x = -1; x < columns+1; x++)
        {
            for (int y = -1; y < rows+1; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                if(x == -1 || x == columns || y == -1 || y == rows)
                {
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                }
                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                instance.transform.parent = boardHolder;
            }
        }

    }

    Vector3 RandomPosition()
    {
        int randomindex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomindex];
        gridPositions.RemoveAt(randomindex);
        return randomPosition;
    }

    /**
    /*  Aligns enemies to the right border. This function needs to be improved so that enemies appear in various formations
    **/
    Vector3 EnemyPosition()
    {
        Vector3 enemyPosition = new Vector3(0f,0f,0f);
        for (int i = 0; i < gridPositions.Count; i++)
        {
            if(gridPositions[i].x == columns - 1)
            {
                enemyPosition = gridPositions[i];
                gridPositions.RemoveAt(i);
                break;
            }
        }
        return enemyPosition;
    }

    Vector3 ObstaclePosition()
    {
        Vector3 obstaclePosition = new Vector3(0f, 0f, 0f);

        while(true) //Keeps repeating as long as we get a position that is not at the borders where units appear
        {
            int randomindex = Random.Range(0, gridPositions.Count);
            obstaclePosition = gridPositions[randomindex];
            if (obstaclePosition.x !=0 && obstaclePosition.x!= columns-1)   //If the position is not at the borders
            {
                gridPositions.RemoveAt(randomindex);    //remove the position
                break;  //break the loop
            }
        }
        return obstaclePosition;
    }

    void LayoutObjectAtRandom(GameObject[] array, int minimum, int maximum)
    {
        int objectCount = Random.Range(minimum, maximum + 1);
        
        for(int i = 0; i<objectCount; i++)
        {
            Vector3 randomPosition;
            if (array[0].name == "chr_Enemy")
            {                
               //ebug.Log("Hello " + array[0].transform.position.ToString(), array[0]);
                randomPosition = EnemyPosition();   //Not so random in case of enemies
            }
            else if (array[0].tag == "Obstacles")
            {
                //ebug.Log("Hello " + array[0].transform.position.ToString(), array[0]);
                randomPosition = ObstaclePosition();   //Not so random in case of enemies
            }
            else
            {
                randomPosition = RandomPosition();
            }
            GameObject tileChoice = array[Random.Range(0, array.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
        
    }

    public void SetupScene(int level)
    {
        BoardSetup();
        initializeList();
        LayoutObjectAtRandom(obstacles, obstacleCount.minimum, obstacleCount.maximum);
        int enemyCount = (int)Mathf.Log(level, 2f);
        LayoutObjectAtRandom(enemies, enemyCount, enemyCount);
    }
}
