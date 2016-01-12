using UnityEngine;
using System.Collections;

public class MoveSecuenceConfigReader : MonoBehaviour {

    public TextAsset asset; 
    private string[] secuence;

    void Start()
    {
        secuence = asset.text.Split(' ');
    }

    public string[] getSecuence() {
        return secuence;
    }
}
