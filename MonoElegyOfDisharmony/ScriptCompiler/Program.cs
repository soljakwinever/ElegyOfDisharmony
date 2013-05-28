using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ScriptCompiler
{
    class Program
    {
        static Dictionary<string, int> _methodIndexes;

        static Program()
        {
            _methodIndexes = new Dictionary<string, int>();
            //0-9 will be reserved for System functions
            _methodIndexes["Wait"] = 1;
            _methodIndexes["Conditional"] = 2;

            _methodIndexes["SetVariable"] = 20;
            _methodIndexes["SetSwitch"] = 21;
            _methodIndexes["ToggleSwitch"] = 22;
            //30-39 will be reserved for game specific
            _methodIndexes["AddGold"] = 30;
            _methodIndexes["OpenSaveScreen"] = 31;
            _methodIndexes["StartBattle"] = 31;
            _methodIndexes["DamageActor"] = 32;
            _methodIndexes["ShowMessageBox"] = 33;
            //40-45 will be reserved for audio
            _methodIndexes["SetBgm"] = 40;
            _methodIndexes["SetSfx"] = 41;
            _methodIndexes["ChangeBattleBgm"] = 42;
            //_methodIndexes[""] = 43;
            //_methodIndexes[""] = 44;
        }

        static void Main(string[] args)
        {

            if (args.Length != 2)
            {
                throw new Exception("Invalid params passed");
            }

            using (Utilities.FormattedFile file = new Utilities.FormattedFile())
            {
                string temp = "";
                Dictionary<int, object[]> _methodsRaw = new Dictionary<int, object[]>();

                //Read in the .ell script format to compile to a .ecs file
                file.ReadBegin(args[0] + ".ell");

                temp = file.ReadLine(); //Read in the version
                if (temp != "1.0.0.0")
                    throw new Exception("Script is not for this version of the compiler");

                temp = file.ReadBlock(); //Read in the header

                if (temp != "ElegyScript")
                    throw new Exception("Invalid header tag");

                int entries = int.Parse(file.ReadLine());

                for (int i = 0; i < entries; i++)
                {
                    temp = file.ReadBlock();    //Read in the method name

                    switch (temp)
                    {
                        case "ShowMessageBox":
                            {
                                int lines = int.Parse(file.ReadLine());
                                string[] methodArgs = new string[lines];

                                for (int l = 0; l < lines; l++)
                                    methodArgs[l] = file.ReadLine();

                                _methodsRaw[i] = new object[2];
                                _methodsRaw[i][0] = temp;
                                _methodsRaw[i][1] = methodArgs;
                                break;
                            }
                        case "SetVariable":
                            {
                                string name, val;
                                name = file.ReadLine();
                                val = file.ReadLine();
                                _methodsRaw[i] = new object[3];
                                _methodsRaw[i][0] = temp;
                                _methodsRaw[i][1] = name;
                                _methodsRaw[i][2] = val;
                                break;
                            }
                        case "SetSwitch":
                            {
                                string name;
                                string rawVal;
                                bool val = false;
                                name = file.ReadLine();
                                rawVal = file.ReadLine();
                                if (rawVal == "ON")
                                    val = true;
                                else if (rawVal == "OFF")
                                    val = false;
                                _methodsRaw[i] = new object[3];
                                _methodsRaw[i][0] = temp;
                                _methodsRaw[i][1] = name;
                                _methodsRaw[i][2] = val;
                                break;
                            }
                        case "ToggleSwitch":
                        case "AddGold":
                        case "Wait":
                            {
                                string name;
                                name = file.ReadLine();
                                _methodsRaw[i] = new object[2];
                                _methodsRaw[i][0] = temp;
                                _methodsRaw[i][1] = name;
                                break;
                            }
                        case "Conditional":
                            {
                                break;
                            }
                        case "OpenSaveScreen":
                        case "StartBattle":
                        case "DamageActor":

                        //Audio
                        case "SetBgm":
                        case "SetSfx":
                            break;
                        case "ChangeBattleBgm":
                            break;
                        default:
                            throw new Exception("No such method exists or is implemented");
                    }


                    file.ReadEndBlock();
                }

                file.ReadEndBlock();

                file.ReadEnd();

                using (System.IO.BinaryWriter bw = new BinaryWriter(File.Create(args[1] + ".esl")))
                {
                    foreach (var kvp in _methodsRaw)
                    {
                        bw.Write(kvp.Key);
                        bw.Write(_methodIndexes[kvp.Value[0].ToString()]);
                        switch (kvp.Value[0].ToString())
                        {
                            case "ShowMessageBox":
                                {
                                    string[] lines = (string[])kvp.Value[1];
                                    bw.Write((byte)lines.Length);
                                    for (byte i = 0; i < lines.Length; i++)
                                    {
                                        bw.Write(lines[i]);
                                    }
                                }
                                break;
                            case "SetVariable":
                            case "SetSwitch":
                            case "ToggleSwitch":
                            case "AddGold":
                            case "Wait":
                            case "Conditional":
                            case "OpenSaveScreen":
                            case "StartBattle":
                            case "DamageActor":

                            //Audio
                            case "SetBgm":
                            case "SetSfx":
                            case "ChangeBattleBgm":
                                break;
                        }
                    }
                }
            }
        }
    }
}
