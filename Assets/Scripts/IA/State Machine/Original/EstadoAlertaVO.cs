using UnityEngine;
using System.Collections;

public class EstadoAlertaVO : MonoBehaviour {

    //Cantidad de grados que girara por segundo
    public float velocidadGiroBusqueda = 120f;
    //Tiempo que girara sobre si mismo buscando al jugador
    public float duracionBusqueda = 4f;
    //El color del estado alerta es amarillo
    public Color ColorEstado = Color.yellow;

    //Referencias a los scripts
    private MaquinaDeEstadosVO maquinaDeEstados;
    private ControladorNavMeshVO controladorNavMesh;
    private ControladorVisionVO controladorVision;

    //Guardamos la cantidad de tiempo que lleva buscando
    private float tiempoBuscando;

	void Awake () {
        maquinaDeEstados = GetComponent<MaquinaDeEstadosVO>();
        controladorNavMesh = GetComponent<ControladorNavMeshVO>();
        controladorVision = GetComponent<ControladorVisionVO>();
	}

    void OnEnable()
    {
        //Cambiamos el color
        maquinaDeEstados.MeshRendererIndicador.material.color = ColorEstado;
        //Detenemos el navmeshagent
        controladorNavMesh.DetenerNavMeshAgent();
        //Ponemos en cero el tiempo que lleva buscando
        tiempoBuscando = 0f;
    }
	
	void Update () {
        // Ve al jugador?
        RaycastHit hit;
        if (controladorVision.PuedeVerAlJugador(out hit))
        {
            //En la variable hit tendremos la información del jugador y la asignamos en perseguirObjectivo
            controladorNavMesh.perseguirObjectivo = hit.transform;
            //Cambiamos al estado Persecución
            maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoPersecucion);
            //Retornamos para evitar que se ejecute el resto del código
            return;
        }
        //Rotamos sobre el eje y en cada fotograma
        transform.Rotate(0f, velocidadGiroBusqueda * Time.deltaTime, 0f);
        //Incrementamos el tiempo que lleva buscando
        tiempoBuscando += Time.deltaTime;

        //Si el tiempo de búsqueda supera al tiempo de duración
        if(tiempoBuscando >= duracionBusqueda)
        {
            //Cambiamos al estado patrulla
            maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoPatrulla);
            //Retornamos para que no se ejecute le resto del código
            return;
        }
	}
}
