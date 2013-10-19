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
		if(distance<Vector3.Distance(transform.position,targetLocation)){
			RaycastHit ground;
			Ray feeler;
			feeler = new Ray(transform.position+(transform.up*0.25f), transform.forward);
			if(Physics.Raycast(feeler,out ground, distance)){
			//	distance-=Vector3.Distance(transform.position,ground.point);
				transform.up=ground.normal;
				transform.position = ground.point;
				surface=ground.collider.gameObject;
			}else{
				transform.position+=transform.forward*distance;
				feeler= new Ray(transform.position+(transform.up*0.25f),-transform.up);
				if(Physics.Raycast(feeler,out ground)){
					//	distance-=Vector3.Distance(transform.position,ground.point);
					transform.up=ground.normal;
					transform.position = ground.point;
					surface=ground.collider.gameObject;
				}	
			}
			Quaternion look = Quaternion.LookRotation((targetLocation-transform.position).normalized, transform.up);
			look =Quaternion.AngleAxis(look.eulerAngles.y,transform.up);
			transform.rotation =Quaternion.Euler(transform.rotation.eulerAngles.x+look.eulerAngles.x,look.eulerAngles.y,transform.eulerAngles.z+look.eulerAngles.z);
		}
		Debug.Log(surface);
	}
}
