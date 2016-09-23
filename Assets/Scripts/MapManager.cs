using UnityEngine;
using System.Collections;

public class MapManager : MonoBehaviour {

	public GameObject[] outWallArray;
	public GameObject[] floorArray;
	public int rows=10;
	public int cols=10;

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
		for (int x = 0; x < cols; x++) 
		{
			for (int y = 0; y < rows; y++) 
			{
				initTile (x, y);
			}
		}
	}

	void initTile(int xPos,int yPos)
	{
		//初始化外围墙
		if (xPos == 0 || yPos == 0 || xPos == cols - 1 || yPos == rows - 1) {
			int index = Random.Range (0, outWallArray.Length);
			GameObject.Instantiate (outWallArray [index], new Vector3 (xPos, yPos, 0), Quaternion.identity);
		}

		//初始化地板
		else 
		{
			int index = Random.Range (0, floorArray.Length);
			GameObject.Instantiate (floorArray [index], new Vector3 (xPos, yPos, 0), Quaternion.identity);

		}
	}

}
