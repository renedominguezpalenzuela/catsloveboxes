using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GatoControlador : MonoBehaviour {

    NavMeshAgent miAgente; //---Componente NavMesh Agent del NPC
    Transform objetivo; //--- objetivo final

    Transform puntoNacimiento;
    Transform puntoFinal;

    public bool iniciar = false;

    public float offset = 1; //distancia para considerar que llego al objetivo
    int posicionActual = 0; //indice del nodo en el que esta

    void Start () {
        if (miAgente == null) {
            miAgente = this.gameObject.GetComponent<NavMeshAgent> ();
        }

        GetComponent<Animator> ().SetBool ("controller_walk", true);

        objetivo = puntoNacimiento;
    }

    void Update () {
        // if (iniciar == false) {
        //     return;
        // }

        miAgente.SetDestination (objetivo.position);

        Vector3 distancia = objetivo.position - transform.position;
        
         if(distancia.magnitude <= offset) {  //se alcanzo el objetivo, cambiar de objetivo
              
              //Cambio de objetivo
              if(posicionActual == 0 ) {
                    posicionActual = 1;
                    objetivo = puntoFinal;
              } else if (posicionActual == 1) {
                    posicionActual = 0;
                    objetivo = puntoNacimiento;
              }
        
             
         }


    }

    public void setPuntos (Transform pPuntoInicial, Transform pPuntoFinal) {

        puntoNacimiento = pPuntoInicial;
        puntoFinal = pPuntoFinal;
        transform.position = puntoNacimiento.position;

    }
}