using UnityEngine;
using System.Collections;

public class ResourceManagement : MonoBehaviour {
	//variable declaration
	public GUIText FValue, MValue;
	
	public struct Res { // this is a structure allowing us to have constant control over our resources.		
		public int Amount;
		public float DecayRate, gainRate, timer;
		//public GUIText textO;
		public void setValues(int x, float y, float z){
        	Amount = x;
			DecayRate = y;
			gainRate = z;
			timer = 0;
		}      	
	};			
		
	//funtion declaration
	int resUpdate(ref Res x){
		x.timer += Time.deltaTime;
		if((x.DecayRate - x.gainRate) > 0){
			if(x.timer >= (1/(x.DecayRate - x.gainRate))){
				x.timer = 0;
				x.Amount --;
			}
		}else if((x.DecayRate - x.gainRate) < 0){
			if(x.timer >= (1/(x.gainRate - x.DecayRate))){
				x.timer = 0;
				x.Amount ++;
			}
		}
		return x.Amount;
	}
	
	// creating the basic mats
	public Res Food;
    public Res Mats;
	
	// Use this for initialization
	void Start () {		
		Food.setValues(1000, 5, 3);
		Mats.setValues(100, 0, 1);
		print( 1 / Food.DecayRate);
	}
	
	// Update is called once per frame
	void Update () {
		FValue.text = resUpdate(ref Food).ToString();
		MValue.text = resUpdate(ref Mats).ToString();
	}		
}