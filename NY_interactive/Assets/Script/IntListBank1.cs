using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Mathematics;
using AirFishLab.ScrollingList;
// The bank for providing the content for the box to display
// Must be inherit from the class BaseListBank
public class IntListBank1 : BaseListBank
{
    Script_Prova a;
    //array contenente i nomi 
    [SerializeField]
    private string[] _contents;




    // This function will be invoked by the `CircularScrollingList`
    // when acquiring the content to display
    // The object returned will be converted to the type `object`
    // which will be converted back to its own type in `IntListBox.UpdateDisplayContent()`
    public override object GetListContent(int index)
    {
        _contents = a.Filter_prova();
        return _contents[index];
    }
    public override int GetListLength()
    {
        _contents = a.Filter_prova();
        return _contents.Length;
    }
}


