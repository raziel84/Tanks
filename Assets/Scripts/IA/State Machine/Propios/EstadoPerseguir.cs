using UnityEngine;
using System.Collections;

public class EstadoPerseguir : BaseEstado
{
    private ControladorNavMeshAgent controladorNavMesh;
    private ControladorRangoDeVision controladorVision;

    protected override void Awake()
    {
        //La palabra reservada base se utiliza para llamar a métodos de la clase de la cual se hereda
        base.Awake();
        controladorNavMesh = GetComponent<ControladorNavMeshAgent>();
        controladorVision = GetComponent<ControladorRangoDeVision>();
	}

    void OnEnable()
    {
    }
	
	void Update () {
        RaycastHit hit;
        
        if(!controladorVision.PuedeVerAlJugador(out hit, true))
        {
            maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoAlerta);
            return;
        }
        Debug.DrawLine(transform.position, hit.transform.position);
        controladorNavMesh.ActualizarPuntoDestinoNavMeshAgent();    
	}
}
