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
        if (x < GraphPoints[0] || x > GraphPoints[3])
            return 0;

        if (GraphPoints[0] == GraphPoints[1] && x <= GraphPoints[1])
            return 1;

        if (GraphPoints[2] == GraphPoints[3] && x >= GraphPoints[2])
            return 1;

        if (x > GraphPoints[1] && x < GraphPoints[2])
            return 1;

        if (x <= GraphPoints[1])
            return (x - GraphPoints[0]) / (GraphPoints[1] - GraphPoints[0]);

        return (x - GraphPoints[2]) / (GraphPoints[3] - GraphPoints[2]);
    }
}

