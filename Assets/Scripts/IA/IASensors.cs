using UnityEngine;
using System.Collections.Generic;

public class IASensors: MonoBehaviour
    {
    //Sensores
    public GameObject[] sensors;
    private Vector3 sensorPosition;
    private Vector3 sensorDirection;
    private Ray ray;
    private RaycastHit[] hit;

    //Script de movimiento y disparo

    private Complete.TankMovementIA movTanque;
    private Complete.TankShootingIA shootTanque;

    //Posición del NPC
    private Vector3 npcPosition;

    public int contador = 0;

    private void Awake()
    {
        movTanque = GetComponent<Complete.TankMovementIA>();
        shootTanque = GetComponent<Complete.TankShootingIA>();

    }
     void FixedUpdate()
    {
        
    }

    void Update() {
        ActualizaPosiciones();
    }

   

    public void ActualizaPosiciones()
    {
        npcPosition = transform.position;

        for (int i = 0; i < sensors.Length; i++)
        {
            sensorPosition = sensors[i].transform.position;
            sensorDirection = sensorPosition - npcPosition;
            ray = new Ray(npcPosition, sensorPosition);
            Debug.DrawRay(ray.origin, sensorDirection * 1f, Color.blue);
            //CompruebaObstaculos(ray);
        }

    }

    /*private void CompruebaObstaculos(Ray rayo)
    {
        hit = Physics.RaycastAll(rayo);

        Debug.Log(hit.Length);

        if (hit.Length >0)
        {
            

            foreach (RaycastHit contacto in hit)
            {
                GameObject obstaculo = contacto.transform.gameObject;
                Vector3 obstaculoPosition = obstaculo.transform.position;
                Vector3 obstaculoDirection = obstaculoPosition - npcPosition;
                float distanciaObstaculo = Vector3.Distance(npcPosition, obstaculoPosition);
                //Debug.Log("Obstaculo en frente " + contacto.transform.gameObject.name + " a distancia: " + distanciaObstaculo);
                Debug.DrawRay(ray.origin, obstaculoDirection * 2.0f, Color.red);
                

                if(contacto.transform.gameObject.tag == "Player")
                {
                    Debug.Log("Tengo un jugador a la vista");
                    Debug.DrawRay(ray.origin, obstaculoDirection * 5.0f, Color.green);
                }

                break;
            }
        }     
    }*/

    void OnTriggerEnter(Collider contacto)
    {
        Debug.Log(contacto.name);
        
        Vector3 nuevaDirection = contacto.transform.position - npcPosition;
        movTanque.CambioDeDireccion(npcPosition);



    }
}
