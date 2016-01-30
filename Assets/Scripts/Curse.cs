using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Curse : MonoBehaviour {
    public enum Type {Invisible, Confused};
    public Type type;
    public int timer;
    
    void Start () {
		timer = 0;
    }

    void setTimer (int _timer){
		timer = _timer;
    }

    int getTimer (){
		return timer;
    }

    void update (){
		timer--;
    }

}
