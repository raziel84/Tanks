using UnityEngine;

public class DisparoSimple : TankShootingIA
{
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }    

    protected override void Fire() {
        // Set the fired flag so only Fire is only called once.
        m_Fired = true;

        // Create an instance of the shell and store a reference to it's rigidbody.
        Rigidbody shellInstance =
            Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

        //Llamamos a CalculoSize para asignar el tamaño de la bala
        shellInstance.transform.localScale = CalculoSize();

        //Guardamos la referencia al script ShellExplosion de la bala instanciada
        Complete.ShellExplosion shellExplotion = shellInstance.GetComponent<Complete.ShellExplosion>();

        //Asignamos el bonus de daño
        shellExplotion.bonusDamage = bonusDamage;

        // Set the shell's velocity to the launch force in the fire position's forward direction.
        shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

        // Change the clip to the firing clip and play it.
        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();

        // Reset the launch force.  This is a precaution in case of missing button events.
        m_CurrentLaunchForce = m_MinLaunchForce;
        shoot = false;
    }
}