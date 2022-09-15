using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatoControlador : MonoBehaviour {
    Transform puntoNacimiento;
    Transform puntoFinal;

    public bool iniciar = false;

    public float velocidad = 1;
   
    void Start () {
        GetComponent<Animator> ().SetBool ("controller_walk", true);
    }

    void Update () {
        if (iniciar == false) {
            return;
        }

        transform.position += new Vector3 (0, 0, velocidad * Time.deltaTime);

        if (transform.position.z >= puntoFinal.position.z) {
            velocidad *= -1;
            transform.Rotate (0, 180, 0);
        }

        if (transform.position.z <= puntoNacimiento.position.z) {
            velocidad *= -1;
            transform.Rotate (0, 180, 0);
        }

    }

    public void setPuntos (Transform pPuntoInicial, Transform pPuntoFinal) {

        puntoNacimiento = pPuntoInicial;
        puntoFinal = pPuntoFinal;
        transform.position = puntoNacimiento.position;

    }
}