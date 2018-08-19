using UnityEngine;
using System.Collections;
#region Delegates
#endregion
public class CameraController : MonoBehaviour {

	#region Private References      
    [SerializeField, Range(0.0f, 1.0f)]
    private float _lerpRate;
    private float _xRotation;
    private float _yYRotation;

	[SerializeField, Range(20f, 350f)]
	public float XRotMin_F;
	[SerializeField, Range(60f, 360f)]
	public float XRotMax_F;

	#endregion

	#region Private Methods
    private void Rotate(float xMovement, float yMovement)
    {
        _xRotation += xMovement;
        _yYRotation += yMovement;
    }
	#endregion

	#region Unity CallBacks
	
	void Start ()
	{
        InputManager.MouseMoved += Rotate;
		// time = 1.2
	}


	private float time;

	void Update ()
	{
		/*
		if( Input.GetMouseButtonDown( 0 ) )
		{

*/
			

	    _xRotation = Mathf.Lerp(_xRotation, 0, _lerpRate );
			
		Vector3 vect = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
		vect += new Vector3 (-Mathf.Lerp (_yYRotation, 0, _lerpRate), _xRotation, 0);



		if ( vect.x > XRotMin_F && vect.x < XRotMax_F )
				_yYRotation = Mathf.Lerp( -_yYRotation, 0, 0.99f );
			else
				_yYRotation = Mathf.Lerp(_yYRotation, 0, _lerpRate);


       		transform.eulerAngles += new Vector3(-_yYRotation, _xRotation, 0);

		if (transform.eulerAngles.x > XRotMin_F) {
			if (transform.eulerAngles.x < XRotMax_F) {
				if (transform.eulerAngles.x > ( XRotMax_F - XRotMin_F ) / 2 )
					transform.eulerAngles = new Vector3 (XRotMax_F, transform.eulerAngles.y, transform.eulerAngles.z);
				else
					transform.eulerAngles = new Vector3 (XRotMin_F, transform.eulerAngles.y, transform.eulerAngles.z);
			}
		}

//		print( transform.eulerAngles.ToString() );

//			if (transform.eulerAngles.x > 90)
//				transform.eulerAngles = new Vector3 (90, transform.eulerAngles.y, transform.eulerAngles.z);
		/*
		}
		else
		{
			
			time -= Time.deltaTime;

			if( time <= 0 )
			{
				_xRotation = Mathf.Lerp( Random.Range( -0.05f, 0.05f ) , 0, _lerpRate);
				_yYRotation = Mathf.Lerp( Random.Range( -0.08f, 0.05f ), 0, _lerpRate);
				transform.eulerAngles += new Vector3(-_yYRotation, _xRotation, 0);

				time = Random.Range( 1.5f, 2.25f );

			}
			else
			{
				transform.eulerAngles += new Vector3(-_yYRotation, _xRotation, 0);
			}
		}
		*/

    }

   void OnDestroy()
   {
       InputManager.MouseMoved += Rotate;    
   }
	#endregion
}
