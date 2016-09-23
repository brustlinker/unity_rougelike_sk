using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapManager : MonoBehaviour {

	public GameObject[] outWallArray;
	public GameObject[] floorArray;
	public int rows=10;
	public int cols=10;
	private Transform mapHolder;

	private List<Vector2> positionList = new List<Vector2>(); 

	// Use this for initialization
	void Start () 
	{
		InitMap ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void InitMap()
	{
		mapHolder = new GameObject ("Map").transform; 
		for (int x = 0; x < cols; x++) 
		{
			for (int y = 0; y < rows; y++) 
			{
				initTile (x, y);

			}
		}



		//创建障碍物，敌人和水
		positionList.Clear();
		for (int x = 2; x < cols - 2;x++)
		{
			for (int y = 2; y < rows - 2; y++)
			{
				
			}
			
		}
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
			GameObject.Instantiate (floorArray [index], new Vector3 (xPos, yPos, 0), Quaternion.identity);

		}



	}

}
