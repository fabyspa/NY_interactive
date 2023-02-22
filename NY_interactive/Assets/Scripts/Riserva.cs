using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Riserva
{
    public string type;
    public string name;
    public string coord;
    public string descr;
    public string state;

    // Update is called once per frame
    public Riserva( Riserva r)
    {
        type = r.type;
        name = r.name;
        coord = r.coord;
        descr = r.descr;
        state = r.state;
    }
}
