using Assets.Scripts.Behaviour.Fuzzy_Logic;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Fuzzy_Sets
{
    private FuzzySets _walkingRules;

    public Fuzzy_Sets()
    {
        _walkingRules = JsonHandler<FuzzySets>.GetFromJson("FuzzySets.json");
    }

    public Dictionary<string, float> CalculateValues(float[] xValues)
    {
        var d = new Dictionary<string, float>();
        int i = 0;

        foreach (var x in xValues)
        {
            var a = _walkingRules.Antecedents[i];

            foreach (var av in a.AntecedentValues)
                d.Add($"{a.Name}_{av.Name}", av.GetYValue(x));
            i++;
        }
        return d;
    }

    public List<List<List<long>>> GetGraphPoints()
    {
        var list = new List<List<List<long>>>();

        foreach(var a in _walkingRules.Antecedents)
        {
            var listList = new List<List<long>>();
            foreach(var av in a.AntecedentValues)            
                listList.Add(av.GraphPoints);
            list.Add(listList);
        }
        return list;
    }
}

[Serializable]
public partial class FuzzySets
{
    public List<Antecedent> Antecedents;
}

[Serializable]
public partial class Antecedent
{
    public string Name;
    public List<AntecedentValue> AntecedentValues;

}

[Serializable]
public partial class AntecedentValue
{
    public string Name;
    public List<long> GraphPoints;

    public float GetYValue(float x)
    {
        //Outside of graph
        if (x < GraphPoints[0] || x > GraphPoints[3])
            return 0;

        //Right Shoulder
        if (GraphPoints[0] == GraphPoints[1] && x <= GraphPoints[1])
            return 1;

        //Left Shoulder
        if (GraphPoints[2] == GraphPoints[3] && x >= GraphPoints[2])
            return 1;

        //Middle
        if (x >= GraphPoints[1] && x <= GraphPoints[2])
            return 1;

        //Left Triangle
        if (x <= GraphPoints[1])
            return (x - GraphPoints[0]) / (GraphPoints[1] - GraphPoints[0]);

        //Right Triangle
        return (x - GraphPoints[3]) / (GraphPoints[2] - GraphPoints[3]);
    }
}

