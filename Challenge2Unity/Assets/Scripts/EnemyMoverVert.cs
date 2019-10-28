using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoverVert : MonoBehaviour
{
	public float yPos;
	

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, (yPos + Mathf.PingPong(Time.time, 1)), transform.position.z);
    }
}
