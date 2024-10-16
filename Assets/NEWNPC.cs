using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEWNPC : MonoBehaviour
{

    public int rutina;
    public float cronometro;
    public float grado;


    public Quaternion angulo;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ComportamientoEnemigo()
    {
        cronometro += 1 * Time.deltaTime;
        if(cronometro >= 4)
        {
            rutina = Random.Range(0, 2);
            cronometro = 0;

        }
        switch (rutina)
        {
            case 0:
             //
             break;

            case 1:
                grado = 0;
                break;
        }
    }
}
