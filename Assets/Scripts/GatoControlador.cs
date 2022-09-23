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
   
    float longitud_rayo = 2f;

    LayerMask mascara_colisiones;

    public Vector3 punto_frente;
    public Vector3 punto_detras;

    float distance_punto_forward = 3f;
    public Transform forward_reference;

    public DireccionObjetos direccion_gato;

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

        direccion_gato = calcular_direccion_gato ();

        //Vision frontal del gato
        RaycastHit hit;

        if (Physics.Raycast (origen_rayos.position, origen_rayos.forward, out hit, longitud_rayo, mascara_colisiones)) {

            if (hit.transform.tag == "puerta_caja") {

                DireccionObjetos direccion_puerta_caja = hit.transform.gameObject.GetComponent<CajaPuerta>().direccion_puerta_caja;

                Debug.Log ("Puerta encontrada");

                if (direccionGatoCajaOK (direccion_gato, direccion_puerta_caja)) {
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
        distanciaAbs = Mathf.Abs (distancia.magnitude);

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