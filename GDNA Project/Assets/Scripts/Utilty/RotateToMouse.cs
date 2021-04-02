using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    public float mouseAngle;
    public int offsetAngle = 90;

    // Update is called once per frame
    void Update()
    {
        //get world position of mouse
        Vector3 mousePos = GetWorldPositionOnPlane(Input.mousePosition, 0);

        //calculate magnitude of x and y components of resultant vector between object to mouse
        mousePos.x -= transform.position.x;
        mousePos.y -= transform.position.y;

        //caclulate angle in degrees of resultant vector 
        mouseAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        //set object rotation to same as calculated angle 
        //(added offset to correct orientation of object due to incorrect inherent orientation of object's sprite)
        transform.rotation = Quaternion.AngleAxis(mouseAngle + offsetAngle, Vector3.forward);

    }

    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        //set ray from camera through screen point at mouse position
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        //create plane 
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));

        //raycast to plane
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}
