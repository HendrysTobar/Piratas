using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controlador : MonoBehaviour {

    #region Atributos
    /// <summary>
    ///  velocidad de movimiento
    /// </summary>
    public float velocidad;
    public float velocidadRotacion;
    public float velocidadBala;
    public GameObject balaPrefab;
    public GameObject explosionPrefab;
    public GameObject salidaBala;
    protected int puntosDeDano;
    protected SpriteRenderer spriteRenderer;
    #endregion

    #region Funciones de evento
    /// <summary>
    /// Se invoca en el primer frame cuando el script se habilita, justo antes que las otras funciones de evento se llamen por primera vez
    /// </summary>
    public virtual void Start()
    {
        // Se obtiene la referencia del componente SpriteRenderer dentro de unos de los hijos de este objeto de juego
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    /// <summary>
     /// Se encarga de verificar cuando este barco ha sido colisionado por una bala
     /// </summary>
     /// <param name="collision">colisionador</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // si el objeto que colisiona tiene el tag: Bala
        if (collision.collider.CompareTag("Bala"))
        {
            // Destruye dicho objeto
            Destroy(collision.gameObject);
            // Llama el mñetodo ReflejarDaño
            ReflejarDano();
        }
    }
    #endregion

    #region Métodos
    /// <summary>
    /// Se encarga de mover de manera líneal este objeto de juego desde su posición hasta un punto final
    /// </summary>
    /// <param name="posicionDestino">Posicion destino.</param>
    public void MoverBarco(Vector2 posicionDestino) { transform.position = Vector2.MoveTowards(transform.position, posicionDestino, velocidad * Time.deltaTime); }

    /// <summary>
    /// Se encarga de incrementar los puntos de daño recibidos
    /// y de crear una instancia de la explosión
    /// </summary>
    public virtual void ReflejarDano()
    {
        puntosDeDano++;
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }

    /// <summary>
    /// Se encarga de verificar si este objeto de juego recibio el daño suficiente para ser destruido
    /// </summary>
    /// <returns><c>true</c>, si los puntos de daño son mayores o igual a 4, <c>false</c> si los puntos de daño son menores a 4.</returns>
    public bool VerificarEstadoDanoActual()
    {
        if (puntosDeDano >= 4)
        {
            // si los puntos de daño son mayores o iguales a 4 remuevo este objeto de juego de la lista en la clase Manager
            ManejadorJuego.Instance.RemoverBarcoEnemigoDeLista(this.gameObject);
            // Luego destruyo este objeto de juego
            Destroy(this.gameObject);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Se encarga de rotar el barco hacía donde se indique como parámetro
    /// </summary>
    /// <param name="posicionObjetivo">Posicion objetivo.</param>
    public void RotarBarco(Vector2 posicionObjetivo)
    {
        Vector2 vectorToTarget = posicionObjetivo - (Vector2)transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * velocidadRotacion);
    }

    /// <summary>
    /// Se encarga de crear ns instancia de la bala a disparar y de agregarle una fuerza para dar el efecto de ser una bala disparada desde un cañon
    /// </summary>
    public void Disparar()
    {
        Rigidbody2D balaDisparada = Instantiate(balaPrefab, salidaBala.transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        balaDisparada.AddForce(salidaBala.transform.right * velocidadBala, ForceMode2D.Force);
    }
    #endregion
}
