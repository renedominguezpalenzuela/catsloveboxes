using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CajaCentroColisiones : MonoBehaviour {

    float longitud_rayo = 15f;
    LayerMask mascara_colisiones;

    //Esfera en el centro de la caja, para pasar sus coordenadas al gato
    public Transform Centro_Caja;

    //Caja, para poder desactivar el NavMesh Obstacle
    public Transform Caja;

    void Start () {

        // mascara_colisiones = LayerMask.GetMask ("gato");
    }

    // Update is called once per frame
    void Update () {

        // RaycastHit hit;

        // if (Physics.Raycast (transform.position, transform.forward, out hit, longitud_rayo)) {

        //     if (hit.transform.tag == "gato") {
        //         Debug.Log ("Colision Centro de colisiones");
        //         Debug.Log (hit.transform.tag);
        //         hit.transform.gameObject.GetComponent<GatoControlador> ().setDestino (Centro_Caja);
        //     }
        // }

        // RaycastHit hit;

        // if (Physics.Raycast (transform.position, transform.forward, out hit, longitud_rayo)) {

        //     if (hit.transform.tag == "gato") {
        //         Debug.Log ("Colision Centro de colisiones");
        //         Debug.Log (hit.transform.tag);
        //         hit.transform.gameObject.GetComponent<GatoControlador> ().setDestino (Centro_Caja);

        //         if (Caja.GetComponent<NavMeshObstacle> ().enabled == true) {
        //             Caja.GetComponent<NavMeshObstacle> ().enabled = false;
        //         }

        //     }
        // } else {
        //     // if (Caja.GetComponent<NavMeshObstacle> ().enabled == false) {
        //     //     Caja.GetComponent<NavMeshObstacle> ().enabled = true;
        //     // }
        // }

    }

    // public void GatoVioCaja () {
    //     Debug.Log ("Gato Vio Caja");

    //     //Vision frontal del gato
    //     RaycastHit hit;

    //     if (Physics.Raycast (transform.position, transform.forward, out hit, longitud_rayo)) {

    //         if (hit.transform.tag == "gato") {
    //             Debug.Log ("Colision Centro de colisiones");
    //             Debug.Log (hit.transform.tag);
    //             hit.transform.gameObject.GetComponent<GatoControlador> ().setDestino (Centro_Caja);

    //             //Caja.GetComponent<NavMeshObstacle>().enabled =false;
    //         }
    //     } else {

    //     }
    // }

    void OnDrawGizmos () {
        Gizmos.color = Color.red;
        Gizmos.DrawRay (transform.position, transform.forward * longitud_rayo);
    }

}