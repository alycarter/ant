using UnityEngine;
using System.Collections;

public class follow : MonoBehaviour {
	
	public Transform target;
	private Quaternion angle;
	private float distance = 6; // distance the camera is to target
	private float height = 3;
	// Use this for initialization
	void Start () {
       angle =target.rotation;
	}
 
    void Update () {
		if(Input.GetKey("left")){
			angle = Quaternion.AngleAxis(angle.eulerAngles.y+(Time.deltaTime*90),Vector3.up);
		}
		if(Input.GetKey("right")){
			angle = Quaternion.AngleAxis(angle.eulerAngles.y-(Time.deltaTime*90),Vector3.up);
		}
		float lowest= height-Time.deltaTime*3;
		RaycastHit hit;
		if(Physics.Raycast(transform.position, Vector3.down, out hit)){
			lowest=hit.point.y-target.position.y;
		}
		if(Input.GetKey("down")){
			height-=Time.deltaTime*3;
		}
		if(Input.GetKey("up")){
			height+=Time.deltaTime*3;
		}
		if(height<lowest){
			height=lowest;
		}
		if(Input.GetAxis("Mouse ScrollWheel")>0){
			distance--;
		}
		if(Input.GetAxis("Mouse ScrollWheel")<0){
			distance++;
		}
		
		if(height> 5){
			height =5;
		}
		if(distance>7){
			distance=7;
		}else{
			if(distance<1){
				distance=1;
			}
		}
		Vector3 position = new Vector3(distance,target.position.y+height,0);
		position = angle * position;
		position+=target.position;
		if(Physics.Raycast(target.position,(position-target.position),out hit)){
			if(Vector3.Distance(hit.point,target.position)<Vector3.Distance(position,target.position)){
				position = hit.point;
				distance=Vector2.Distance(new Vector2(target.position.x, target.position.z),new Vector2(position.x, position.z));
				height = position.y-target.position.y;
			}
		}
		position+=new Vector3(0,1,0);
		transform.position=position;
		transform.LookAt(target);
    }
 
}


