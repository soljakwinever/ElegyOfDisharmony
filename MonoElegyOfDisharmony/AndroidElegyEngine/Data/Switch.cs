namespace EquestriEngine.Data
{
    public class Switch : Interfaces.IDataEntry
    {
        private bool _value;
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public bool Value
        {
            get { return _value; }
        }

        public void TurnOn()
        {
            _value = true;
            if(OnValueChange != null)
                OnValueChange(this, null);
        }

        public void TurnOff()
        {
            _value = false;
            if (OnValueChange != null)
                OnValueChange(this, null);
        }

        public void Toggle()
        {
            _value = !_value;
            if(OnValueChange != null)
                OnValueChange(this, null);
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", _name, _value);
        }

        public event GenericEvent OnValueChange;

        public void SaveData()
        {

        }
    }
}
