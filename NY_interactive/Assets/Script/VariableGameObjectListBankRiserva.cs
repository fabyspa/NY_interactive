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
                loadexcel.InstantiatePoints(loadexcel.ordenList);
            }
            //Debug.Log(type);
            else if (loadexcel.type.Contains(type))
            {
                //Debug.Log(_dataWrapper.data.transform.childCount);
                loadexcel.LoadRiservaByType(type);
                _contentsList.Clear();
                foreach (Riserva r in loadexcel.riservaDatabaseType)
                {
                    //_dataWrapper.data.transform.GetChild(0).GetComponent<Text>().text = r.name;
                    //_dataWrapper.data.transform.GetChild(1).GetComponent<Text>().text = r.descr;


                    _contentsList.Add(r);
                    //foreach (Transform child in _dataWrapper.data.transform)
                    //{
                    //    Debug.Log(child.GetComponent<Text>().text);
                    //}
                    //_contentInputField.text.Split(
                    //    new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                }
                loadexcel.InstantiatePoints(loadexcel.riservaDatabaseType);
               //Debug.Log(loadexcel.pointList.Count);
            }
           

            _contents = _contentsList.ToArray();
            _circularList.Refresh();

        }

        //disattiva il filtro per tipo se il primo filtro è parchi
        public void DeactivateTypeFilter()
        {

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
