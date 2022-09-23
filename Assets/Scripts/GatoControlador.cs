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
    public Vector3 distancia;
    public float distanciaAbs = 0;

    Transform punto_destino_anterior;

    public Transform puntoNacimiento;
    public Transform puntoFinal;

    public bool iniciar = false;

    public float offset = 0.8f; //distancia para considerar que llego al punto_destino
    public int posicionActual = 0; //indice del nodo en el que esta

    Action accion = Action.Normal;

    

    public Transform origen_rayos; //punto desde donde se original los rayos
    public float longitud_rayo = 23f;

    LayerMask mascara_colisiones;

    void Start () {
        if (miAgente == null) {
            miAgente = this.gameObject.GetComponent<NavMeshAgent> ();
        }

        GetComponent<Animator> ().SetBool ("controller_walk", true);

        punto_destino = puntoNacimiento;
        punto_destino_anterior = punto_destino;

        mascara_colisiones = LayerMask.GetMask ("rayos_gato");
    }

    void Update () {
        // if (iniciar == false) {
        //     return;
        // }

        //Vision frontal del gato
        RaycastHit hit;

        if (Physics.Raycast (origen_rayos.position, origen_rayos.forward, out hit, longitud_rayo, mascara_colisiones)) {
            // Debug.Log("Algo Encontrado");
            // Debug.Log("TAG: "+hit.transform.tag);

            if (hit.transform.tag == "puerta_caja") {
                Transform caja = hit.transform.gameObject.GetComponent<CajaPuerta> ().Caja;
                float caja_angulo_y = Mathf.Abs (caja.eulerAngles.y);
                float gato_angulo_y = Mathf.Abs (transform.rotation.eulerAngles.y);
                  Debug.Log ("Puerta encontrada"  );
                  Debug.Log("caja_angulo_y "+caja_angulo_y);
                  Debug.Log("gato_angulo_y "+gato_angulo_y);

                //Caja orientada hacia el W, el gato va hacia el este  
                if (caja_angulo_y == 180 && gato_angulo_y < 20) {
                    Transform Centro_Caja = hit.transform.gameObject.GetComponent<CajaPuerta> ().Centro_Caja;
                    Debug.Log ("Puerta direccion correcta ");
                    accion = Action.FoundBox;
                    punto_destino_anterior = punto_destino;
                    punto_destino = Centro_Caja;

                }

            }

        } else {

          // punto_destino =  punto_destino_anterior;
           accion = Action.Normal;

        }

  


        if (punto_destino == null) {
            return;
        }

    
        //Movimiento
        miAgente.SetDestination (punto_destino.position);
                  //Calculo de distancia
        distancia = punto_destino.position - transform.position;
        distanciaAbs = Mathf.Abs(distancia.magnitude);

     
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

   
        
        if (distanciaAbs <= offset) { //se alcanzo el punto_destino, cambiar de punto_destino

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

    // public void setDestino (Transform pDestino) {
    //     Debug.Log ("Seteando destinoi");
    //     punto_destino = pDestino;
    //     accion = Action.FoundBox;
    // }

}