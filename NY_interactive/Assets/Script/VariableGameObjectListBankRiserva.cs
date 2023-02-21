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
        private List<GameObject> _contentsList = new List<GameObject>();
        public GameObject[] _contents;
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

            //Debug.Log(type);
            if (loadexcel.type.Contains(type))
            {
                loadexcel.LoadRiservaByType(type);
                _contentsList.Clear();
                foreach (Riserva r in loadexcel.riservaDatabaseType)
                {
                    gameobjectToClone.transform.Find("Nome").GetComponent<TextMeshPro>().text = r.name;
                    gameobjectToClone.transform.Find("Descr").GetComponent<TextMeshPro>().text = r.descr;

                    _contentsList.Add(gameobjectToClone);
                    //_contentInputField.text.Split(
                    //    new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                }
                loadexcel.InstantiatePoints(loadexcel.riservaDatabaseType);

                _contents = _contentsList.ToArray();
                _circularList.Refresh();

            }


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
            public GameObject data;
        }

        
    }
}
