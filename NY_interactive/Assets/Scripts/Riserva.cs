using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Riserva
{
    public string type;
    public string name;
    public string coord;
    public string descr;
    public string state;
    public Sprite sprite;
    // Update is called once per frame
    public Riserva( Riserva r)
    {
        type = r.type;
        name = r.name;
        coord = r.coord;
        descr = r.descr;
        sprite= r.sprite;
        state = r.state;
    }
}
