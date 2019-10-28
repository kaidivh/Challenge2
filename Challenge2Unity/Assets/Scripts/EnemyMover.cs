using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
	public float xPos;
	

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3((xPos + Mathf.PingPong(Time.time, 1)), transform.position.y, transform.position.z);
    }
}
