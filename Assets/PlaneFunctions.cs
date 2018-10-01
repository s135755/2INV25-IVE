using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneFunctions : MonoBehaviour{

	// Bottom left corner point as 
	// Get a point for the peg for the given mouse location
	public Vector3 getPegPointByMouseLocation(Vector2 mouseLocation) {
		
		// Get bottom left point
		Vector3 planeCenter = this.transform.position;
		Vector3 extent = GetComponent<Renderer>().bounds.extents;
		Vector3 bottomLeft = new Vector3(planeCenter.x - extent.x, planeCenter.y, planeCenter.z - extent.z);

		// Get relative position of mouse on the plane
		// Scale from screen size to plane size
		float w = extent.x * 2;
		float h = extent.z * 2;
		float pegX = bottomLeft.x + (w / Screen.width) * mouseLocation.x;
		float pegZ = bottomLeft.z + (h / Screen.height) * mouseLocation.y;

		return new Vector3(pegX, planeCenter.y, pegZ);
	}
}
