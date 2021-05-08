using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnTetro : MonoBehaviour
{
    public Transform[] criaPecas;

    // Start is called before the first frame update
    void Start()
    {
        // intancia uma peça, localização: transforme.positiom; posição: padrão
        Instantiate(criaPecas[Random.Range(0, 7)], transform.position, Quaternion.identity);
    }
}
