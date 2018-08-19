using UnityEngine;
using System.Collections;

public class Orbitron : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Globals.Game_E = GameState.Turn_PlayerOne;
	}

	GameObject SelectShip()
	{
		
		Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
		Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

		RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 150, 1 << 8) || Physics.Raycast(ray, out hit, 150, 1 << 9))
        {
            //Debug.Log("Hitted" + hit.collider.gameObject.name );
            Transform tmp_T;

            tmp_T = hit.collider.gameObject.transform;

            if (Globals.SelectedShip_GO)
                Globals.SelectedShip_GO.GetComponent<Ship>().MotorEmission_GO.SetActive(false);

            Globals.SelectedShip_GO = tmp_T.parent.gameObject;
            Globals.PlayGrid_PG.Clear();
            Globals.SelectedShip_GO.transform.parent.GetComponent<SpaceField>().HighLight();
            Globals.PlayGrid_PG.ShowFlyFields(Globals.SelectedShip_GO);
            Globals.SelectedShip_GO.GetComponent<Ship>().MotorEmission_GO.SetActive(true);


            return Globals.SelectedShip_GO;

        }
        else
            return null;
			

	}

	public void ShipMove()
	{

		Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
		Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

		RaycastHit hit;

		if( Physics.Raycast( ray , out hit, 150, 1 << 10 ) )
		{
			Transform tmp_T;

			tmp_T = hit.collider.gameObject.transform;

//			print( hit.collider.gameObject.GetComponent<Renderer>().material.color );
//			print( hit.collider.gameObject.GetComponent<SpaceField>().Base_MAT.name.ToString() );

			if( hit.collider.gameObject.GetComponent<Renderer>().material.color == hit.collider.gameObject.GetComponent<SpaceField>().Base_MAT.color )
			{
				print("The same");
				return;
			}

			if( Globals.SelectedShip_GO )
				Globals.SelectedShip_GO.GetComponent< Ship >().MotorEmission_GO.SetActive( false );

			Globals.SelectedShip_GO.transform.position = hit.transform.position + new Vector3( 0, 0.7f ,0 );
			Globals.SelectedShip_GO.transform.parent = tmp_T;

			// Globals.SelectedShip_GO.transform.parent.GetComponent< SpaceField >().HighLight();
			Globals.SelectedShip_GO = null;
			Globals.PlayGrid_PG.Clear();

			//Globals.PlayGrid_PG.ShowFlyFields( Globals.SelectedShip_GO );
			//Globals.SelectedShip_GO.GetComponent< Ship >().MotorEmission_GO.SetActive( true );

		}

	}

	// Update is called once per frame
	void Update () 
	{
		switch( Globals.Game_E )
		{
			case GameState.Turn_PlayerOne:
			
				if( Input.GetMouseButtonDown( 0 ) )
				{
					Globals.SelectedShip_GO = SelectShip();
				}

				if( Globals.SelectedShip_GO )
				{
					if( Input.GetMouseButtonDown( 1 ) )
					{
						ShipMove();
					}
				}

			break;
		}
	}
}
