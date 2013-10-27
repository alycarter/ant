using UnityEngine;
using System.Collections;

public class moveToLocation : MonoBehaviour {
	
	private bool mouseRelased = true;
	private Vector3 targetLocation;
	private GameObject surface = null;
	public Transform marker;
	// Use this for initialization
	void Start () {
		RaycastHit hit;
		if(Physics.Raycast(transform.position,Vector3.down,out hit)){
			surface=hit.collider.gameObject;
			targetLocation=hit.point;
			
		}
	}
	//this is a test comment
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
		if(marker != null){
			marker.position = targetLocation;
			float change = 360*Time.deltaTime;
			marker.rotation *= Quaternion.Euler(new Vector3(change,change,change));
		}
		float distance = 5*Time.deltaTime;
		if(0.5<Vector3.Distance(transform.position,targetLocation)){
			RaycastHit ground;
			Ray feeler;
			feeler = new Ray(transform.position+(transform.up*0.25f), transform.forward);
			if(Physics.Raycast(feeler,out ground, distance)){
			//	distance-=Vector3.Distance(transform.position,ground.point);
				//transform.up=ground.normal;
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
				RaycastHit down;
				bool downHit= Physics.Raycast(feeler,out down);
				RaycastHit back;
				feeler= new Ray(transform.position+(transform.up*0.25f),-transform.up-transform.forward);
				bool backHit= Physics.Raycast(feeler,out back);
				if(backHit || downHit){
					//know one or both is hit but not what ones
					if(!downHit){
						//know down isnt hit
						//know back is hit
						ground=back;
					}else{
						//down is hit
						//dont know if back is
						if(backHit){
							// know both are hit
							if(Vector3.Distance(transform.position, down.point)<Vector3.Distance(transform.position, back.point)){
								// down is closer
								ground=down;
							}else{
								//back is closer
								ground=back;
							}
						}else{
							//know back isnt hit
							//know down is hit
							ground=down;
						}
					}
					transform.up=ground.normal;
					transform.position = ground.point;
					surface=ground.collider.gameObject;
				}
			}
			Quaternion look = Quaternion.LookRotation((targetLocation-transform.position).normalized, transform.up);
			look =Quaternion.AngleAxis(look.eulerAngles.y,transform.up);
			transform.rotation =Quaternion.Euler(transform.rotation.eulerAngles.x+look.eulerAngles.x,look.eulerAngles.y,transform.eulerAngles.z+look.eulerAngles.z);
		}
	}
}
