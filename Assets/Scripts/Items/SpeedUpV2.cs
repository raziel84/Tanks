using UnityEngine;
using System.Collections;
using Complete;

public class SpeedUpV2 : MonoBehaviour {

    public float multiplicador = 1.2f;          //factor por el que va a multiplicar la velocidad del tanque
    private string playerTag = "Player";        //para comprobar si el collider que dispara el trigger es un jugador.
    private Complete.TankMovement movTanque;    //referencia al script TankMovement
    private bool colision = false;              //variable de control, para comprabar si hubo colision
    private float timer = 1;                    //tiempo de duración del objeto, se inicializa en 1 para evitar que se destruya solo

    void Update()
    {
        if (colision)                   //si hay colision
            timer -= Time.deltaTime;    //comienza a reducir la variable timer

        if (timer <= 0)                 //una vez que timer llega a 0
            Reseteo();                  //ejecuta la funcion Reseteo

        //Si hubo colision y se modifico el bonusSpeed significa que el PJ tomo un ítem con otro efecto
        if (colision && movTanque.bonusSpeed != multiplicador)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider tank)
    {
        if (tank.tag != playerTag)      //si el tag del del collider "tank" no es igual al playerTag, no ejecuta nada
            return;            
       
        gameObject.GetComponent<MeshRenderer>().enabled = false;            //desactiva el Renderer del objeto para evitar que se vea
        gameObject.GetComponent<Collider>().enabled = false;                //desactivamos el collider para evitar colisiones
        transform.SetParent(tank.transform);                                //coloca el objeto como hijo del tanque
        movTanque = tank.gameObject.GetComponent<Complete.TankMovement>();  //recupera el script TankMovement del collider

        //Comprobamos que el player tenga el script asignado para evitar error NullReference
        if (movTanque == null)
        {
            Debug.Log("El PJ no tiene el script");
            return;
        }

        if (movTanque.bonusSpeed == multiplicador)                      //Si se cumple significa que el PJ ya cogio una copia de este ítem
        {            
            transform.SetParent(null);                                  //quita el objeto de la jerarquia del tanque
            gameObject.GetComponent<MeshRenderer>().enabled = true;     //activa el Renderer del objeto
            gameObject.GetComponent<Collider>().enabled = true;         //activamos el collider
            return;                                                     //Salimos del Trigger
        }

        movTanque.bonusSpeed = multiplicador;                           //aplica el nuevo coeficiente de velocidad                           
        
        colision = true;                                                //setea la variable a true para que comience a disminuir el timer en Update
        timer = 5f;                                                     //setea el valor del timer
        
    }

    void Reseteo()
    {
        movTanque.bonusSpeed = 1;           //restaura el coeficiente de velocidad
        transform.SetParent(null);          //quita el objeto de la jerarquia del tanque
        Destroy(gameObject);                //se destruye el objeto
    }
}
