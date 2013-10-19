using UnityEngine;
using System.Collections;

public class moveToLocation : MonoBehaviour {
	
	private bool mouseRelased = true;
	private Vector3 targetLocation;
	private GameObject surface = null;
	// Use this for initialization
	void Start () {
		RaycastHit hit;
		if(Physics.Raycast(transform.position,Vector3.down,out hit)){
			surface=hit.collider.gameObject;
			targetLocation=hit.point;
		}
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
		float distance = 5*Time.deltaTime;
		RaycastHit ground;
		Ray feeler;
		if(distance<Vector3.Distance(transform.position,targetLocation)){
			feeler = new Ray(transform.position, transform.forward);
			if(Physics.Raycast(feeler,out ground, distance)){
				distance-=Vector3.Distance(transform.position,ground.point);
				transform.position = ground.point;
				transform.up=ground.normal;
			}else{
				transform.position+=transform.forward*distance;
				if(Physics.Raycast(transform.position,(surface.transform.position-transform.position).normalized,out ground)){
					transform.position = ground.point;
					transform.up=ground.normal;
				}	
				distance=0;
			}
		}else{
			transform.position=targetLocation;
		}
	}
}
