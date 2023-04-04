using UnityEngine;
using System.Collections;

public class EstadoPersecucionVO : MonoBehaviour {

    //El color del estaod persecución será rojo
    public Color ColorEstado = Color.red;
    //Referencia a los scripts
    private MaquinaDeEstadosVO maquinaDeEstados;
    private ControladorNavMeshVO controladorNavMesh;
    private ControladorVisionVO controladorVision;

	void Awake () {
        maquinaDeEstados = GetComponent<MaquinaDeEstadosVO>();
        controladorNavMesh = GetComponent<ControladorNavMeshVO>();
        controladorVision = GetComponent<ControladorVisionVO>();
	}

    void OnEnable()
    {
        //Cambiamos el color del indicador de estado
        maquinaDeEstados.MeshRendererIndicador.material.color = ColorEstado;
    }
	
	void Update () {

        RaycastHit hit;
        //Si dejamos de ver al jugador
        if(!controladorVision.PuedeVerAlJugador(out hit, true))
        {
            //Pasamos al estado alerta
            maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoAlerta);
            //Retornamos para que no se ejecute el resto del código
            return;
        }

        //Mientras veamos al jugador actualizamos la posición de destino
        controladorNavMesh.ActualizarPuntoDestinoNavMeshAgent();    
	}
}
