 using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private static GameManager _instance;
	public static GameManager Instance
	{
		get
		{
			return _instance;
		}
	}

	public int level = 1;//当前关卡
	public int food  = 100;


	void Awake()
	{
		_instance = this;
	}

	public void ReduceFood(int count)
	{
		food-=count;
	}

	public void AddFood(int count)
	{
		food+=count;
	}
}
