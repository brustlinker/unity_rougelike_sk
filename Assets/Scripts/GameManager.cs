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

	void Awake()
	{
		_instance = this;
		InitGame();
	}

	void InitGame()
	{
		foodText=GameObject.Find("FoodText").GetComponent<Text>();
		UpdateFoodText(0);

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
	}
}
