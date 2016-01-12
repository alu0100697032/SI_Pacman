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
    }

    public string[][] getPATable()
    {
        return PATable;
    }

}
