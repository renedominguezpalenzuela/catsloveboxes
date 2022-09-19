using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase==TouchPhase.Began) {

            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            Debug.DrawRay(ray.origin, ray.direction*1000,Color.green, 5, false );

            RaycastHit hit;

            if(Physics.Raycast(ray, out hit )) {
                if (hit.collider !=null && hit.collider.gameObject.tag == "Seleccionable")  {
                    Color nuevoColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
                    hit.collider.GetComponent<MeshRenderer>().material.color = nuevoColor;
                }
            }         
        }


      #if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) {

             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction*1000,Color.green, 5, false );

            RaycastHit hit;

            if(Physics.Raycast(ray, out hit )) {
                if (hit.collider !=null && hit.collider.gameObject.tag == "Seleccionable")  {
                    Color nuevoColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
                    hit.collider.GetComponent<MeshRenderer>().material.color = nuevoColor;
                }
            }    

        }
      #endif  
        
    }
}


/*

if(Physics.Raycast(transform.position,transform.forward,out hit)) { 
    if (hit.collider.gameObject.tag == "Tagged") {
       Debug.DrawRay(transform.position, transform.forward, Color.green); 
       print("Hit"); } 
    }
*/