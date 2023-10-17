using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScr : MonoBehaviour
{
    public GameObject target;
    public GameObject point;
    public float minFoV = 15.0f;
    public float maxFoV = 60.0f;
    public float zoomDuration = 1.0f;

    // public void zoomin(){
    //     StartCoroutine(ZoomIn());
    // }

    public void zoomout(){
        transform.position = new Vector3(33.0f, 8.0f, 12.5f);
        transform.rotation = Quaternion.Euler(2.0f, -135.5f, 0.0f);
        Camera.main.fieldOfView = 60.0f;
    }

    // IEnumerator ZoomIn()
    // {
    //     float startTime = Time.time;
    //     float startFoV = Camera.main.fieldOfView;
    //     float endFoV = minFoV;
    //     Vector3 startPosition = transform.position;
    //     Vector3 endPosition = point.transform.position;
    //     while (Time.time < startTime + (zoomDuration*1.0f))
    //     {
    //         float t = (Time.time - startTime) / (zoomDuration*1.0f);
    //         Camera.main.fieldOfView = Mathf.Lerp(startFoV, endFoV, t);
    //         transform.position = Vector3.Lerp(startPosition, endPosition, t);
    //         transform.LookAt(target.transform.position);
    //         yield return null;
    //     }
    //     Camera.main.fieldOfView = endFoV;
    //     transform.position = endPosition;
    // }

    public void zoomin(){
        transform.position = point.transform.position;
        transform.LookAt(target.transform.position);
        Camera.main.fieldOfView = 15.0f;
    }
}