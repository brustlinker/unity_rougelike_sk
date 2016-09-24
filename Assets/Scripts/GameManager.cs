using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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

	public List<Enemy> enemyList = new List<Enemy>();
	private bool sleepStep = true;

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


	public void OnPlayerMove()
	{
		if(sleepStep)
		{
			sleepStep = false;
		}
		else
		{
			sleepStep = true;

			foreach(var enemy in enemyList)
			{
				enemy.Move(); 
			}
		}
	}
}
