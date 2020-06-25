using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmo : MonoBehaviour {
    public Color color = Color.yellow;
    public float radius = 0.1f;
   
  
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnDrawGizmos()
    {
        //设置Gizmos的颜色
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position,radius);
    }
}
