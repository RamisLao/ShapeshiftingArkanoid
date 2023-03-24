using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPatternData : MonoBehaviour
{
    [SerializeField] private TextAsset patternsCsv;
    private List<List<string>> data = new List<List<string>>();
    public List<List<string>> Data => data;

    void Awake()
    {
        string fileData = patternsCsv.ToString();
        fileData = fileData.Replace("\r", string.Empty);
        string[] lines = fileData.Split("\n"[0]);
        List<string> pattern = new List<string>();

        foreach (string line in lines)
        {
            string[] splittedLine = line.Split(","[0]);
            if (splittedLine[0] == "")
            {
                data.Add(pattern);
                pattern = new List<string>();
                continue;
            }

            pattern.AddRange(splittedLine);
        }
    }
}
