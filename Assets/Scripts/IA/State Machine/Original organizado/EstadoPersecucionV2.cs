using UnityEngine;
using System.Collections;

public class EstadoPersecucionV2 : BaseEstado
{
    private ControladorNavMeshV2 controladorNavMesh;
    private ControladorVisionV2 controladorVision;

    protected override void Awake()
    {
        //La palabra reservada base se utiliza para llamar a métodos de la clase de la cual se hereda
        base.Awake();
        controladorNavMesh = GetComponent<ControladorNavMeshV2>();
        controladorVision = GetComponent<ControladorVisionV2>();
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

        controladorNavMesh.ActualizarPuntoDestinoNavMeshAgent();    
	}
}
