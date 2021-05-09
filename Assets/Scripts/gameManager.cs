using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    // dimensões da grade
    public static int altura = 20;
    public static int largura = 10;

    public int score = 0;
    public Text textoScore;

    public static Transform[,] grade = new Transform[largura, altura];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textoScore.text = score.ToString();
    }

    public bool dentroGrade(Vector2 posicao)
    {
        return ( (int)posicao.x >= 0 && (int)posicao.x < largura && (int)posicao.y >= 0 );
    }

                                // nº arredondado 
    public Vector2 arredonda(Vector2 nA)
    {
        return new Vector2(Mathf.Round(nA.x), Mathf.Round(nA.y));
    }

    public void atualizaGrade(tetroMov pecaTetris)
    {
        for (int y = 0; y < altura; y++)
        {
            for (int x = 0; x < largura; x++)
            {
                // se houver uma peça
                if (grade[x,y] != null)
                {
                    // verifica se é a peça que está sendo controlada
                    if (grade[x,y].parent == pecaTetris.transform)
                    {
                        // atribui null a posição da peça
                        grade[x,y] = null;
                    }
                }
            }
        }

        foreach (Transform peca in pecaTetris.transform)
        {
            Vector2 posicao = arredonda(peca.position);

            // se estier dentro
            if(posicao.y < altura)
            {
                grade[(int)posicao.x, (int)posicao.y] = peca;
            }
        }
    }

    public Transform posicaoTransformGrade(Vector2 posicao)
    {
        // se a posição estiver acima da altura
        if(posicao.y > altura - 1)
        {
            return null;
        }
        else // se estiver abaixo da altura
        {
            // retorna posição para ser tratada
            return grade[(int)posicao.x, (int)posicao.y];
        }
    }

    public bool linhaCheia(int y)
    {
        for (int x = 0; x < largura; x++)
        {
            if (grade[x, y] == null)
            {
                return false;
            }
        }

        return true;
    }

    public void deletaQuadrado(int y)
    {
        for (int x = 0; x < largura; x++)
        {
            Destroy(grade[x,y].gameObject); // destroi quadrado

            grade[x,y] = null; // atualiza grade
        }
    }

    public void moveLinhaBaixo(int y)
    {
        for (int x = 0; x < largura; x++)
        {
            // verifica se tem objeto para ser
            // movido para baixo
            if (grade[x,y] != null)
            {
                grade[x, y-1] = grade[x, y];
                grade[x, y] = null;

                grade[x, y-1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public void moveTodasLinhasBaixo(int y)
    {
        for (int i = y; i < altura; i++)
        {
            moveLinhaBaixo(i);
        }
    }

    public void apagaLinha()
    {
        for (int y = 0; y < altura; y++)
        {
            if (linhaCheia(y))
            {
                deletaQuadrado(y);
                moveTodasLinhasBaixo(y+1);
                y--;
                score += 100;
            }
        }
    }
}
