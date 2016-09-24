using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapManager : MonoBehaviour {

	public GameObject[] outWallArray;
	public GameObject[] floorArray;
	public GameObject[] wallArray;
    public GameObject[] foodArray;
    public GameObject[] enemyArray;
    public GameObject   exitPrefab;
	public int rows=10;
	public int cols=10;

	public int minCountWall = 2;
	public int maxCountWall = 8;

	private Transform mapHolder;

	private List<Vector2> positionList = new List<Vector2>(); 

	private GameManager gameManager;


	public void InitMap()
	{
		gameManager = this.GetComponent<GameManager> ();
		mapHolder = new GameObject ("Map").transform; 
		for (int x = 0; x < cols; x++) 
		{
			for (int y = 0; y < rows; y++) 
			{
				initTile (x, y);

			}
		}



		//可创建障碍物，敌人和水的数组
		positionList.Clear();
		for (int x = 2; x < cols - 2;x++)
		{
			for (int y = 2; y < rows - 2; y++)
			{
				positionList.Add(new Vector2(x,y));
			}
			
		}

		//创建障碍物
		int wallCount = Random.Range(minCountWall,maxCountWall+1);
        InstanceItem(wallCount, wallArray);

		//创建食物2-level*2
		int footCount = Random.Range (2, gameManager.level * 2 + 1);
        InstanceItem(footCount, foodArray);

        //创建敌人 level/2
        int enemyCount = gameManager.level/2;
        InstanceItem(enemyCount, enemyArray);
  

        //创建出口
        GameObject exit=GameObject.Instantiate(exitPrefab,new Vector2(cols-2,rows-2),Quaternion.identity) as GameObject;
        exit.transform.SetParent(mapHolder);

	}

	void initTile(int xPos,int yPos)
	{
		//初始化外围墙
		if (xPos == 0 || yPos == 0 || xPos == cols - 1 || yPos == rows - 1) {
			int index = Random.Range (0, outWallArray.Length);
			GameObject tile= GameObject.Instantiate (outWallArray [index], new Vector3 (xPos, yPos, 0), Quaternion.identity) as GameObject;
			tile.transform.SetParent (mapHolder);
		}

		//初始化地板
		else 
		{
			int index = Random.Range (0, floorArray.Length);
			GameObject tile = GameObject.Instantiate (floorArray [index], new Vector3 (xPos, yPos, 0), Quaternion.identity) as GameObject;
			tile.transform.SetParent (mapHolder);

		}
	}
   
    private void InstanceItem(int count,GameObject[] prefabs)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 pos = RandomPosition();
            GameObject itemPrefab = RandomPrefab(prefabs);
            GameObject tile = GameObject.Instantiate (itemPrefab , new Vector3 (pos.x, pos.y, 0), Quaternion.identity) as GameObject;
            tile.transform.SetParent(mapHolder);
        }
    }

	Vector2 RandomPosition()
	{
		int positionIndex = Random.Range(0,positionList.Count);
		Vector2 pos = positionList[positionIndex];
		positionList.RemoveAt(positionIndex);
		return pos; 
	}

	GameObject RandomPrefab(GameObject[] prefabs)
	{
        int index = Random.Range(0, prefabs.Length);
        return prefabs[index];
	}

}
