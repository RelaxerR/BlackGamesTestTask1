using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePos : MonoBehaviour
{
    private GameObject MovePoint;
    void Start()
    {
        MovePoint = GameObject.FindWithTag("MovePoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touches.Length > 0) {
            var touch = Input.GetTouch(Input.touches.Length - 1);

            var ray = Camera.main.ScreenPointToRay (touch.position);
            RaycastHit hit;

            //Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue);
            if (Physics.Raycast (ray, out hit)) {
                Debug.Log($"Hit! ({hit.transform.position})");
                MovePoint.transform.position = hit.point;
            }
        }
    }
}