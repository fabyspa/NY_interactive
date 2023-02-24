using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace AirFishLab.ScrollingList
{
    public class VariableGameObjectListBankRiserva : BaseListBank
    {

        LoadExcel loadexcel;
        //[SerializeField]
        //private InputField _contentInputField;
        private List<Riserva> _contentsList = new List<Riserva>();
        public Riserva[] _contents;
        [SerializeField]
        private CircularScrollingListRiserva _circularList;
        [SerializeField]
        private GameObject gameobjectToClone;
        
        //[SerializeField]
        //private CircularScrollingList _thirdCircular;
        // [SerializeField]
        // private CircularScrollingList _linearList;

        private readonly DataWrapper _dataWrapper = new DataWrapper();

        /// <summary>
        /// Extract the contents from the input field and refresh the list
        /// </summary>
       

        public override object GetListContent(int index)
        {
            _dataWrapper.data = _contents[index];
            return _dataWrapper;
        }

        public override int GetListLength()
        {
            return _contents.Length;
        }

        public void ChangeInfoContents(string type)
        {
            loadexcel = GameObject.FindObjectOfType<LoadExcel>();
            if (type == "Tutte")
            {
                _contentsList.Clear();
                foreach (Riserva r in loadexcel.ordenList)
                {
                    _contentsList.Add(r);
                }
                loadexcel.riservaDatabaseType.Clear();
                loadexcel.riservaDatabaseType.AddRange(loadexcel.ordenList);
               loadexcel.AddState();

                loadexcel.InstantiatePoints(loadexcel.ordenList);
            }
            else if (loadexcel.type.Contains(type))
            {
                loadexcel.LoadRiservaByType(type);
                _contentsList.Clear();
                foreach (Riserva r in loadexcel.riservaDatabaseType)
                { 
                    _contentsList.Add(r);
                }
                loadexcel.AddState();

                loadexcel.InstantiatePoints(loadexcel.riservaDatabase);

                //Debug.Log(loadexcel.pointList.Count);
            }
            _contents = _contentsList.ToArray();
            _circularList.Refresh();
            GetCenterItem();
        }

        public Riserva GetCenterItem()
        {
            int size = this.transform.childCount;
            GameObject obj = this.transform.GetChild(size - 1).gameObject;
            //Debug.Log(obj.GetComponentInChildren<Text>().text);
            foreach (Riserva r in loadexcel.riservaDatabase)
            {
                if(r.name== obj.GetComponentInChildren<Text>().text)
                {
                    //Debug.Log(r.name);
                    return r;
                }
            }
            return null;
        }
        /// <summary>
        /// Used for carry the data of value type to avoid boxing/unboxing
        /// </summary>
        public class DataWrapper
        {
            public Riserva data;
        }

        
    }
}
