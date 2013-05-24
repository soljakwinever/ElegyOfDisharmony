namespace EquestriEngine.Data
{
    public class Variable : Interfaces.IDataEntry
    {
        private object _value;
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public object Value
        {
            get { return _value; }
            set
            {
                if (!(value is string ^ value is int))
                    throw new System.Exception("Invalid Data type");
                _value = value;
                if (OnValueChange != null)
                    OnValueChange(this, null);
            }
        }

        public int AsInt
        {
            get
            {
                try
                {
                    return ((int)_value);
                }
                catch
                {
                    return -1;
                }
            }
            set
            {
                _value = value;
                if (OnValueChange != null)
                    OnValueChange(this, null);
            }
        }

        public string AsString
        {
            get
            {
                return (string)_value;
            }
            set
            {
                _value = value;
                if (OnValueChange != null)
                    OnValueChange(this, null);
            }
        }

        public Variable(object input)
        {
            _value = input;
        }

        public event GenericEvent OnValueChange;

        public void SaveData()
        {

        }

        public bool EqualTo(object input)
        {
            if (!(input is string ^ input is int))
                throw new System.Exception("Invalid Data type");
            return input.ToString() == _value.ToString();
        }

        public bool GreaterThan(object input)
        {
            return (System.Convert.ToInt32(input) < System.Convert.ToInt32(_value));
        }

        public bool LessThan(object input)
        {
            if (!(input is int))
                throw new System.Exception("Invalid Data type");
            return System.Convert.ToInt32(input) > System.Convert.ToInt32(_value);
        }

        #region Operators

        public static Variable Set(Variable v1, int l)
        {
            if (v1.Value is int)
            {
                v1.AsInt = l;
            }
            else
            {
                v1.AsString = l.ToString();

            }
            return v1;
        }

        public static Variable Set(Variable v1, string l)
        {
            if (v1.Value is string)
            {
                v1.AsString = l;
            }
            else
            {
                throw new System.Exception("Invalid Conversion");
            }
            return v1;
        }

        public static Variable operator +(Variable v1, Variable v2)
        {
            if ((v1.Value is int && v2.Value is string) || (v1.Value is string && v2.Value is string))
                throw new System.Exception("Incompatible Types");
            else if (v1.Value is int && v2.Value is int)
            {
                v1.AsInt += v2.AsInt;
                return v1;
            }
            else
            {
                v1.AsString += v2.AsString;
                return v1;
            }
        }

        public static Variable operator -(Variable v1, Variable v2)
        {
            if ((v1.Value is string || v2.Value is string))
                throw new System.Exception("Incompatible Types");
            else
            {
                v1.AsInt -= v2.AsInt;
                return v1;
            }
        }

        public static Variable operator *(Variable v1, Variable v2)
        {
            if ((v1.Value is string || v2.Value is string))
                throw new System.Exception("Incompatible Types");
            else
            {
                v1.AsInt *= v2.AsInt;
                return v1;
            }
        }

        public static Variable operator /(Variable v1, Variable v2)
        {
            if ((v1.Value is string || v2.Value is string))
                throw new System.Exception("Incompatible Types");
            else
            {
                v1.AsInt = (int)((float)v1.AsInt / v2.AsInt);
                return v1;
            }
        }

        public static Variable operator %(Variable v1, Variable v2)
        {
            if ((v1.Value is string || v2.Value is string))
                throw new System.Exception("Incompatible Types");
            else
            {
                v1.AsInt = (int)((float)v1.AsInt % v2.AsInt);
                return v1;
            }
        }

        public static Variable operator +(Variable v1, int input)
        {
            if (v1.Value is string)
                throw new System.Exception("Incompatible Types");
            else
            {
                v1.AsInt += input;
                return v1;
            }
        }

        public static Variable operator -(Variable v1, int input)
        {
            if (v1.Value is string)
                throw new System.Exception("Incompatible Types");
            else
            {
                v1.AsInt -= input;
                return v1;
            }
        }

        public static Variable operator *(Variable v1, int input)
        {
            if (v1.Value is string)
                throw new System.Exception("Incompatible Types");
            else
            {
                v1.AsInt *= input;
                return v1;
            }
        }

        public static Variable operator /(Variable v1, int input)
        {
            if (v1.Value is string)
                throw new System.Exception("Incompatible Types");
            else
            {
                v1.AsInt /= input;
                return v1;
            }
        }

        public static Variable operator %(Variable v1, int input)
        {
            if (v1.Value is string)
                throw new System.Exception("Incompatible Types");
            else
            {
                v1.AsInt %= input;
                return v1;
            }
        }

        public static Variable operator +(Variable v1, string input)
        {
            if (v1.Value is int)
                throw new System.Exception("Incompatible Types");
            else
            {
                v1.AsString += input;
                return v1;
            }
        }

        public static Variable operator +(Variable v1, char input)
        {
            if (v1.Value is int)
                throw new System.Exception("Incompatible Types");
            else
            {
                v1.AsString += input;
                return v1;
            }
        }

        #endregion
    }
}
