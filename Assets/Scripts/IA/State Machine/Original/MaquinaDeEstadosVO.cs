using UnityEngine;
using System.Collections;

public class MaquinaDeEstadosVO : MonoBehaviour {

    //Referencia a cada script de Estado
    public MonoBehaviour EstadoPatrulla;
    public MonoBehaviour EstadoAlerta;
    public MonoBehaviour EstadoPersecucion;

    //Referencia al estado inicial
    public MonoBehaviour EstadoInicial;

    //Variable para cambiar el color de un indicador de estado
    public MeshRenderer MeshRendererIndicador;

    //Referencia al estado actual
    private MonoBehaviour estadoActual;

    void Start () {

        //Activamos el estado inicial
        ActivarEstado(EstadoInicial);
	}
	
    //Método para cambiar de estado
    public void ActivarEstado(MonoBehaviour nuevoEstado)
    {
        //Deshabilitamos el estado actual
        //Está salvedad es porque los estados deben estar deshabilitados en el inspector
        if (estadoActual!=null) {
            estadoActual.enabled = false;
        }
        //Cambiamos el estado actual por el nuevo estado
        estadoActual = nuevoEstado;

        //Habilitamos el estado actual
        estadoActual.enabled = true;
    }

}
