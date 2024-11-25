using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEngine.UI.Image;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;


    [SerializeField] private float raycastHeight;

    private Transform tr;


    private void Start()
    {
        tr = GetComponent<Transform>();
    }

    private void Update()
    {
        Watch();

        //Move(player);
    }

    private void Move(Transform target)
    {
        agent.SetDestination(target.position);
    }

    private void Watch()
    {
        Vector3 start = tr.position + new Vector3(0, raycastHeight, 0);

        Vector3 end = player.position + new Vector3(0, raycastHeight, 0);

        Vector3 direction = (end - start);

        //Physics.Raycast(start, direction, out RaycastHit hit, distance);

        Debug.DrawRay(start, direction, Color.black);

        if (Physics.Raycast(start, direction, out RaycastHit hit))
        {
            //Debug.DrawRay(start, direction, Color.black);

            Debug.Log(hit.collider.name);
        }


        //if (Physics.Raycast(start, direction, out RaycastHit hit, distance))
        //{
        //    // Si el rayo golpea algo
        //    Debug.Log($"El rayo impactó con: {hit.collider.name}");

        //    // Visualización opcional del rayo en la escena
        //    Debug.DrawLine(tr.position, hit.point, Color.red);

        //    Debug.DrawRay(start, direction * distance, Color.black);

        //}




        //Physics.Raycast(tr.position, direction, out RaycastHit hit, distance);
        //Debug.DrawLine(tr.position, hit.point, Color.red);


        //Vector3 origen = transform.position;
        //Vector3 direccion = Vector3.forward;
        //float distancia = 12f;

        //RaycastHit2D hit = Physics2D.Raycast(origen, direccion, distancia);
        //Debug.DrawRay(origen, direccion * distancia, Color.red);



        ////Physics.Raycast(tr.position, player, out RaycastHit hit, maxWatchDistance);
        //Ray ray = new Ray(transform.position, player.transform.position);
        //Debug.DrawRay(transform.position, player.transform.position * maxWatchDistance, Color.red);








        ////Comprueba el angulo del raycast
        //float angle = Vector3.Angle(rayDirection, hit.point - transform.position);

        //// Comprobamos si el ángulo está dentro del rango de +-45º
        //if (angle <= angleThreshold)
        //{
        //    // Si está dentro del ángulo permitido, hacemos algo
        //    Debug.Log("El Raycast está dentro del ángulo permitido.");
        //    // Aquí puedes agregar lo que quieres hacer cuando esté dentro del ángulo
        //}
    }
}
