using UnityEngine;
using System.Collections;

/// <summary>
/// This is a test class to demonstrate the scaling behaviour with AutoTextureTiling in comparison to DynamicTextureTiling.
/// </summary>
public class ScaleAnimation : MonoBehaviour {

	public float minScale = 1f;
	public float maxScale = 2f;

	private float targetScale;

	void Start() {

		targetScale = maxScale;

	}

	// Update is called once per frame
	void FixedUpdate () {
	
		if (transform.localScale.x < targetScale) {
//			Debug.Log("Scaling up");
			transform.localScale = new Vector3(transform.localScale.x + Time.deltaTime, transform.localScale.y, transform.localScale.z);
			if (transform.localScale.x >= maxScale) {
//				Debug.Log("Switching dir to Scaling down");
				targetScale = minScale;
			}
		}
		else if (transform.localScale.x > targetScale) {
//			Debug.Log("Scaling down");
			transform.localScale = new Vector3(transform.localScale.x - Time.deltaTime, transform.localScale.y, transform.localScale.z);
			if (transform.localScale.x <= minScale) {
//				Debug.Log("Switching dir to Scaling up");
				targetScale = maxScale;
			}
		}

	}

}
