using UnityEngine;
using System.Collections;

public class CameraSmoothFollow : MonoBehaviour {
	public Transform target;            
	public float smoothing = 5f;        
	
	Vector3 offset;                    
	
	void Start ()
	{
		CodeProfiler.Begin("CameraSmoothFollow:Start");
		offset = transform.position - target.position;
		CodeProfiler.End("CameraSmoothFollow:Start");
	}
	
	void FixedUpdate ()
	{
		CodeProfiler.Begin("CameraSmoothFollow:FixedUpdate");
		Vector3 targetCamPos = target.position + offset;
		

		transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
		CodeProfiler.End("CameraSmoothFollow:FixedUpdate");
	}
}
