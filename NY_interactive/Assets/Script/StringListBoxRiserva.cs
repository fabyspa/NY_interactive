using UnityEngine;
using UnityEngine.UI;

namespace AirFishLab.ScrollingList.Demo
{
    public class StringListBoxRiserva : ListBox
    {
        [SerializeField]
        private Text _text;

        protected override void UpdateDisplayContent(object content)
        {
            var dataWrapper = (VariableStringListBankRiserva.DataWrapper) content;
            _text.text = dataWrapper.data;
        }
    }
}
