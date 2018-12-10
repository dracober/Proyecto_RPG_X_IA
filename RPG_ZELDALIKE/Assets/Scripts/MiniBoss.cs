using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : MonoBehaviour {

    [Header("Mini-Boss Health Settings")]
    public int MaxHealth = 100;
    public int CurrentHealth = 0;
    [Header("Mini-Boss Speed Settings")]
    public float Speed = 2.5f;
    public float Attackspd = 3f;
    [Header("Mini-Boss Power Stance Settings")]
    public int MaxMana = 100;
    public int CurrentMana = 0;
    public int Magica = 50; //Magic DMG
    public bool inmunity;
    public float VisionRange = 4.5f;
    public float attackRadius = 3.5f;
    public bool OnRange = true;
    public bool attacking;
    // 1.- Fire Ball 2.- Meteor Mash 3.- Darkness Sword 4.- Heal
    public int SpellAtk = 0;

    Animator anim;

    // Variable para guardar al jugador
    GameObject player;

    // Variable para guardar la posición inicial
    Vector3 initialPosition, target;

    Rigidbody2D rb2d;
    // Use this for initialization
    void Start() {

        // Recuperamos al jugador gracias al Tag
        player = GameObject.FindGameObjectWithTag("Player");

        // Por defecto nuestro target siempre será nuestra posición inicial
        target = initialPosition;

        // Guardamos nuestra posición inicial
        initialPosition = transform.position;

        rb2d = GetComponent<Rigidbody2D>();

        StartCoroutine(SpellAttack());
    }

    // Update is called once per frame
    void Update() {
        // Por defecto nuestro target siempre será nuestra posición inicial
        target = initialPosition;

        // Comprobamos un Raycast del enemigo hasta el jugador
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            player.transform.position - transform.position,
            VisionRange,
            1 << LayerMask.NameToLayer("Default")
        // Poner el propio Enemy en una layer distinta a Default para evitar el raycast
        // También poner al objeto Attack y al Prefab Slash una Layer Attack 
        // Sino los detectará como entorno y se mueve atrás al hacer ataques
        );

        // Aquí podemos debugear el Raycast
        Vector3 forward = transform.TransformDirection(player.transform.position - transform.position);
        Debug.DrawRay(transform.position, forward, Color.red);

        // Si el Raycast encuentra al jugador lo ponemos de target
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player")
            {
                target = player.transform.position;
            }
        }

        // Calculamos la distancia y dirección actual hasta el target
        float distance = Vector3.Distance(target, transform.position);
        Vector3 dir = (target - transform.position).normalized;

        // Si es el enemigo y está en rango de ataque nos paramos y le atacamos
        if (target != initialPosition && distance < attackRadius)
        {
            // Aquí le atacaríamos, pero por ahora simplemente cambiamos la animación
            anim.SetFloat("movX", dir.x);
            anim.SetFloat("movY", dir.y);
            anim.Play("Enemy_Walk", -1, 0);  // Congela la animación de andar

            ///-- Empezamos a atacar (importante una Layer en ataque para evitar Raycast)
            if (!attacking) StartCoroutine(SpellAttack);
        }
        // En caso contrario nos movemos hacia él
        else
        {
            rb2d.MovePosition(transform.position + dir * Speed * Time.deltaTime);

            // Al movernos establecemos la animación de movimiento
            anim.speed = 1;
            anim.SetFloat("movX", dir.x);
            anim.SetFloat("movY", dir.y);
            anim.SetBool("walking", true);
        }

        // Una última comprobación para evitar bugs forzando la posición inicial
        if (target == initialPosition && distance < 0.05f)
        {
            transform.position = initialPosition;
            // Y cambiamos la animación de nuevo a Idle
            anim.SetBool("walking", false);
        }

        // Y un debug optativo con una línea hasta el target
        Debug.DrawLine(transform.position, target, Color.green);
        ResetHealth();
    }

    void movement() { }
    void Attack() { }
    void Follow() { }
    void Idle() { }
    IEnumerator SpellAttack()
    {
        while (OnRange == true){
            int randomSpell = Random.Range(1, 5);
            int spell = randomSpell;

            switch (spell)
            {
                case 1:
                    Debug.Log("Fire Ball");
                    break;
                case 2:
                    Debug.Log("Meteor Mash");
                    break;
                case 3:
                    Debug.Log("Darkness Sword");
                    break;
                case 4:
                    Debug.Log("Heal");
                    if (CurrentHealth != MaxHealth)
                    {
                        CurrentHealth += 40;
                    }
                    break;
            }
            yield return new WaitForSeconds(Attackspd);
        }
    }
    void InmuneStatus() { }
    void ResetHealth()
    {
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }

    IEnumerator Attack(float seconds)
    {
        attacking = true;  // Activamos la bandera
        // Si tenemos objetivo y el prefab es correcto creamos la roca
        if (target != initialPosition /*&& rockPrefab != null*/)
        {
            //Instantiate(rockPrefab, transform.position, transform.rotation);
            // Esperamos los segundos de turno antes de hacer otro ataque
            yield return new WaitForSeconds(seconds);
        }
        attacking = false; // Desactivamos la bandera
    }
}
