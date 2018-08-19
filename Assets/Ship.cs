using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	Vector2 MapPosition_V2;

    public bool IsPlayerShip;
    public AnimationCurve Accel_AC;
    public AnimationCurve AccelBack_AC;
    public AnimationCurve AccelRight_AC;

    public float EngineForce_F;
    private float AccelTimer;
    private float AccelBackTimer;
    private float AccelRightTimer;
    private float AccelLeftTimer;

	public GameObject MotorEmission_GO;
    public ParticleSystem[] EngineEmiter_;

    public Transform TargetPoint_T;


    private Transform GR_T;                         // Graphic represent

    public bool MouseAction;


    private float AroundTimer;
    private float EngineEmiterLT_F;

    private void Awake()
    {
        GR_T = transform.Find("Graphic");
        EngineEmiter_ = MotorEmission_GO.GetComponentsInChildren<ParticleSystem>();
    }

    // Use this for initialization
    void Start () {
		//MotorEmission_GO.SetActive( false );
	}
	
	// Update is called once per frame
	void Update () {

        if (!IsPlayerShip) return;

        UpdateMovement();

        foreach (ParticleSystem pe in EngineEmiter_ )
        {
            pe.startLifetime = 0.1f + EngineEmiterLT_F;
        }
    }


    public void UpdateMovement()
    {

        EngineEmiterLT_F = -0.05f;
        // Camera target point on enemy ship
        float distance = (TargetPoint_T.position - transform.position).magnitude;

        //Debug.Log(distance);
        TargetPoint_T.localPosition = new Vector3(0, -10f * distance / 100f, 0);
        Camera.main.transform.LookAt(TargetPoint_T);
        float locYCam = Camera.main.transform.localEulerAngles.y;
        Camera.main.transform.localPosition = new Vector3(0f, 5f, -5f);

        // Forward
        if (Input.GetKeyDown(KeyCode.W))
        {
            AccelTimer = 0f;
        }

        float intertion = 1.0f;
        if( Input.GetKey(KeyCode.LeftShift) )
        {
            intertion = 1.6f;
        }

        if (Input.GetKey(KeyCode.W))
        {
            EngineEmiterLT_F = 0.03f;
            AccelTimer += Time.deltaTime;

            if (AccelTimer < Accel_AC.length)
            {
                transform.position += transform.forward * Accel_AC.Evaluate(AccelTimer) * EngineForce_F * Time.deltaTime * intertion;
                float diff = Accel_AC.Evaluate(AccelTimer + 0.05f) - Accel_AC.Evaluate(AccelTimer);                         // Zrychleni delta za jednotku casu 0.05f
                Camera.main.transform.localPosition = new Vector3(0f, 5f, -5f - diff);
                //Debug.Log(diff);
            }
            else
            {
                transform.position += transform.forward * EngineForce_F * Time.deltaTime * intertion;
            }

            if( intertion > 1 )
            {
                transform.position += GR_T.up * Accel_AC.Evaluate(AccelTimer) * EngineForce_F * Time.deltaTime * intertion;


               // Debug.Log(distance.ToString());
               /*
                if (distance < 50 )
                {
                    Debug.Log("Add");
                    transform.position -= 2.0f * transform.forward * Accel_AC.Evaluate(AccelTimer) * EngineForce_F * Time.deltaTime * intertion;
                    transform.position += -TargetPoint_T.forward * EngineForce_F * Time.deltaTime * intertion;
                }
                */
            }

        }
        else
        {
            AccelTimer = Mathf.Clamp(AccelTimer, 0, Accel_AC.length);
            AccelTimer -= 3f * Time.deltaTime;

            if (AccelTimer > 0)
            {
                transform.position += transform.forward * Accel_AC.Evaluate(AccelTimer) * EngineForce_F * Time.deltaTime * intertion;


                float diff = Accel_AC.Evaluate(AccelTimer) - Accel_AC.Evaluate(AccelTimer - 0.05f) ;
                Camera.main.transform.localPosition = new Vector3(0f, 5f, -5f + diff);
                //Debug.Log( diff );
            }


        }

        // Backward
        if (Input.GetKeyDown(KeyCode.S))
        {
            AccelBackTimer = 0f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            EngineEmiterLT_F = 0.03f;
            AccelBackTimer += Time.deltaTime;

            if (AccelBackTimer < AccelBack_AC.length)
            {
                transform.position -= transform.forward * AccelBack_AC.Evaluate(AccelBackTimer) * EngineForce_F * Time.deltaTime * 0.75f * intertion;
            }
            else
            {
                transform.position -= transform.forward * EngineForce_F * Time.deltaTime * 0.75f * intertion;
            }
        }
        else
        {
            AccelBackTimer = Mathf.Clamp(AccelBackTimer, 0, AccelBack_AC.length);
            AccelBackTimer -= 3f * Time.deltaTime;

            if (AccelBackTimer > 0)
            {
                transform.position -= transform.forward * AccelBack_AC.Evaluate(AccelBackTimer) * EngineForce_F * Time.deltaTime * 0.75f * intertion;
            }

        }

        // ---------------------------------------------------

        // Left
        if (Input.GetKeyDown(KeyCode.A))
        {
            AccelLeftTimer = 0f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            EngineEmiterLT_F = 0.03f;
            AccelLeftTimer += Time.deltaTime;

            if (AccelLeftTimer < AccelRight_AC.length)
            {
                transform.position -= transform.right * AccelRight_AC.Evaluate(AccelLeftTimer) * EngineForce_F * Time.deltaTime * 0.85f * intertion;
                //Camera.main.transform.localPosition += transform.right * AccelRight_AC.Evaluate(AccelLeftTimer);
                //GR_T.eulerAngles = new Vector3(GR_T.eulerAngles.x, GR_T.eulerAngles.y, Mathf.LerpAngle(GR_T.eulerAngles.z, 5f * AccelRight_AC.Evaluate(AccelLeftTimer), 0.2f ) );
            }
            else
            {
                transform.position -= transform.right * EngineForce_F * Time.deltaTime * 0.85f * intertion;
                //Vector3.Lerp(new Vector3(0f, 5f, -5f), Camera.main.transform.localPosition, Time.deltaTime);
            }
        }
        else
        {
            AccelLeftTimer = Mathf.Clamp(AccelLeftTimer, 0, AccelRight_AC.length);
            AccelLeftTimer -= 3f * Time.deltaTime;

            if (AccelLeftTimer > -0.1)
            {
                transform.position -= transform.right * AccelRight_AC.Evaluate(AccelLeftTimer) * EngineForce_F * Time.deltaTime * 0.85f * intertion;
                //Camera.main.transform.localPosition += transform.right * AccelRight_AC.Evaluate(AccelLeftTimer);
                //GR_T.eulerAngles = new Vector3(GR_T.eulerAngles.x, GR_T.eulerAngles.y, Mathf.LerpAngle(GR_T.eulerAngles.z, 5f * AccelRight_AC.Evaluate(AccelLeftTimer), 0.5f) );
            }

        }

        // Right
        if (Input.GetKeyDown(KeyCode.D))
        {
            AccelRightTimer = 0f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            EngineEmiterLT_F = 0.03f;
            AccelRightTimer += Time.deltaTime;

            if (AccelRightTimer < AccelRight_AC.length)
            {
                transform.position += transform.right * AccelRight_AC.Evaluate(AccelRightTimer) * EngineForce_F * Time.deltaTime * 0.85f * intertion;
                
                //GR_T.eulerAngles = new Vector3(GR_T.eulerAngles.x, GR_T.eulerAngles.y, Mathf.LerpAngle(GR_T.eulerAngles.z, -5f * AccelRight_AC.Evaluate(AccelRightTimer), 0.2f) );
            }
            else
            {
                transform.position += transform.right * EngineForce_F * Time.deltaTime * 0.85f * intertion;
            }

            //Camera.main.transform.localPosition -= transform.right * AccelRight_AC.Evaluate(AccelRightTimer) / 4f;

        }
        else
        {
            AccelRightTimer = Mathf.Clamp(AccelRightTimer, 0, AccelRight_AC.length);
            AccelRightTimer -= 3f * Time.deltaTime;

            if (AccelRightTimer > -0.1)
            {
                transform.position += transform.right * AccelRight_AC.Evaluate(AccelRightTimer) * EngineForce_F * Time.deltaTime * 0.85f * intertion;
                //Camera.main.transform.localPosition -= transform.right * AccelRight_AC.Evaluate(AccelRightTimer) / 4f;
                //GR_T.eulerAngles = new Vector3(GR_T.eulerAngles.x, GR_T.eulerAngles.y, Mathf.LerpAngle(GR_T.eulerAngles.z, -5f * AccelRight_AC.Evaluate(AccelRightTimer), 0.5f) );
            }

        }


        // ---------------------------------------------------
        // Camera and look pint movement and ship directions
        // ---------------------------------------------------

        if (locYCam > 180)
        {
            locYCam = locYCam - 360;
        }

        Camera.main.transform.localPosition += -Camera.main.transform.forward * Mathf.Abs( locYCam ) / 20f;
        Camera.main.transform.localEulerAngles = new Vector3(Camera.main.transform.localEulerAngles.x, locYCam / 2f, Camera.main.transform.localEulerAngles.z);

        transform.LookAt( Vector3.Lerp(transform.position + transform.forward, TargetPoint_T.parent.transform.position, Time.deltaTime / 150f * Mathf.Abs( locYCam )) );

        float zRot = GR_T.eulerAngles.z;

        if (intertion > 1)
        {
            float ang = Vector3.AngleBetween((GR_T.position - TargetPoint_T.parent.position), GR_T.forward);
            //Debug.Log(ang);
            if (ang > Mathf.PI / 90f && ang < Mathf.PI )
            {
                //Vector3 uptm = GR_T.up;
                GR_T.LookAt(Vector3.Lerp(GR_T.position + GR_T.forward, TargetPoint_T.parent.transform.position, Time.deltaTime * 20f));

                //if( ( uptm.normalized - GR_T.up.normalized).magnitude > 1.0 ) GR_T.up = uptm;
                AroundTimer = 1.2f;
            }
        }
        /*
        if( AroundTimer > 0)
        {
            AroundTimer -= Time.deltaTime;
            float ang = Vector3.AngleBetween((GR_T.position - TargetPoint_T.parent.position), GR_T.forward);

            if (ang > Mathf.PI / 90f && ang < Mathf.PI)
            {
               
                GR_T.LookAt(Vector3.Lerp(GR_T.position + GR_T.forward, TargetPoint_T.parent.transform.position, Time.deltaTime * 20f));
            }
        }
        */

        // Naklapeni
        GR_T.transform.eulerAngles = new Vector3(GR_T.transform.eulerAngles.x, GR_T.eulerAngles.y, zRot);

        float divLR = AccelRight_AC.Evaluate(AccelRightTimer) - AccelRight_AC.Evaluate(AccelLeftTimer);
        GR_T.eulerAngles = new Vector3(GR_T.eulerAngles.x, GR_T.eulerAngles.y, Mathf.LerpAngle(GR_T.eulerAngles.z, -2.5f * divLR * intertion, 0.2f));


        // ovladani mysi
        if (MouseAction)
        {
            if (Input.mousePosition.x > Screen.width >> 1)
            {
                float add = 20f * Time.deltaTime * (Mathf.Abs((float)Input.mousePosition.x / (float)Screen.width) - 0.5f);
                GR_T.localEulerAngles += new Vector3( 0, Mathf.Sqrt(add), 0 );
 //               Debug.Log( GR_T.localEulerAngles.ToString() );
            }
            else
            {
                float add = 20f * Time.deltaTime * (Mathf.Abs(((float)Screen.width - (float)Input.mousePosition.x) / (float)Screen.width) - 0.5f);
                GR_T.localEulerAngles -= new Vector3( 0, Mathf.Sqrt(add), 0 );
            }
            /*
            if(Input.mousePosition.y > Screen.height >> 1 )
            {
                float add = 20f * Time.deltaTime * (Mathf.Abs((float)Input.mousePosition.y / (float)Screen.height) - 0.5f);
                GR_T.localEulerAngles += new Vector3( add, 0, 0 );
            }
            else
            {
                float add = 20f * Time.deltaTime * ((((float)Screen.height - (float)Input.mousePosition.y) / (float)Screen.height) - 0.5f);
                Debug.Log(add);
                GR_T.localEulerAngles -= new Vector3(add, 0, 0);
            }*/
        }

    }
}
