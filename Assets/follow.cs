using UnityEngine;
using System.Collections;

public class follow : MonoBehaviour {
	
	public Transform target;
	public Quaternion angle;
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
		if(Input.GetKey("down")){
			height-=Time.deltaTime*4;
		}
		if(Input.GetKey("up")){
			height+=Time.deltaTime*4;
		}
		if(Input.GetAxis("Mouse ScrollWheel")>0){
			distance--;
		}
		if(Input.GetAxis("Mouse ScrollWheel")<0){
			distance++;
		}
		Vector3 position = new Vector3(distance,target.localPosition.y+height,0);
		position = angle * position;
		position+=target.position;
		RaycastHit hit;
		if(Physics.Raycast(new Vector3(position.x,1000,position.z),Vector3.down,out hit)){
			if(hit.point.y>position.y){
				height+=hit.point.y-position.y;
				position=hit.point;
			}
		}
		position+=new Vector3(0,1,0);
		transform.position=position;
		transform.LookAt(target);
    }
 
}


