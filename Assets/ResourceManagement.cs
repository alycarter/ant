using UnityEngine;
using System.Collections;

public struct Res { // this is a structure allowing us to have constant control over our resources.
		public int Amount;
	
        public Res(int thing){
        	Amount = 1000;
		}
}
	

		

public class ResourceManagement : MonoBehaviour {
	//public transform foodbrick;
	// Use this for initialization
	void Start () {
		Res Food = new Res();
		Res BuildingMaterial = new Res();
		
	}
	
	// Update is called once per frame
	void Update () {
	//transform.position = 2;
		
	}
}
