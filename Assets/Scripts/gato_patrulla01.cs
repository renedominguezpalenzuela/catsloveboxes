using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gato_patrulla01 : MonoBehaviour {

    public Transform puntoNacimiento;
    public Transform puntoFinal;

    public float velocidad = 1;
    // Start is called before the first frame update
    void Start () {
        GetComponent<Animator> ().SetBool ("controller_walk", true);

        transform.position = puntoNacimiento.position;
    }

    // Update is called once per frame
    void Update () {
        transform.position += new Vector3 (velocidad * Time.deltaTime, 0, 0);

        if (transform.position.x >= puntoFinal.position.x) {
            velocidad *= -1;
            transform.Rotate (0, 180, 0);
        }

        if (transform.position.x <= puntoNacimiento.position.x) {
            velocidad *= -1;
            transform.Rotate (0, 180, 0);
        }

    }

//    public void
}