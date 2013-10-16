using UnityEngine;
using System.Collections;

public class moveToLocation : MonoBehaviour {
	
	private bool mouseRelased = true;
	private Vector3 targetLocation;
	// Use this for initialization
	void Start () {
		targetLocation=transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			if(mouseRelased){
				Ray ray=  Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if(Physics.Raycast(ray, out hit)){
					targetLocation=hit.point;
				}
				mouseRelased=false;
			}
		}else{
			mouseRelased=true;
		}
		if(Vector3.Distance(targetLocation,transform.position)>0.5){
			transform.LookAt(targetLocation);
			transform.position= Vector3.MoveTowards(transform.position,targetLocation,Time.deltaTime*5);
		}
	}
}
