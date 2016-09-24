using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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


	private Text foodText;
	private Text failText;
	private Player player;
	private MapManager mapManager;
	public bool isEnd = false;//是否到达终点

	void Awake()
	{
		_instance = this;
		InitGame();
		failText=GameObject.Find("FailText").GetComponent<Text>();
	}

	void InitGame()
	{
		foodText=GameObject.Find("FoodText").GetComponent<Text>();
		UpdateFoodText(0);
		failText = GameObject.Find("FailText").GetComponent<Text>();
		failText.enabled = false;
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		mapManager = GetComponent<MapManager>();

	}

	void UpdateFoodText(int foodChange)
	{
		if(foodChange==0)
		{
			foodText.text="Food:"+food;
		}
		else
		{
			string str="";
			if(foodChange<0)
			{
				str=foodChange.ToString();
			}
			else
			{
				str="+"+foodChange.ToString();
			}
			foodText.text=str+"  Food:"+food;
		}

	}

	public void ReduceFood(int count)
	{
		food-=count;
		UpdateFoodText(-count);
		if(food<0)
		{
			failText.enabled = true;
		}
	}

	public void AddFood(int count)
	{
		food+=count;
		UpdateFoodText(count);
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

		//检测有没有到达终点
		if(player.targetPos.x == mapManager.cols-2&&player.targetPos.y == mapManager.rows-2)
		{
			isEnd = true;

			//加载下一个管卡
		}

	}
}
