using UnityEngine;
using System.Collections;
using Complete;

public class SpeedDownV1 : MonoBehaviour {

    //Requiere una variable bool llamada bonusSpeed en el PJ

    public float multiplicador = 0.8f;          //factor por el que va a multiplicar la velocidad del tanque
    private string playerTag = "Player";        //para comprobar si el collider que dispara el trigger es un jugador.
    private Complete.TankMovement movTanque;    //referencia al script TankHealth
    public float movOriginal = 0;               //variable que guarda la velocidad original del tanque
    private bool colision = false;              //variable de control, par comprabar si hubo colision
    private float timer = 1;                    //tiempo de duración del objeto, se inicializa en 1 para evitar que se destruya solo

    void Update()
    {
        if (colision)                   //si hay colision
            timer -= Time.deltaTime;    //comienza a reducir la variable timer
        if (timer <= 0)                 //una vez que timer llega a 0
            Reseteo();                  //ejecuta la funcion Reseteo
    }

    void OnTriggerEnter(Collider tank)
    {
        if (tank.tag != playerTag)      //si no el tag del del collider "tank" es igual al playerTag, no ejecuta nada
            return;

        transform.SetParent(tank.transform);                        //coloca el objeto como hijo del tanque
        gameObject.GetComponent<MeshRenderer>().enabled = false;    //desactiva el Renderer del objeto para evitar colisiones

        movTanque = tank.gameObject.GetComponent<Complete.TankMovement>();  //recupera el script TankMovement del collider

        /*if (movTanque.bonusSpeed)                   //Si esta variable es true, quiere decir que ya tenemos un buff de velocidad
        {
            Destroy(gameObject);                //por lo que destruimos el objeto para evitar seguir sumando nuevos buffs
            return;
        }*/

        movOriginal = movTanque.m_Speed;                                    //guarda el valor original de velocidad del tanque
        movTanque.m_Speed *= multiplicador;                                 //aplica el multiplicador a la velocidad del tanque                            
        //movTanque.bonusSpeed = true;

        colision = true;                    //setea la variable a true para que comience a disminuir el timer en Update
        timer = 5f;                         //setea el valor del timer

    }

    void Reseteo()
    {
        movTanque.m_Speed = movOriginal;    //restaura la velocidad original del tanque
        //movTanque.bonusSpeed = false;
        transform.SetParent(null);          //quita el objeto de la jerarquia del tanque
        Destroy(gameObject);                //se destruye el objeto
    }
}
