using UnityEngine;
using System.Collections;

public class Globals: MonoBehaviour {

	public static PlayGrid PlayGrid_PG;


	public static GameObject[,] Grid__;
	// public Enum GameState{ START, END };

	public static GameObject SelectedShip_GO;

	public static GameObject PlayerOne;
	public static GameObject PlayerTwo;

	public static GameState Game_E;
	// Ships

	void Start()
	{
		PlayerOne = GameObject.Find("PlayerOne");
		PlayerTwo = GameObject.Find("PlayerTwo");
		PlayGrid_PG = GetComponent< PlayGrid >();
	}
}
