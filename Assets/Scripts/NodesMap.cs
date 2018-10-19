﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodesMap : MonoBehaviour {

    //(Valores de Nodos en el quinto Valor)
    //(0:Pelea,1:Tienda,2:Cofre,3:Hospital,4:Inn,5:Fabrica)
	public static int[,] nodesArray = new int[125, 5] {
		{1, 0, 0, 0, 0 },
		{2, 0, 0, 0, 0 },
		{3, 1, 0, 0, 0 },
		{4, 2, 0, 0, 0 },
		{5, 3, 0, 0, 0 },
		{10, 4, 6, 7, 0 },
		{0, 0, 0, 5, 2 },
		{8, 0, 5, 0, 1 },
		{9, 7, 0, 0, 0 },
		{0, 8, 0, 0, 2 },
		{0, 5, 11, 13, 0 },
		{12, 10, 0, 13, 4 },
		{17, 11, 0, 14, 0 },
		{14, 10, 11, 0, 0 },
		{0, 13, 12, 15, 0 },
		{19, 0, 14, 21, 0 },
		{18, 0, 0, 0, 2 },
		{18, 12, 0, 0, 0 },
		{19, 17, 0, 16, 0 },
		{0, 15, 18, 20, 0 },
		{0, 0, 19, 22, 0 },
		{22, 0, 15, 0, 0 },
		{23, 21, 20, 25, 0 },
		{0, 22, 24, 0, 3 },
		{0, 0, 0, 23, 0 },
		{0, 0, 22, 26, 5 },
		{27, 124, 25, 0, 0 },
		{29, 26, 28, 0, 0 },
		{0, 27, 0, 29, 2 },
		{0, 27, 28, 30, 0 },
		{0, 0, 29, 31, 0 },
		{0, 32, 30, 49, 0 },
		{31, 0, 33, 48, 0 },
		{0, 34, 0, 32, 0 },
		{33, 42, 35, 0, 0 },
		{124, 0, 36, 34, 0 },
		{0, 37, 0, 35, 0 },
		{36, 39, 38, 0, 0 },
		{37, 0, 0, 39, 2 },
		{37, 0, 38, 40, 0 },
		{0, 0, 39, 41, 4 },
		{42, 0, 40, 46, 0 },
		{34, 41, 0, 43, 0 },
		{44, 46, 42, 45, 0 },
		{0, 43, 0, 0, 5 },
		{47, 0, 43, 0, 2 },
		{43, 0, 41, 57, 0 },
		{48, 45, 0, 55, 0 },
		{0, 47, 32, 0, 0 },
		{0, 0, 31, 50, 0 },
		{51, 0, 49, 53, 0 },
		{0, 50, 0, 52, 1 },
		{0, 53, 51, 0, 0 },
		{52, 0, 50, 54, 0 },
		{53, 55, 0, 0, 2 },
		{54, 56, 47, 0, 0 },
		{55, 58, 57, 0, 0 },
		{56, 0, 46, 59, 0 },
		{56, 59, 0, 0, 0 },
		{58, 0, 57, 0, 3 },
		{0, 61, 0, 0, 0 },
		{60, 62, 0, 0, 0 },
		{0, 0, 61, 63, 0 },
		{0, 64, 62, 0, 0 },
		{63, 66, 0, 65, 0 },
		{64, 0, 66, 0, 3 },
		{64, 0, 67, 65, 0 },
		{70, 0, 68, 66, 2 },
		{69, 0, 0, 67, 0 },
		{0, 68, 0, 0, 1 },
		{72, 67, 73, 71, 0 },
		{0, 70, 72, 0, 2 },
		{0, 70, 74, 71, 0 },
		{74, 0, 76, 70, 0 },
		{0, 73, 75, 72, 0 },
		{0, 76, 77, 74, 4 },
		{75, 0, 78, 73, 0 },
		{0, 78, 79, 75, 0 },
		{77, 0, 83, 76, 2 },
		{0, 0, 80, 77, 0 },
		{0, 0, 81, 79, 0 },
		{80, 82, 0, 0, 5 },
		{0, 0, 81, 83, 0 },
		{0, 0, 82, 78, 0 },
		{0, 0, 85, 111, 0 },
		{86, 84, 0, 0, 4 },
		{87, 85, 0, 0, 0 },
		{112, 86, 88, 109, 0 },
		{113, 89, 93, 87, 0 },
		{88, 90, 92, 0, 0 },
		{89, 0, 91, 0, 0 },
		{92, 0, 0, 90, 1 },
		{93, 91, 0, 89, 3 },
		{116, 92, 94, 88, 0 },
		{0, 0, 0, 93, 2 },
		{96, 0, 0, 117, 0 },
		{97, 95, 0, 118, 2 },
		{98, 96, 0, 121, 0 },
		{94, 97, 0, 122, 0 },
		{0, 98, 0, 100, 0 },
		{0, 122, 99, 101, 0 },
		{0, 123, 100, 102, 0 },
		{0, 103, 123, 0, 0 },
		{102, 104, 123, 0, 0 },
		{103, 114, 120, 105, 0 },
		{0, 106, 104, 0, 2 },
		{105, 109, 112, 107, 0 },
		{0, 108, 106, 0, 0 },
		{107, 0, 109, 0, 2 },
		{106, 110, 87, 108, 0 },
		{109, 111, 0, 0, 0 },
		{110, 84, 0, 0, 0 },
		{115, 87, 113, 106, 0 },
		{114, 88, 116, 112, 0 },
		{104, 113, 119, 115, 0 },
		{0, 112, 114, 0, 5 },
		{119, 93, 117, 113, 5 },
		{118, 0, 95, 116, 0 },
		{121, 117, 96, 119, 0 },
		{120, 119, 118, 114, 0 },
		{123, 119, 121, 104, 0 },
		{122, 118, 97, 120, 0 },
		{100, 121, 98, 123, 5 },
		{101, 120, 122, 103, 0 },
		{26, 35, 0, 0, 0 },
	};
	public static Vector3[] nodesPosition = new Vector3[] {
        new Vector3(0, 0, 0),
        new Vector3(44.9f, -3.3f, 7.0f),
        new Vector3(45.3f, -3.9f, 9.6f),
        new Vector3(48.7f, -5.4f, 13.0f),
        new Vector3(51.5f, -6.3f, 13.4f),
        new Vector3(55.9f, -6.3f, 17.0f),
        new Vector3(60.7f, -6.5f, 12.0f),
        new Vector3(52.5f, -6.4f, 20.8f),
        new Vector3(55.2f, -6.4f, 26.8f),
        new Vector3(54.2f, -6.4f, 33.9f),
        new Vector3(58.7f, -6.3f, 19.7f),
        new Vector3(63.2f, -6.3f, 19.9f),
        new Vector3(66.5f, -6.3f, 23.6f),
        new Vector3(58.7f, -6.3f, 24.2f),
        new Vector3(62.0f, -6.3f, 27.8f),
        new Vector3(58.7f, -6.3f, 34.2f),
        new Vector3(63.2f, -6.3f, 31.0f),
        new Vector3(68.0f, -6.3f, 29.2f),
        new Vector3(66.0f, -6.3f, 34.2f),
        new Vector3(64.0f, -6.3f, 38.9f),
        new Vector3(59.9f, -6.3f, 42.3f),
        new Vector3(53.0f, -6.3f, 39.4f),
        new Vector3(55.9f, -6.3f, 45.5f),
        new Vector3(58.4f, -6.3f, 51.6f),
        new Vector3(63.9f, -6.3f, 44.9f),
        new Vector3(52.6f, -6.3f, 47.1f),
        new Vector3(47.3f, -6.3f, 49.8f),
        new Vector3(50.6f, -6.3f, 55.0f),
        new Vector3(58.6f, -6.3f, 59.2f),
        new Vector3(52.3f, -6.3f, 63.9f),
        new Vector3(44.5f, -6.3f, 65.5f),
        new Vector3(30.8f, -6.3f, 66.4f),
        new Vector3(26.6f, -6.3f, 62.0f),
        new Vector3(31.6f, -6.3f, 54.8f),
        new Vector3(26.7f, -6.3f, 45.8f),
        new Vector3(33.7f, -6.3f, 42.2f),
        new Vector3(35.8f, -6.3f, 32.7f),
        new Vector3(32.0f, -6.3f, 25.3f),
        new Vector3(36.3f, -6.3f, 15.1f),
        new Vector3(28.6f, -6.3f, 19.8f),
        new Vector3(20.1f, -6.3f, 25.1f),
        new Vector3(12.8f, -6.3f, 29.7f),
        new Vector3(19.2f, -6.3f, 36.9f),
        new Vector3(10.0f, -6.3f, 43.1f),
        new Vector3(13.1f, -5.8f, 46.7f),
        new Vector3(5.1f, -6.3f, 46.6f),
        new Vector3(3.2f, -6.3f, 35.0f),
        new Vector3(7.8f, -5.9f, 55.9f),
        new Vector3(16.6f, -6.3f, 63.0f),
        new Vector3(25.2f, -6.3f, 74.9f),
        new Vector3(14.6f, -6.3f, 73.7f),
        new Vector3(14.3f, -6.3f, 82.7f),
        new Vector3(2.4f, -5.9f, 83.9f),
        new Vector3(3.1f, -6.3f, 75.0f),
        new Vector3(-5.0f, -6.3f, 69.8f),
        new Vector3(-0.5f, -6.3f, 60.0f),
        new Vector3(-10.9f, -6.3f, 56.6f),
        new Vector3(-5.0f, -6.3f, 39.9f),
        new Vector3(-14.0f, -6.3f, 51.6f),
        new Vector3(-16.3f, -6.3f, 46.6f),
        new Vector3(-19.1f, -6.3f, 37.6f),
        new Vector3(-30.2f, -6.3f, 32.0f),
        new Vector3(-46.2f, -6.3f, 28.6f),
        new Vector3(-69.0f, -6.3f, 28.9f),
        new Vector3(-68.0f, -6.3f, 22.3f),
        new Vector3(-68.4f, -6.3f, 11.3f),
        new Vector3(-61.3f, -6.3f, 7.7f),
        new Vector3(-48.0f, -6.3f, -1.7f),
        new Vector3(-37.6f, -6.3f, -5.4f),
        new Vector3(-29.3f, -6.3f, -1.2f),
        new Vector3(-42.2f, -6.3f, 5.1f),
        new Vector3(-51.4f, -6.3f, 15.9f),
        new Vector3(-41.0f, -6.3f, 15.9f),
        new Vector3(-28.6f, -6.3f, 11.9f),
        new Vector3(-26.3f, -6.3f, 20.8f),
        new Vector3(-15.3f, -6.3f, 24.5f),
        new Vector3(-15.3f, -6.3f, 15.9f),
        new Vector3(-2.2f, -6.3f, 23.8f),
        new Vector3(-2.9f, -6.3f, 14.5f),
        new Vector3(9.7f, -6.3f, 18.2f),
        new Vector3(16.5f, -6.3f, 14.8f),
        new Vector3(21.4f, -6.3f, 7.3f),
        new Vector3(12.0f, -6.3f, 7.5f),
        new Vector3(4.8f, -6.3f, 11.1f),
        new Vector3(-67.8f, -6.3f, 42.1f),
        new Vector3(-63.7f, -6.2f, 43.8f),
        new Vector3(-59.6f, -6.2f, 52.0f),
        new Vector3(-55.5f, -6.2f, 61.6f),
        new Vector3(-43.6f, -6.2f, 55.9f),
        new Vector3(-45.7f, -6.2f, 51.0f),
        new Vector3(-48.3f, -6.2f, 45.5f),
        new Vector3(-42.5f, -6.2f, 43.2f),
        new Vector3(-39.8f, -6.2f, 48.9f),
        new Vector3(-38.0f, -6.2f, 53.3f),
        new Vector3(-26.0f, -6.2f, 48.7f),
        new Vector3(-20.1f, -6.2f, 52.2f),
        new Vector3(-14.8f, -6.2f, 64.1f),
        new Vector3(-12.4f, -6.2f, 69.7f),
        new Vector3(-7.0f, -6.2f, 81.8f),
        new Vector3(-4.5f, -6.2f, 87.1f),
        new Vector3(-9.8f, -6.2f, 89.8f),
        new Vector3(-19.6f, -6.2f, 94.4f),
        new Vector3(-25.0f, -6.2f, 96.5f),
        new Vector3(-27.7f, -6.2f, 91.7f),
        new Vector3(-33.2f, -6.2f, 79.4f),
        new Vector3(-51.0f, -6.2f, 87.1f),
        new Vector3(-58.7f, -6.2f, 69.6f),
        new Vector3(-65.3f, -6.2f, 72.5f),
        new Vector3(-68.1f, -6.2f, 67.2f),
        new Vector3(-61.4f, -6.2f, 63.8f),
        new Vector3(-65.3f, -6.2f, 54.6f),
        new Vector3(-69.0f, -6.2f, 46.2f),
        new Vector3(-53.0f, -6.2f, 67.0f),
        new Vector3(-41.1f, -6.2f, 61.7f),
        new Vector3(-35.9f, -6.2f, 73.6f),
        new Vector3(-47.7f, -6.2f, 79.2f),
        new Vector3(-35.4f, -6.2f, 59.4f),
        new Vector3(-25.3f, -6.2f, 54.8f),
        new Vector3(-20.2f, -6.2f, 66.5f),
        new Vector3(-30.0f, -6.2f, 71.1f),
        new Vector3(-27.7f, -6.2f, 76.8f),
        new Vector3(-17.7f, -6.2f, 72.5f),
        new Vector3(-12.6f, -6.2f, 84.4f),
        new Vector3(-22.4f, -6.2f, 89.1f),
        new Vector3(43.8f, -6.3f, 45.3f)
    };

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
