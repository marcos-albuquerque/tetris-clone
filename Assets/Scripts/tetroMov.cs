using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tetroMov : MonoBehaviour
{
    public bool podeRodar; // a peça pode rodar?
    public bool roda360;

    public float queda;

    public float velocidade;
    public float timer;

    gameManager gManager;
    spawnTetro gSpawner;

    // Start is called before the first frame update
    void Start()
    {
        // o timer começa a contar a partir do valor
        // que dermos para velocidade
        timer = velocidade;

        gManager = GameObject.FindObjectOfType<gameManager>();
        gSpawner = GameObject.FindObjectOfType<spawnTetro>();
    }

    // Update is called once per frame
    void Update()
    {
        // quando o jogador soltar uma tecla
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.DownArrow))
            timer = velocidade;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            timer += Time.deltaTime; // o timer faz um acréssimo numeral

            if (timer > velocidade) {
                transform.position += new Vector3(1, 0, 0);
                timer = 0;
            }

            if (posicaoValida())
            {
                gManager.atualizaGrade(this);
            }
            else
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            timer += Time.deltaTime;

            if (timer > velocidade)
            {
                transform.position += new Vector3(-1, 0, 0);
                timer = 0;
            }            

            if (posicaoValida())
            {
                gManager.atualizaGrade(this);
            }
            else
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }

        if (Input.GetKey(KeyCode.DownArrow)) // || Time.time - queda >= 1)
        {
            timer += Time.deltaTime;

            if (timer > velocidade)
            {
                transform.position += new Vector3(0, -1, 0);
                timer = 0;
            }

            if (posicaoValida())
            {
                gManager.atualizaGrade(this);
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);
                gManager.apagaLinha();
                gManager.score += 10;
                enabled = false; // desabilita a peça
                gSpawner.proximaPeca();
            }

            // queda = Time.time;
        }

        if (Time.time - queda >= 1 && !Input.GetKeyUp(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0, -1, 0);

            if (posicaoValida())
            {
                gManager.atualizaGrade(this);
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);
                gManager.apagaLinha();
                gManager.score += 10;
                enabled = false;
                gSpawner.proximaPeca();
            }

            queda = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            checaRoda();
        }
    }

    void checaRoda()
    {
        if (podeRodar)
        {
            if (!roda360) // se não pode girar em 360°
            {
                // limite apenas a 90 e -90

                // verifica se a rotação está entre 0 e -90
                if (transform.rotation.z < 0)
                {
                    transform.Rotate(0, 0, 90);

                    if (posicaoValida())
                    {
                        gManager.atualizaGrade(this);
                    }
                    else
                    {
                        transform.Rotate(0, 0, -90);
                    }
                }
                else
                {
                    transform.Rotate(0, 0, -90);

                    if (posicaoValida())
                    {
                        gManager.atualizaGrade(this);
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);
                    }
                }
            }
            else // roda livremente sem restrição
            {
                transform.Rotate(0, 0, -90);

                if (posicaoValida())
                {
                    gManager.atualizaGrade(this);
                }
                else
                {
                    transform.Rotate(0, 0, 90);
                }
            }
        }
    }

    bool posicaoValida()
    {
        // child é o nome de cada peça
        foreach (Transform child in transform)
        {
            // passa a posição de cada bloco de uma peça
            Vector2 posBloco = gManager.arredonda(child.position);

            // se o quadrado não estiver dentro da grade
            if(gManager.dentroGrade(posBloco) == false)
            {
                return false;
            }

            // se tiver um bloco presente
            if (gManager.posicaoTransformGrade(posBloco) != null && gManager.posicaoTransformGrade(posBloco).parent != transform)
            {
                return false;
            }
        }

        return true;
    }
}
