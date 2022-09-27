using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum Action {
    Normal,
    FoundBox,
    FoundFrog,
    FoundDog
}

public class GatoControlador : MonoBehaviour {

    NavMeshAgent miAgente; //---Componente NavMesh Agent del NPC
    public Transform punto_destino; //--- punto_destino final
    Vector3 distancia;
    float distanciaAbs = 0;

    public Transform punto_destino_anterior;

    public Transform puntoNacimiento;
    public Transform puntoFinal;

    public bool iniciar = false;

    float offset = 0.8f; //distancia para considerar que llego al punto_destino
    public int posicionActual = 0; //indice del nodo en el que esta

    public Action accion = Action.Normal;

    public Transform origen_rayos; //punto desde donde se original los rayos
                                   //debe ubicarse detras del gato si queda dentro del collider no se detecta colsion
    float longitud_rayo = 10f;

    LayerMask mascara_colisiones;

    Vector3 punto_frente;
    Vector3 punto_detras;

    float distance_punto_forward = 2f;
  

    public DireccionObjetos direccion_gato;


    Text txtPuerta;
    Text txtHittedTag;
    Text txtPuertaDireccionOK;

    void Start () {
        if (miAgente == null) {
            miAgente = this.gameObject.GetComponent<NavMeshAgent> ();
        }

        GetComponent<Animator> ().SetBool ("controller_walk", true);

        punto_destino = puntoFinal;
        punto_destino_anterior = punto_destino;

        mascara_colisiones = LayerMask.GetMask ("rayos_gato");

        txtPuerta = GameObject.Find ("txtPuertaEncontrada").GetComponent<Text> ();
        txtHittedTag = GameObject.Find ("txtHittedTag").GetComponent<Text> ();

        txtPuertaDireccionOK= GameObject.Find ("txtPuertaDireccionOK").GetComponent<Text> ();

    }

    bool cambio_accion = false;

    void Update () {
        // if (iniciar == false) {
        //     return;
        // }

        //saber cual es la direccion que tiene el gato: N,S,E,W
        direccion_gato = calcular_direccion_gato ();

        bool puerta_vista = false;
        bool puerta_direccion_ok = false;

        //Vision frontal del gato
        RaycastHit hit;
        Transform Centro_Caja = null;

        //-------------- SENSORES -----------------------------------------------------------------------------------  
        //if (Physics.Raycast (origen_rayos.position, origen_rayos.forward, out hit, longitud_rayo, mascara_colisiones)) {
            if (Physics.SphereCast (origen_rayos.position, 0.4f, origen_rayos.forward, out hit, longitud_rayo, mascara_colisiones)) {

            if (hit.collider) {

                txtHittedTag.text = "Hited: " + hit.collider.gameObject.name;

                if (hit.transform.tag == "puerta_caja") {
                    puerta_vista = true;

                    DireccionObjetos direccion_puerta_caja = hit.transform.gameObject.GetComponent<CajaPuerta> ().direccion_puerta_caja;
                    puerta_direccion_ok = direccionGatoCajaOK (direccion_gato, direccion_puerta_caja);
                    if (puerta_direccion_ok) {
                        Centro_Caja = hit.transform.gameObject.GetComponent<CajaPuerta> ().Centro_Caja;
                    }
                }
            } else {
                txtHittedTag.text = "NO Hited ";
            }

        }

         txtPuerta.text = "Puerta Encontrada: " + puerta_vista;
          txtPuertaDireccionOK.text = "Puerta Direccion OK: " + puerta_direccion_ok;

      

        //--------- Toma de Decision: determinar accion a realizar usando datos de sensores --------------------

        //Debug.Log("Puerta "+puerta_vista);
        //Debug.Log("Puerta DIR "+puerta_direccion_ok);

       

        if (puerta_vista && puerta_direccion_ok) {

            if (accion != Action.FoundBox) {

                cambio_accion = true;
            }
            accion = Action.FoundBox;
        } else {

            if (accion != Action.Normal) {
                cambio_accion = true;
            }
            accion = Action.Normal;
        }

        switch (accion) {
            case Action.FoundBox:
                {
                    if (cambio_accion) {
                        Debug.Log ("Cambio accion a FoundBox  ");
                        //punto_destino_anterior = punto_destino;

                         if (posicionActual == 0) {
                           
                            punto_destino_anterior = puntoNacimiento ;
                           

                        } else if (posicionActual == 1) {
                         
                            punto_destino_anterior = puntoFinal;
                         
                        }

                        cambio_accion = false;
                    }
                    punto_destino = Centro_Caja;

                    break;
                }

            case Action.Normal:
                {

                    if (cambio_accion) {
                        Debug.Log ("Ejecutanfo Cambio a Normal");

                        punto_destino = punto_destino_anterior;

                        // if (posicionActual == 0) {
                        //     posicionActual = 1;
                        //     punto_destino_anterior = puntoNacimiento;
                        //     punto_destino = puntoFinal;

                        // } else if (posicionActual == 1) {
                        //     posicionActual = 0;
                        //     punto_destino_anterior = puntoFinal;
                        //     punto_destino = puntoNacimiento;                            
                        // }

                        cambio_accion = false;
                    } else {
                        Debug.Log ("Ejecutando normal");
                        AccionNormal ();

                    }

                    break;
                }

            default:
                {
                    break;
                }

        }

        //---------  Ejecutar acciones ------------------------------------------------------------------------

        if (punto_destino == null) {
            Debug.Log ("Punto Destino null");
            return;
        }

        //Movimiento
        miAgente.SetDestination (punto_destino.position);

    }

    //-------------------------------------------------------------------------------------------
    // Camina entre dos puntos
    //-------------------------------------------------------------------------------------------
    private void AccionNormal () {

        //Calculo de distancia
        distancia = punto_destino.position - transform.position;
        distanciaAbs = Mathf.Abs (distancia.magnitude);

        if (distanciaAbs <= offset) { //se alcanzo el punto_destino, cambiar de punto_destino
            //puede_cambiar_accion = true;
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

    //--------------------------------------------------------------------------------------------------------
    // Funcion llamada desde el GeneradorGatos para setear puntos de origen y final
    //--------------------------------------------------------------------------------------------------------
    public void setPuntos (Transform pPuntoInicial, Transform pPuntoFinal) {

        puntoNacimiento = pPuntoInicial;
        puntoFinal = pPuntoFinal;
        transform.position = puntoNacimiento.position;

    }

    void OnDrawGizmos () {
        Gizmos.color = Color.blue;

        Gizmos.DrawRay (origen_rayos.position, origen_rayos.forward * longitud_rayo);

        //         Gizmos.DrawSphere(origen_rayos.position, longitud_rayo);
    }

    // public void setDestino (Transform pDestino) {
    //     Debug.Log ("Seteando destinoi");
    //     punto_destino = pDestino;
    //     accion = Action.FoundBox;
    // }

    //---------------------------------------------------------------------------------------------
    // Calculo de direccion del gato
    // solo valido si puntos de inicio y fin estan correctamente alineados en Z o X 
    // Version mejor calcula el angulo de forward con respecto al eje Z (Este) del universo
    //--------------------------------------------------------------------------------------------
    private DireccionObjetos calcular_direccion_gato () {
        punto_frente = transform.position + transform.forward * distance_punto_forward;
        punto_detras = transform.position;

        float distancia = distance_punto_forward - 1;

        direccion_gato = DireccionObjetos.Este;

        if ((punto_frente.z - punto_detras.z) > distancia) {
            direccion_gato = DireccionObjetos.Este;
        }

        if ((punto_detras.z - punto_frente.z) > distancia) {
            direccion_gato = DireccionObjetos.Oeste;
        }

        if ((punto_frente.x - punto_detras.x) > distancia) {
            direccion_gato = DireccionObjetos.Sur;
        }

        if ((punto_detras.x - punto_frente.x) > distancia) {
            direccion_gato = DireccionObjetos.Norte;
        }

        return direccion_gato;

    }

    public bool direccionGatoCajaOK (DireccionObjetos dir_gato, DireccionObjetos dir_caja) {

        if (dir_caja == DireccionObjetos.Oeste && dir_gato == DireccionObjetos.Este) {

            return true;
        } else {

            return false;
        }

    }

}