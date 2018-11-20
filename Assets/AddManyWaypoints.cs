using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AddManyWaypoints : MonoBehaviour {

	// Use this for initialization
	void Start () {
		CinemachineSmoothPath smoothPath = gameObject.GetComponent<CinemachineSmoothPath>();
		smoothPath.m_Waypoints = new CinemachineSmoothPath.Waypoint[124];
		for(int i = 0; i < 124; i++){
			Vector3 vec = NodesMap.nodesPosition[i];
			smoothPath.m_Waypoints[i] = new CinemachineSmoothPath.Waypoint();
			smoothPath.m_Waypoints[i].position = vec;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
