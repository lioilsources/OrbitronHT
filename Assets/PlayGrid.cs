using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;


public class PlayGrid : MonoBehaviour {

	public GameObject Frame_GO;
	public Vector2 GridSize_V2;

	private bool IsCreated_B;
	public bool IsHelpGrid_B;

	// Use this for initialization
	void Start () {
		Globals.Grid__ = new GameObject[(int)GridSize_V2.x, (int)GridSize_V2.y];

		SpaceField[] objs = GameObject.FindObjectsOfType<SpaceField>();
		int i = 63;
		foreach( SpaceField sf in objs )
		{
			Globals.Grid__[i / 8,i % 8] = sf.gameObject;
			Globals.Grid__[i / 8,i % 8].GetComponent<SpaceField>().PositionY = i % 8;
			Globals.Grid__[i / 8,i % 8].GetComponent<SpaceField>().PositionX = i / 8;
			i--;
		}
	}

	public void CreateGrid()
	{
		Globals.Grid__ = new GameObject[(int)GridSize_V2.x, (int)GridSize_V2.y];


		for( int i = 0; i < GridSize_V2.x; i++ )
			for( int j = 0; j < GridSize_V2.y; j++ )
			{
				Vector3 tmp_V3 = transform.position + 11 * i * Vector3.right + 11 * j * Vector3.forward;
				//Vector3 tmp_V3 = Vector3.zero + 11 * i * Vector3.right + 11 * j * Vector3.forward;
				GameObject tmp_GO = Instantiate( Frame_GO, tmp_V3, Frame_GO.transform.rotation ) as GameObject;
				tmp_GO.transform.parent = transform;
				tmp_GO.GetComponent< SpaceField >().MapPosition_V2 = new Vector2( i, j );
				Globals.Grid__[i,j] = tmp_GO;

			}
	}

	public void Errase()
	{

		SpaceField[] objs = GameObject.FindObjectsOfType<SpaceField>();
		foreach( SpaceField sf in objs )
			GameObject.DestroyImmediate( sf.gameObject );
	}

	public void Clear()
	{
		for( int i = 0; i < GridSize_V2.x; i++ )
			for( int j = 0; j < GridSize_V2.y; j++ )
			{
				Globals.Grid__[i,j].GetComponent< SpaceField >().Clear();
			}	
	}

	private int Dir_I = 1;

	public void SelectThree( int px, int py )
	{
//		print( px.ToString() + " " + py.ToString() );

		int i; int j;


		i = Mathf.Clamp( px, 0, 7 );
		j = Mathf.Clamp( py + 1 * Dir_I, 0, 7 );
		Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();


		i = Mathf.Clamp( px - 1 * Dir_I, 0, 7 );
		j = Mathf.Clamp( py + 1 * Dir_I, 0, 7 );
		Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();


		i = Mathf.Clamp( px + 1 * Dir_I, 0, 7 );
		j = Mathf.Clamp( py + 1 * Dir_I, 0, 7 );
		Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();

	}

	public void SelectCross( int px, int py )
	{

		int i;
		int j;

		i = Mathf.Clamp(px, 0, 7 );
		j = Mathf.Clamp(py + 1 * Dir_I, 0, 7 );
		if( Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight() )
		{
			i = Mathf.Clamp(px, 0, 7 );
			j = Mathf.Clamp(py + 2 * Dir_I, 0, 7 );
			Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();
		}

		i = Mathf.Clamp(px + 1 * Dir_I, 0, 7 );
		j = Mathf.Clamp(py + 1 * Dir_I, 0, 7 );
		Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();

		i = Mathf.Clamp(px + 1 * Dir_I, 0, 7 );
		j = Mathf.Clamp(py, 0, 7 );
		if( Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight() )
		{
			i = Mathf.Clamp(px + 2 * Dir_I, 0, 7 );
			j = Mathf.Clamp(py, 0, 7 );
			Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();		
		}

		i = Mathf.Clamp(px + 1 * Dir_I, 0, 7 );
		j = Mathf.Clamp(py - 1 * Dir_I, 0, 7 );
		Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();

		i = Mathf.Clamp(px, 0, 7 );
		j = Mathf.Clamp(py - 1 * Dir_I, 0, 7 );
		if( Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight() )
		{
			i = Mathf.Clamp(px, 0, 7 );
			j = Mathf.Clamp(py - 2 * Dir_I, 0, 7 );
			Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();		
		}

		i = Mathf.Clamp(px - 1 * Dir_I, 0, 7 );
		j = Mathf.Clamp(py - 1 * Dir_I, 0, 7 );
		Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();

		i = Mathf.Clamp(px - 1 * Dir_I, 0, 7 );
		j = Mathf.Clamp(py, 0, 7 );
		if( Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight() )
		{
			i = Mathf.Clamp(px - 2 * Dir_I, 0, 7 );
			j = Mathf.Clamp(py, 0, 7 );
			Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();			
		}

		i = Mathf.Clamp(px - 1 * Dir_I, 0, 7 );
		j = Mathf.Clamp(py + 1 * Dir_I, 0, 7 );
		Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();


	}


	public void SelectCircle( int px, int py )
	{
		int i; int j;

		i = Mathf.Clamp( px, 0, 7 );
		j = Mathf.Clamp( py + 1 * Dir_I, 0, 7 );
		Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();

		i = Mathf.Clamp( px + 1 * Dir_I, 0, 7 );
		j = Mathf.Clamp( py + 1 * Dir_I, 0, 7 );
		Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();

		i = Mathf.Clamp( px + 1 * Dir_I, 0, 7 );
		j = Mathf.Clamp( py, 0, 7 );
		Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();

		i = Mathf.Clamp( px + 1 * Dir_I, 0, 7 );
		j = Mathf.Clamp( py - 1 * Dir_I, 0, 7 );
		Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();

		i = Mathf.Clamp( px, 0, 7 );
		j = Mathf.Clamp( py - 1 * Dir_I, 0, 7 );
		Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();


		i = Mathf.Clamp( px - 1 * Dir_I, 0, 7 );
		j = Mathf.Clamp( py - 1 * Dir_I, 0, 7 );
		Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();

		i = Mathf.Clamp( px - 1 * Dir_I, 0, 7 );
		j = Mathf.Clamp( py, 0, 7 );
		Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();

		i = Mathf.Clamp( px - 1 * Dir_I, 0, 7 );
		j = Mathf.Clamp( py + 1 * Dir_I, 0, 7 );
		Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();
	}

	public void SelectSniper( int px, int py )
	{
		int i; int j;

		i = Mathf.Clamp( px, 0, 7 );
		j = Mathf.Clamp( py + 1 * Dir_I, 0, 7 );
		if( Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight() )
		{
			i = Mathf.Clamp( px, 0, 7 );
			j = Mathf.Clamp( py + 2 * Dir_I, 0, 7 );
			if( Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight() )
			{
				i = Mathf.Clamp( px, -1, 8 );
				j = Mathf.Clamp( py + 3 * Dir_I, -1, 8 );
				if( i != 8 && j != 8 && i != -1 && j != -1 )
					Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight() ;
			}
		}

		i = Mathf.Clamp( px + 1 * Dir_I, -1, 8 );
		j = Mathf.Clamp( py + 1 * Dir_I, -1, 8 );
		if( i != 8 && j != 8 && i != -1 && j != -1 )
		if( Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight() )
		{
			i = Mathf.Clamp( px + 2 * Dir_I, -1, 8 );
			j = Mathf.Clamp( py + 2 * Dir_I, -1, 8 );
			if( i != 8 && j != 8 && i != -1 && j != -1 )
			if( Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight() )
			{
				i = Mathf.Clamp( px + 3 * Dir_I, -1, 8 );
				j = Mathf.Clamp( py + 3 * Dir_I, -1, 8 );
				if( i != 8 && j != 8 && i != -1 && j != -1 )				
					Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();
			}
		}

		i = Mathf.Clamp( px + 1 * Dir_I, 0, 7 );
		j = Mathf.Clamp( py, 0, 7 );
		//Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();

		i = Mathf.Clamp( px + 1 * Dir_I, -1, 8 );
		j = Mathf.Clamp( py - 1 * Dir_I, -1, 8 );
		if( i != 8 && j != 8 && i != -1 && j != -1 )
			Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();

		i = Mathf.Clamp( px, -1, 8 );
		j = Mathf.Clamp( py - 1 * Dir_I, -1, 8 );
		if( i != 8 && j != 8 && i != -1 && j != -1 )
			Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();

		i = Mathf.Clamp( px - 1 * Dir_I, -1, 8 );
		j = Mathf.Clamp( py - 1 * Dir_I, -1, 8 );
		if( i != 8 && j != 8 && i != -1 && j != -1 )
			Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();

		i = Mathf.Clamp( px - 1 * Dir_I, 0, 7 );
		j = Mathf.Clamp( py, 0, 7 );
		//Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();

		i = Mathf.Clamp( px - 1 * Dir_I, -1, 8 );
		j = Mathf.Clamp( py + 1 * Dir_I, -1, 8 );
		if( i != 8 && j != 8 && i != -1 && j != -1 )		
		if( Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight() )
		{
			i = Mathf.Clamp( px - 2 * Dir_I, -1, 8 );
			j = Mathf.Clamp( py + 2 * Dir_I, -1, 8 );
			if( i != 8 && j != 8 && i != -1 && j != -1 )
			if( Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight() )
			{
				i = Mathf.Clamp( px - 3 * Dir_I, -1, 8 );
				j = Mathf.Clamp( py + 3 * Dir_I, -1, 8 );
				if( i != 8 && j != 8 && i != -1 && j != -1 )
					Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();
			}
		}


	}

	public void SelectStealt( int px, int py )
	{
		int i; int j;

		i = Mathf.Clamp( px, 0, 7 );
		j = Mathf.Clamp( py + 1 * Dir_I, 0, 7 );
		Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();

		i = Mathf.Clamp( px, 0, 7 );
		j = Mathf.Clamp( py + 2 * Dir_I, 0, 7 );
		Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();

		i = Mathf.Clamp( px + 1 * Dir_I, 0, 7 );
		j = Mathf.Clamp( py + 1 * Dir_I, 0, 7 );
		Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();
		i = Mathf.Clamp( px + 1 * Dir_I, 0, 7 );
		j = Mathf.Clamp( py + 2 * Dir_I, 0, 7 );
		Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();		

		i = Mathf.Clamp( px + 1 * Dir_I, 0, 7 );
		j = Mathf.Clamp( py, 0, 7 );
		Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();

		i = Mathf.Clamp( px + 1 * Dir_I, 0, 7 );
		j = Mathf.Clamp( py - 1 * Dir_I, 0, 7 );
		Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();
		i = Mathf.Clamp( px, 0, 7 );
		j = Mathf.Clamp( py - 2 * Dir_I, 0, 7 );
		Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();

		i = Mathf.Clamp( px, 0, 7 );
		j = Mathf.Clamp( py - 1 * Dir_I, 0, 7 );
		Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();


		i = Mathf.Clamp( px - 1 * Dir_I, 0, 7 );
		j = Mathf.Clamp( py - 1 * Dir_I, 0, 7 );
		Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();

		i = Mathf.Clamp( px - 1 * Dir_I, 0, 7 );
		j = Mathf.Clamp( py, 0, 7 );
		Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();

		i = Mathf.Clamp( px - 1 * Dir_I, 0, 7 );
		j = Mathf.Clamp( py + 1 * Dir_I, 0, 7 );
		Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();
		i = Mathf.Clamp( px - 1 * Dir_I, 0, 7 );
		j = Mathf.Clamp( py + 2 * Dir_I, 0, 7 );
		Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();

	}

	public void SelectGuardian( int px, int py )
	{
		int i; int j;

		i = Mathf.Clamp( px, 0, 7 );
		j = Mathf.Clamp( py + 1 * Dir_I, 0, 7 );
		if( Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight() )
		{
			i = Mathf.Clamp( px, -1, 8 );
			j = Mathf.Clamp( py + 2 * Dir_I, -1, 8 );
			if( i != 8 && j != 8 && i != -1 && j != -1 )
			if( Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight() )
			{
				i = Mathf.Clamp( px, -1, 8 );
				j = Mathf.Clamp( py + 3 * Dir_I, -1, 8 );
				if( i != 8 && j != 8 && i != -1 && j != -1 )
					Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight() ;
			}
		}

		i = Mathf.Clamp( px + 1 * Dir_I, 0, 7 );
		j = Mathf.Clamp( py + 1 * Dir_I, 0, 7 );
		if( Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight() )
		{
			i = Mathf.Clamp( px + 2 * Dir_I, -1, 8 );
			j = Mathf.Clamp( py + 2 * Dir_I, -1, 8 );
			if( i != 8 && j != 8 && i != -1 && j != -1 )
			if( Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight() )
			{
				i = Mathf.Clamp( px + 3 * Dir_I, -1, 8 );
				j = Mathf.Clamp( py + 3 * Dir_I, -1, 8 );
				if( i != 8 && j != 8 && i != -1 && j != -1 )				
					Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();
			}
		}

		i = Mathf.Clamp( px + 1 * Dir_I, 0, 7 );
		j = Mathf.Clamp( py, 0, 7 );
		if( Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight() )
		{
			i = Mathf.Clamp( px + 2 * Dir_I, 0, 7 );
			j = Mathf.Clamp( py, 0, 7 );
			Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();			
		}

		i = Mathf.Clamp( px + 1 * Dir_I, 0, 7 );
		j = Mathf.Clamp( py - 1 * Dir_I, 0, 7 );
		if( Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight() )
		{
			i = Mathf.Clamp( px + 2 * Dir_I, 0, 7 );
			j = Mathf.Clamp( py - 2 * Dir_I, 0, 7 );
			Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();		
		}

		i = Mathf.Clamp( px, 0, 7 );
		j = Mathf.Clamp( py - 1 * Dir_I, 0, 7 );
		if( Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight() )
		{
			i = Mathf.Clamp( px, -1, 8 );
			j = Mathf.Clamp( py - 2 * Dir_I, -1, 8 );
			if( i != 8 && j != 8 && i != -1 && j != -1 )
				Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();		
		}

		i = Mathf.Clamp( px - 1 * Dir_I, 0, 7 );
		j = Mathf.Clamp( py - 1 * Dir_I, 0, 7 );
		if( Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight() )
		{
			i = Mathf.Clamp( px - 2 * Dir_I, -1, 8 );
			j = Mathf.Clamp( py - 2 * Dir_I, -1, 8 );
			if( i != 8 && j != 8 && i != -1 && j != -1 )
				Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();			
		}

		i = Mathf.Clamp( px - 1 * Dir_I, 0, 7 );
		j = Mathf.Clamp( py, 0, 7 );
		if( Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight() )
		{
			i = Mathf.Clamp( px - 2 * Dir_I, 0, 7 );
			j = Mathf.Clamp( py, 0, 7 );
			Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();		
		}

		i = Mathf.Clamp( px - 1 * Dir_I, 0, 7 );
		j = Mathf.Clamp( py + 1 * Dir_I, 0, 7 );
		if( Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight() )
		{
			i = Mathf.Clamp( px - 2 * Dir_I, -1, 8 );
			j = Mathf.Clamp( py + 2 * Dir_I, -1, 8 );
			if( i != 8 && j != 8 && i != -1 && j != -1 )
			if( Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight() )
			{
				i = Mathf.Clamp( px - 3 * Dir_I, -1, 8 );
				j = Mathf.Clamp( py + 3 * Dir_I, -1, 8 );
				if( i != 8 && j != 8 && i != -1 && j != -1 )
					Globals.Grid__[ i,j ].GetComponent< SpaceField >().HighLight();
			}
		}
	}

//<ADDCASE_SELECTFNC>

	public void ShowFlyFields( GameObject go )
	{
		
		SpaceField sf = go.transform.parent.GetComponent<SpaceField>();
		Dir_I = go.layer == 8 ? 1: -1; 

		// print( go.name );

		switch( go.name )
		{

			case "Watcher_1":
			case "Watcher_2":
				SelectThree( sf.PositionX, sf.PositionY );
			break;

			case "Defender_1":
				SelectCross(sf.PositionX, sf.PositionY );
			break;

			case "Messenger_1":
				SelectCircle( sf.PositionX, sf.PositionY );
			break;

			case "Stealth_1":
				SelectStealt( sf.PositionX, sf.PositionY );
			break;

			case "Sniper_1":
				SelectSniper( sf.PositionX, sf.PositionY );
			break;

			case "Guardian_1":
			case "Guardian_2":
				SelectGuardian( sf.PositionX, sf.PositionY );
			break;

//<ADDCASE_SHIPNAME_SHOWFLYFIELDS>

			default:
				SelectThree(go.transform.parent.GetComponent<SpaceField>().PositionX, go.transform.parent.GetComponent<SpaceField>().PositionY);
			break;
		}

		/*
		SpaceField[] objs = GameObject.FindObjectsOfType<SpaceField>();
		foreach( SpaceField sfx in objs )
		{
			if( go.transform == sfx.transform.parent )
			switch( sfx.transform.parent.gameObject.name )
			{
				case "Watcher_1":
					SelectThree(sfx.PositionX, sfx.PositionY);
				break;
				default:
					SelectThree(sfx.PositionX, sfx.PositionY);
				break;
			}
		}
*/
	}

	public void PlaceFleet()
	{
		Vector2 pos = Vector2.zero;
		int i = -1;

		Player player = GameObject.Find("PlayerOne").GetComponent<Player>();

		foreach( Ship ship in player.Fleet )
		{
			i++;
			pos.x = i % 8;
			pos.y = 1 - i / 8;
			PlaceShip( ship.gameObject, pos, true );
		}

		pos = Vector2.zero;
		player = GameObject.Find("PlayerTwo").GetComponent<Player>();
		i = 64;

		foreach( Ship ship in player.Fleet )
		{
			i--;
			pos.x = i % 8;
			pos.y = i / 8;
			PlaceShip( ship.gameObject, pos, false );
		}

	}

	public void PlaceShip( GameObject ship, Vector2 pos, bool self )
	{
		
		GameObject tmpgo = Instantiate( ship, Globals.Grid__[(int)pos.x,(int)pos.y].transform.position, Quaternion.identity ) as GameObject;
		tmpgo.name = tmpgo.name.Replace("(Clone)", "");
		tmpgo.transform.position += new Vector3( 0, 0.7f, 0 );

		if( !self )	// Player2
		{
			tmpgo.layer = 9;
			//tmpgo.transform.rotation.Set( tmpgo.transform.rotation.x, tmpgo.transform.rotation.y + 180, tmpgo.transform.rotation.z, tmpgo.transform.rotation.w ); // new Vector3( 0, 180, 0 ); 
			tmpgo.transform.Rotate( tmpgo.transform.up, 180 );
		}

		tmpgo.transform.parent = Globals.Grid__[(int)pos.x,(int)pos.y].transform;
		//Grid__[pos.x, pos.y].GetComponent<SpaceField>().Ship_GO.GetComponent( ChangeLayer ).Make( layer );	

		Globals.Grid__[(int)pos.x, (int)pos.y].GetComponent<SpaceField>().Ship_GO = tmpgo;
		Globals.Grid__[(int)pos.x, (int)pos.y].GetComponent<SpaceField>().ColorSign();


	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
