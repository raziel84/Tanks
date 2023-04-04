using UnityEngine;
using System.Collections;

public class BotiquinRegeneradorV2 : MonoBehaviour
{

    //Este script debe colocarse sobre un objeto que actúe como un item del juego
    //Cuando el personaje con la etiqueta Player toca el objeto recuperará vida por segundo
    //hasta que se cumpla el tiempo establecido en la variable duracion
    //Está segunda versión permite recuperar vida de forma más precisa que la V1

    private string tagJugador = "Player";   //Quienes tengan está etiqueta podrán tomar el gameobject que contenga este script
    public float vidaCurar = 10f;           //Cantidad de vida a recuperar por segundo
    private Complete.TankHealth tankHealth; //Guarda la referencia al script del pj que contiene las variables de vida
    private float tiempoTranscurrido = 0;   //Para guardar la cantidad de tiempo que a transcurrido
    private float duracion = 1;             //Tiempo que durara el efecto. Se inicia a 1 para evitar que el objeto sea destruido
    private bool isPlayer = false;          //Se volverá a true cuando el gameobject que entre al trigger tenga la etiqueta Player

    private void Update()
    {
        //Si lo que toco el trigger tiene la etiqueta Player empezaremos a sumar el deltaTime
        if (isPlayer)
        {
            tiempoTranscurrido = tiempoTranscurrido + Time.deltaTime;
        }
        //Controlamos que la cantidad de tiempo transcurrido sea aproximadamente 1 s
        if (tiempoTranscurrido > 0.9f && tiempoTranscurrido < 1.1f)
        {
            //Llamamos al método Curación y le pasamos la cantidad de vida a recuperar
            tankHealth.Curacion(vidaCurar);
            //Volvemos a cero el contador de tiempo
            tiempoTranscurrido = 0;
            //Restamos 1 s del tiempo que debe durar el efecto
            duracion--;
        }
        //Cuando haya pasado el tiempo que debe durar el efecto destruimos el gameobject
        if (duracion == 0)
        {
            DestruirObjeto();
        }
    }

    void OnTriggerEnter(Collider info)
    {
        //Si lo que entra al Trigger no es un Player salimos del método
        if (info.tag != tagJugador) return;

        //Hacemos hijo del Player el gameobject con este script
        transform.SetParent(info.transform);
        //Desactivamos el renderer para que no se muestre en la escena
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        //Desactivamos el collider para evitar colisiones
        gameObject.GetComponent<Collider>().enabled = false;
        //Guardamos la referencia al script que contiene las variables de vida
        tankHealth = info.gameObject.GetComponent<Complete.TankHealth>();

        //Comprobamos que el player tenga el script asignado para evitar error NullReference
        if (tankHealth == null)
        {
            Debug.Log("El PJ no tiene el script");
            return;
        }

        //Indicamos cuanto va a durar el efecto
        duracion = 5f;
        //Volvemos a true la variable
        isPlayer = true;

    }

    private void DestruirObjeto()
    {
        //El gameobject deja de ser hijo del Player
        transform.SetParent(null);
        //Destruimos el gameobject
        Destroy(gameObject);
    }
}
