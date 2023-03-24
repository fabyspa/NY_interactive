using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;
using System.Runtime.InteropServices;

namespace AirFishLab.ScrollingList.Demo
{
    public class GameObjectListBoxRiserva : ListBox
    {
        [SerializeField]
        private Text _name, _descr,_nameENG, _descrENG;
        [SerializeField]
        private Image _image;
        private Sprite tex;
        [SerializeField]
        private GameObject infos;

        string[] parole;
        
        protected override void UpdateDisplayContent(object content)
        {
            var dataWrapper = (VariableGameObjectListBankRiserva.DataWrapper) content;
            _name.text = dataWrapper.data.name;
            _descr.text = dataWrapper.data.descr;
            _image.sprite = UpdateImage(dataWrapper.data.name);
            _nameENG.text = dataWrapper.data.name_eng;
            _descrENG.text = dataWrapper.data.descr_eng;
            //regione,luogo,anno istituzione,superficie,reparto di competenza
            infos.transform.GetChild(0).GetComponentInChildren<Text>().text = dataWrapper.data.region;
            infos.transform.GetChild(1).GetComponentInChildren<Text>().text = dataWrapper.data.luogo;
            infos.transform.GetChild(2).GetComponentInChildren<Text>().text = dataWrapper.data.anno;
            infos.transform.GetChild(3).GetComponentInChildren<Text>().text = dataWrapper.data.sup;
            infos.transform.GetChild(4).GetComponentInChildren<Text>().text = dataWrapper.data.repC;
            parole = dataWrapper.data.repC.Split(" ");
            int[] indexToRemove = { 0, 1, 2 };
            Array.ForEach(indexToRemove, index => parole= parole.Where(val => Array.IndexOf(parole, val) != index ).ToArray());
            Debug.Log(string.Join(",", parole));
            infos.transform.GetChild(5).GetComponentInChildren<Text>().text = Stampa() + "Carabinieri biodiversity department";
        }
        public Sprite UpdateImage(string _name)
        {
            if (Resources.Load<Sprite>("Images/" + _name) != null)
            {
                tex = Resources.Load<Sprite>("Images/" + _name);
                return tex;
            }
            return null;
        }
        private string Stampa()
        {
            String new_string="";
            foreach (String p in parole)
            {
                 new_string = new String(p + " ");
            }
            return new_string;
        }
    }
       
}
