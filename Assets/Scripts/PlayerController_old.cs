using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_old : MonoBehaviour
{
    
    private KeyboardManager inputArduino;
    [Range(0,0), SerializeField, Tooltip("Velocidad actual del personaje")]
    public float speed = 10.0f;
    
    [Range(0, 25), SerializeField, Tooltip("Velocidad lineal máxima de la persona")]
    private float turnSpeed = 45f;
    
    // Start is called before the first frame update
    void Start()
    {
        inputArduino = GameObject.FindWithTag("KeyboardController").GetComponent<KeyboardManager>();
    }

    // Update is called once per frame
    void Update()
    {
            
        if (inputArduino.running)
        {
            GetComponent<Animator>().SetBool("Moverse", true);
            transform.Translate(Vector3.forward*speed*Time.deltaTime);
        }
        else
        {
            GetComponent<Animator>().SetBool("Moverse",false);
        }
    }
}