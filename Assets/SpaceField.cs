using UnityEngine;
using System.Collections;

public class SpaceField : MonoBehaviour {

	[HideInInspector]
	public Vector2 MapPosition_V2;

	public GameObject Ship_GO;

	public Material Selected_MAT;
	public Material Base_MAT;
	public Material Enemy_MAT;

	public GameObject ColorSignSelf;
	public GameObject ColorSignOther;

	public int PositionX;
	public int PositionY;

	void Start () 
	{
		Ship ship = GetComponentInChildren<Ship>();


		ColorSignSelf.SetActive( false );
		ColorSignOther.SetActive( false );

		if( ship )
		{
			
			Ship_GO = ship.gameObject;

			if( ship.gameObject.layer == 8 )
				ColorSignSelf.SetActive( true );
			if( ship.gameObject.layer == 9 )
				ColorSignOther.SetActive( true );
		}
	}

	public void ColorSign()
	{
		
		/*
		if( gameObject.transform.parent.GetComponent< Grid >().IsHelpGrid_B )
		{
			ColorSignSelf.SetActive( true );
			return;
		}
		*/


		if( Ship_GO == null )
		{
			ColorSignSelf.SetActive( false );
			ColorSignOther.SetActive( false );
			return;
		}

		if( Ship_GO.layer == 8 )
		{
			ColorSignSelf.SetActive( true );
			ColorSignOther.SetActive( false );
		}
		else
			if( Ship_GO.layer == 9 )
			{
				ColorSignSelf.SetActive( false );
				ColorSignOther.SetActive( true );
			}
			else
			{
				ColorSignSelf.SetActive( false );
				ColorSignOther.SetActive( false );	
			}
	}

	public void Clear()
	{

		if( GetComponentInChildren<Ship>() == null )
			Ship_GO = null;
		else
			Ship_GO = GetComponentInChildren<Ship>().gameObject;
		
		gameObject.GetComponent<Renderer>().material = Base_MAT;
		ColorSign();

		/*
		if( gameObject.transform.parent.GetComponent< PlayGrid >().IsHelpGrid_B )
		{
			ColorSignSelf.SetActive( false );
			ColorSignOther.SetActive( false );
		}
		else
		{
			
		}
		*/
	}

	public bool IsCombat()
	{

		if( GetComponent<Renderer>().material.color == Enemy_MAT.color )
		{
			return true;
		}

		return false;
	}

	public bool IsPossible()
	{
		if( GetComponent<Renderer>().material.color == Selected_MAT.color ) return true;
		return false;
	}

	public bool HighLight()
	{
		if( Ship_GO )
		{
			if( Globals.SelectedShip_GO.layer != Ship_GO.layer )
				GetComponent<Renderer>().material = Enemy_MAT;

			if( Globals.SelectedShip_GO == Ship_GO )
				GetComponent<Renderer>().material = Selected_MAT;

			return false;
		}

		//if( gameObject.transform.parent.GetComponent( Grid ).IsHelpGrid_B )
		//	ColorSign();

		GetComponent<Renderer>().material = Selected_MAT;

		return true;

	}
		
	// Update is called once per frame
	void Update () {
	
	}
}
