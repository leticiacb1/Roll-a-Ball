using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{   
    //  ----- Constants -----
    public float speed = 0;
    private int totalPickUps = 178;
    private int count = 0;
    private int health = 3;

    //  ----- Scene Number -----
    private int END_GAME_SCENE = 2;
    private int WIN_GAME_SCENE = 3;
    
    //  ----- Texts -----
    public TextMeshProUGUI countText;
    public TextMeshProUGUI healthText;

    //  ----- Player variables -----
    private Rigidbody rb; 
    private float movementX;
    private float movementY;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 

        setCountText();
        setHealth();

    }

    void OnMove(InputValue movementValue){
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void setCountText(){
        countText.text = count.ToString() + " / " +  totalPickUps.ToString();

        if(count >= totalPickUps){
            // Win Screen
            SceneManager.LoadScene(WIN_GAME_SCENE);
        }
    }

    void setHealth(){
        healthText.text = health.ToString();

        if(health <= 0){
            //Lose Screen
            SceneManager.LoadScene(END_GAME_SCENE);
        }
    }

    void FixedUpdate() {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement*speed);
    }

    // Desativar objetos que o player collidir com
    // Para que isso não ocorra ocm o chão e com as paredes, adicionamos Tag aos objetos pegaveis
    private void OnTriggerEnter(Collider other){
        
        if(other.gameObject.CompareTag("PickUp")){
            other.gameObject.SetActive(false);
            count = count + 1;
            setCountText();
        }

        if(other.gameObject.CompareTag("Ghost")){
            
            // Jogador volta para a posição (0,0) do jogo
            // Os fantasmas voltam pra sua posição de respaw original

            health = health - 1;
            setHealth();
        }
    }
}
