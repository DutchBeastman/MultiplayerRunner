using UnityEngine;
using System.Collections;

public class TrapControl : MonoBehaviour {

	Vector3 shootingTrapPosition;
	GameObject CurrentTrap;
	bool    MovingTrap;
	public Camera cam;
	int activeTrap;
	// Use this for initialization
	void Start () {

		activeTrap = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F1)){
			activeTrap = 0;
		}
		if(Input.GetKeyDown(KeyCode.F2)){
			activeTrap = 1;
			Debug.Log ("trap2");
		}
		if(Input.GetKeyDown(KeyCode.F3)){
			activeTrap = 2;
			Debug.Log ("trap3");
		}
		Ray ray = cam.ScreenPointToRay (Input.mousePosition);

		if(Input.GetMouseButtonDown(0))
		{

			MovingTrap = true;



//			if(activeTrap == 0){
			GameObject ShootingTrap = Instantiate(Resources.Load("Prefabs/ShootingTrap"), new Vector3(Input.mousePosition.x,Input.mousePosition.y,Input.mousePosition.z), Quaternion.identity) as GameObject;
			CurrentTrap = ShootingTrap;

		        if(activeTrap == 1){
				GameObject SmackTrap = Instantiate(Resources.Load("Prefabs/ShootingTrap"), new Vector3(Input.mousePosition.x,Input.mousePosition.y,Input.mousePosition.z), Quaternion.identity) as GameObject;
				CurrentTrap = SmackTrap;
			}
			else if(activeTrap == 2){
				GameObject SpikeTrap = Instantiate(Resources.Load("Prefabs/ShootingTrap"), new Vector3(Input.mousePosition.x,Input.mousePosition.y,Input.mousePosition.z), Quaternion.identity) as GameObject;
				CurrentTrap = SpikeTrap;
			}
			if(MovingTrap == true){
				shootingTrapPosition = ray.origin;
				shootingTrapPosition.z = ray.origin.z + 9.7f;
				ShootingTrap.transform.localPosition = shootingTrapPosition;
				Debug.Log(ray.origin);
			}



		}
//		if(Input.GetMouseButtonUp(0)){
//			MovingTrap = false;
//
//		}	                 
//
//		if(MovingTrap == true){
//
//			shootingTrapPosition = ray.origin;
//			shootingTrapPosition.z = ray.origin.z + 9.7f;
//			CurrentTrap.transform.localPosition= shootingTrapPosition;
//			
//			Debug.Log(ray.origin);
//	}
	}
}
