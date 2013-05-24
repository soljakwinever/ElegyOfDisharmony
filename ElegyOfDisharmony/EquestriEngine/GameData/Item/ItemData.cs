using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquestriEngine.GameData.Item
{
    public class ItemData
    {
        private string _name;
        private string _description;
        private int _cost;

        private bool _equipable;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public ItemData(string name, string description, bool equipable)
        {
            _name = name;
            _description = description;
            _equipable = equipable;
        }
    }
}
