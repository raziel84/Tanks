using UnityEngine;
using System.Collections;

public class ControladorRangoDeVision : MonoBehaviour {

    public Transform Ojos;
    public float rangoVision = 20f;
    public Vector3 offset = new Vector3(0f, 0.75f, 0f);

    private ControladorNavMeshAgent controladorNavMesh;

    void Awake()
    {
        controladorNavMesh = GetComponent<ControladorNavMeshAgent>();
    }

    void Update()
    {
        /* Código para debug - Ver el rayo
        
        Vector3 vectorDireccion;
        //vectorDireccion = (controladorNavMesh.perseguirObjectivo.position + offset) - Ojos.position;
        //valorADevolver = Physics.Raycast(Ojos.position, vectorDireccion, out hit, rangoVision) && hit.collider.CompareTag("Player");
        vectorDireccion = Ojos.forward;
        Ray ray = new Ray(Ojos.position, (controladorNavMesh.perseguirObjectivo.position + offset));
        Debug.DrawRay(ray.origin, vectorDireccion * 1f, Color.blue);

        RaycastHit hit;

        Debug.Log(Physics.Raycast(Ojos.position, vectorDireccion, out hit, rangoVision) && hit.collider.CompareTag("Player"));*/
    }

    public bool PuedeVerAlJugador(out RaycastHit hit, bool mirarHaciaElJugador = false)
    {

        Vector3 vectorDireccion;
        if (mirarHaciaElJugador)
        {
            vectorDireccion = (controladorNavMesh.perseguirObjectivo.position + offset) - Ojos.position;
        }
        else
        {
            vectorDireccion = Ojos.forward;
        }

        return Physics.Raycast(Ojos.position, vectorDireccion, out hit, rangoVision) && hit.collider.CompareTag("Player");
        
    }
}
