using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

	public enum CLASS
	{
		A,
		B,
		C,			// Multi-frekv. shield, better compensation DMG, no fluctuation 
		D,			// BackUp energy transfering, more stronger and energy, 
		E,			// Basic shield - regen on battle, more Energy, fluctuations
		F			// Base shield - no regeneration on battle, minimal energy 
	}

	// Shield base modificators    - Capacitore, Regeneratore, Hardener, BackUp transfering
	// Shield advanced modificator - Compensator( fluctuation ), Booster, 
	// Shield powered modificator  - MultiFrekvent, Absorber, Reflection, Adaptation


	// Base params
	public int Energy_I;
	public int Regen_I;
	public float ShieldActivateTMR_F;		// Seconds shield activate on 65%

	// Fluctuation and strongs
	public int DMGMax_I;					// Max damage
	public int Strong_I;					// Max damage per second, after fluct

	public float FluctuationTMR_F;			// After stronger DMG than Strong_I, shield fluctuation
	public int FluctuationDummy_I;			// Percentable Dummy

	//





	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
