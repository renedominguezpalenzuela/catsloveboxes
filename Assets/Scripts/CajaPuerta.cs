using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Esto no se debe pasar a Caja controlador
//pues caja controlador tiene otro collider para los toques 

public class CajaPuerta : MonoBehaviour {

    //Esfera en el centro de la caja, para pasar sus coordenadas al gato
    public Transform Centro_Caja;

    //Caja para poder saber la orientacion de la caja
    public Transform Caja;


    //Direccion de la puerta de la caja, obtenido desde el parent
    public DireccionObjetos direccion_puerta_caja = DireccionObjetos.Sur;

    void Start () {

        // mascara_colisiones = LayerMask.GetMask ("gato");
    }

    void Update() {
        direccion_puerta_caja = Caja.GetComponent<CajaControlador>().direccion_puerta_caja;
    }

}