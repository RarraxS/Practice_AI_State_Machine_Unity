using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYERMOVEMENTE : MonoBehaviour
{

    public float speed = 5f; // Velocidad de movimiento

    public float rotationSpeed = 5f;

    private Vector3 movement;

    void Update()
    {

        //REVISAR SCRIPT ---------------------------------------------------------------------------------------
        // Obtener las entradas del teclado
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D o Flechas izquierda/derecha
        float moveVertical = Input.GetAxis("Vertical"); // W/S o Flechas arriba/abajo


        transform.Translate(Vector3.forward * Time.deltaTime * moveHorizontal);

        //rotacion de personaje
        transform.Rotate(Vector3.up * moveHorizontal * rotationSpeed * Time.deltaTime);
    }
}

