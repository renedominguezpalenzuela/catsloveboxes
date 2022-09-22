using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Action {
    Normal,
    FoundBox,
    FoundFrog,
    FoundDog
}

public class GatoControlador : MonoBehaviour {

    NavMeshAgent miAgente; //---Componente NavMesh Agent del NPC
    public Transform punto_destino; //--- punto_destino final

    Transform puntoNacimiento;
    Transform puntoFinal;

    public bool iniciar = false;

    public float offset = 1; //distancia para considerar que llego al punto_destino
    int posicionActual = 0; //indice del nodo en el que esta

    Action accion = Action.Normal;

    Vector3 distancia;

    public Transform origen_rayos; //punto desde donde se original los rayos

    public float longitud_rayo = 23f;

    LayerMask mascara_colisiones;

    void Start () {
        if (miAgente == null) {
            miAgente = this.gameObject.GetComponent<NavMeshAgent> ();
        }

        GetComponent<Animator> ().SetBool ("controller_walk", true);

        punto_destino = puntoNacimiento;

        mascara_colisiones = LayerMask.GetMask ("rayos_gato");
    }

    void Update () {
        // if (iniciar == false) {
        //     return;
        // }

        //Vision frontal del gato
        RaycastHit hit;

        if (!Physics.Raycast (origen_rayos.position, origen_rayos.forward, out hit, longitud_rayo, mascara_colisiones)) {
            if (accion == Action.FoundBox) {
                accion = Action.Normal;
                punto_destino = puntoFinal;
            }
        }

        if (punto_destino == null) {
            return;
        }

        //Movimiento
        miAgente.SetDestination (punto_destino.position);

        //Calculo de distancia
        distancia = punto_destino.position - transform.position;

        //Determinar accion a ejecutar
        switch (accion) {
            case Action.Normal:
                {
                    AccionNormal ();
                    break;
                }

            default:
                {
                    break;
                }

        }

    }

    //--------------------------------------------------------------------------------------------------------
    // Funcion llamada desde el GeneradorGatos para setear puntos de origen y final
    //--------------------------------------------------------------------------------------------------------
    public void setPuntos (Transform pPuntoInicial, Transform pPuntoFinal) {

        puntoNacimiento = pPuntoInicial;
        puntoFinal = pPuntoFinal;
        transform.position = puntoNacimiento.position;

    }

    //-------------------------------------------------------------------------------------------
    // Camina entre dos puntos
    //-------------------------------------------------------------------------------------------
    private void AccionNormal () {
        if (distancia.magnitude <= offset) { //se alcanzo el punto_destino, cambiar de punto_destino

            //Cambio de punto_destino
            if (posicionActual == 0) {
                posicionActual = 1;
                punto_destino = puntoFinal;
            } else if (posicionActual == 1) {
                posicionActual = 0;
                punto_destino = puntoNacimiento;
            }

        }
    }

    void OnDrawGizmos () {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay (origen_rayos.position, origen_rayos.forward * longitud_rayo);
    }

    public void setDestino (Transform pDestino) {

        Debug.Log ("Seteando destinoi");

        punto_destino = pDestino;

        accion = Action.FoundBox;

    }

}