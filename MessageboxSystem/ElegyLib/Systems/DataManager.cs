using GameComponent = Microsoft.Xna.Framework.GameComponent;
using Game = Microsoft.Xna.Framework.Game;
using EquestriEngine.Data;
using EquestriEngine.Data.Collections;

namespace EquestriEngine.Systems
{

    public delegate void StringChanged(object sender, string input);
    public class DataManager : GameComponent
    {
        private static SwitchCollection _switches = null;
        private static VariableCollection _variables = null;
        Achievement[] achievements;

        static Variable
            PlayerName,
            PlayerGold,
            PlayerSteps;

        public DataManager(Game game)
            : base(game)
        {
            _switches = new SwitchCollection();
            _variables = new VariableCollection();

            _switches["Achievement Test"] = new Switch()
            {
                Name = "Achievement Test"
            };
            _switches["switch_ponies"] = new Switch()
            {
                Name = "Multi Switch Check Test"
            };

            _switches["Achievement Test"].TurnOn();
            PlayerName = new Variable("Pinkie Pie");
            PlayerGold = new Variable(1000);
            PlayerSteps = new Variable(0);
            PlayerName.Value = "Pinkie Pie";
            _variables["{steps_taken}"] = PlayerSteps;
            _variables["{profilename}"] = PlayerName;
            _variables["{gold}"] = PlayerGold;
        }

        public override void Initialize()
        {
            base.Initialize();

            using (System.IO.Stream stream =
                System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("EquestriEngine.Resources.Data.Achievements.xml"))
            //System.IO.File.OpenRead(@"writeout.xml"))
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(Achievement[]));
                achievements = (Achievement[])serializer.Deserialize(stream);
            }

            foreach (Achievement ach in achievements)
            {
                if (ach.DataName == null)
                    continue;
                string[] temp = ach.DataName.Split(';');
                try
                {
                    switch (temp[0])
                    {
                        case "swt":
                            if (!_switches.ContainsKey(temp[1]))
                                _switches[temp[1]] = new Switch()
                                    {
                                        Name = temp[1]
                                    };
                            ach.RegisterData(_switches[temp[1]]);
                            break;
                        case "var":
                            ach.RegisterData(_variables[temp[1]]);
                            break;
                    }
                    ConsoleWindow.WriteLine("Successfully added Achievement - {0} - {1}", ach.Name, ach.Description);
                }
                catch
                {
                    ConsoleWindow.WriteLine("Warning - Adding Achievement failed: {0} - data missing", ach.Name);
                }
            }

            PlayerGold.Value = 0;

            ConsoleWindow.WriteLine("");
        }

        public static string PrintSwitches(int page)
        {
            string temp = "--Switch List--\n";
            const string format = "Name({0}) - Value({1})\n";
            foreach (var p in _switches.Values)
            {
                temp += string.Format(format, p.Name, p.Value);
            }

            return temp;
        }

        public static string PrintVariables(int page)
        {
            string temp = "--Variable List--\n";
            const string format = "Name({0}) - Value({1})\n";
            foreach (var v in _variables)
            {
                temp += string.Format(format, v.Key, v.Value.Value);
            }

            return temp;
        }

        public static Variable GetVariable(string name)
        {
            try
            {
                return _variables[name];
            }
            catch(System.Exception ex) 
            {
                ConsoleWindow.WriteLine("Warning: {0}", ex.Message);
            }
            return null;
        }

        public static void SetVariable(string name, Variable value)
        {
            if (_variables.ContainsKey(name))
            {
                _variables[name] = value;
            }
            else
                _variables.Add(name, value);
        }

        public static void TurnOnSwitch(string name)
        {
            if (!_switches.ContainsKey(name))
            {
                _switches[name] = new Switch() { Name = name };
                _switches[name].TurnOn();
            }
            else
                _switches[name].TurnOn();
        }

        public static void TurnOffSwitch(string name)
        {
            if (!_switches.ContainsKey(name))
            {
                _switches[name] = new Switch() { Name = name };
                _switches[name].TurnOff();
            }
            else
                _switches[name].TurnOff();
        }

        public static void ToggleSwitch(string name)
        {
            if (!_switches.ContainsKey(name))
            {
                _switches[name] = new Switch() { Name = name };
                _switches[name].Toggle();
            }
            else
                _switches[name].Toggle();
        }
    }
}
