using UnityEngine;
using System.Collections;

public class PATableConfigReader : MonoBehaviour {

    public TextAsset asset;
    private string[][] PATable;

    void Start()
    {
        string[] lines = asset.text.Split('\n');
        PATable = new string[lines.Length][];
        for (int i = 0; i < lines.Length; i++) {
            string[] line = lines[i].Split(' ');
            PATable[i] = new string[line.Length];
            PATable[i] = line;
        }

        for (int i = 0; i < PATable.Length; i++) {
            string linea = "";
            for (int j = 0; j < PATable[i].Length; j++) {
                linea += PATable[i][j] + " ";
            }
            Debug.Log(linea);
        }
    }

    public string[][] getPATable()
    {
        return PATable;
    }

}
