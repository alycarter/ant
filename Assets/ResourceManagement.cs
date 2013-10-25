using UnityEngine;
using System.Collections;

public class ResourceManagement : MonoBehaviour {
	//variable declaration
	float timedialation = 0;
	public GUIText FValue;
	
	public struct Res { // this is a structure allowing us to have constant control over our resources.
		public int Amount;	
		
        public Res(int thing){
        	Amount = 1000;			
		}
	}
	// creating the basic mats
	Res Food = new Res();
    
	// Use this for initialization
	void Start () {
		FValue.text = "1000";
		Food.Amount = 1000;
		//FValue.text = "Hello";
	}
	
	// Update is called once per frame
	void Update () {
		
		timedialation += Time.deltaTime;
		if (timedialation >= 1)
		{
			timedialation = 0;
			Food.Amount --;
		}
		//Food.Amount ++;
		FValue.text = Food.Amount.ToString();
	}
}
