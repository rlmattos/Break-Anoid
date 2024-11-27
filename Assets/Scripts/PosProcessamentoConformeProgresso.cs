using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PosProcessamentoConformeProgresso : MonoBehaviour
{
    [SerializeField] Volume volume;
    private void OnEnable()
    {
        Instanciador.blocoDestruido += AoDestruiBloco;
    }

    private void OnDisable()
    {
        Instanciador.blocoDestruido -= AoDestruiBloco;
    }

    private void Start()
    {
        volume.weight = 0;
    }

    void AoDestruiBloco(float progresso)
    {
        volume.weight = progresso;
    }
}
