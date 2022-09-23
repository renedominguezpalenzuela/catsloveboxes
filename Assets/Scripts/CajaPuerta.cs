using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CajaPuerta : MonoBehaviour {

    //Esfera en el centro de la caja, para pasar sus coordenadas al gato
    public Transform Centro_Caja;

    //Caja para poder saber la orientacion de la caja
    public Transform Caja;

    void Start () {

        // mascara_colisiones = LayerMask.GetMask ("gato");
    }

}