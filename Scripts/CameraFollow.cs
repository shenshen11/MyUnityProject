using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public float xMargin = 1f; 
	//public float yMargin = 1f; // Distance in the y axis the player can move before the camera follows.
	public float xSmooth = 8f; 
	public float ySmooth = 8f; 
	public Vector2 maxXAndY; 
	public Vector2 minXAndY; 

	private Transform m_Player; 


	private void Awake()
	{

		m_Player = GameObject.FindGameObjectWithTag("Player").transform;
	}


	private bool CheckXMargin()
	{

		return (transform.position.x - m_Player.position.x) < xMargin;
	}


	//private bool CheckYMargin()
	//{
		// Returns true if the distance between the camera and the player in the y axis is greater than the y margin.
		//return Mathf.Abs(transform.position.y - m_Player.position.y) > yMargin;
	//}


	private void Update()
	{
		TrackPlayer();
	}


	private void TrackPlayer()
	{
		float targetX = transform.position.x;
		//float targetY = transform.position.y;

		if (CheckXMargin())
		{
			targetX = Mathf.Lerp(transform.position.x, m_Player.position.x, xSmooth * Time.deltaTime);
		}

		// If the player has moved beyond the y margin...
		//if (CheckYMargin())
		//{
			// ... the target y coordinate should be a Lerp between the camera's current y position and the player's current y position.
			//targetY = Mathf.Lerp(transform.position.y, m_Player.position.y, ySmooth * Time.deltaTime);
		//}

		targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
		//targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

		transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
	}

}

