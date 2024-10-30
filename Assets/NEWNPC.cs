using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.UIElements;

public class NEWNPC : MonoBehaviour
{

    public int rutina;
    public float cronometro;
    public float grado;

    public GameObject Target;

    public Quaternion angulo;

    
    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {
        ComportamientoEnemigo();
    }


    public void ComportamientoEnemigo()
    {
        if(Vector3.Distance(transform.position, Target.transform.position) > 5)
        {
            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;

            }
            switch (rutina)
            {
                case 0:
                    //El personaje esta quieto.
                    break;

                case 1:
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;

                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                    break;
            }
        }
        else
        {
            var lookPos = Target.transform.position - transform.position;
            lookPos.y = 0;
            var RotPos = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, RotPos , 2);

            transform.Translate(Vector3.forward * 2 * Time.deltaTime);
        }
        
    }
}
