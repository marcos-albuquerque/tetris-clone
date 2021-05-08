using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    // dimensões da grade
    public static int altura = 20;
    public static int largura = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
