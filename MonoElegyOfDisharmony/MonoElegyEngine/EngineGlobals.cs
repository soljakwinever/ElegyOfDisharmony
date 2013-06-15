using EquestriEngine.Data;
using EquestriEngine.Data.Inputs.Interfaces;
using EquestriEngine.Data.Inputs;
using EquestriEngine.Data.Collections;

//using EquestriEngine.Objects.Nodes;
using EquestriEngine.Systems;
using Helper = Microsoft.Xna.Framework.MathHelper;

namespace EquestriEngine
{
    //public delegate void TriggerEvent(Objects.Nodes.Node trigger, NodeFlags triggerFlags);
    public delegate void GenericEvent(object sender, IEventInput input);

    public class EngineGlobals
    {
        public static EquestriEngine GameReference;
        public static Microsoft.Xna.Framework.GameTime GameTime;

        public static GameSettings Settings
        {
            get { return GameReference.Settings; }
        }
        
        public static AssetManager AssetManager
        {
            get { return GameReference.AssetManager; }
        }

        public static DataManager DataManager
        {
            get { return GameReference.DataManager; }
        }

        public static StateManager StateManager
        {
            get { return GameReference.State_Manager; }
        }


        protected EngineGlobals()
        {

        }

        public static MethodResult Wait(object sender, IEventInput input)
        {
            IntInput IInput = (IntInput)input;

            System.Threading.Thread.Sleep(IInput.Input);

            return MethodResult.Success;
        }

        #region Switches And Variables

        public static MethodResult CreateVariable(object sender, IEventInput input)
        {
            var vInput = (CreateVariableInput)input;

            Variable newVar = new Variable(vInput.Value);

            DataManager.SetVariable(vInput.Name, newVar);
            return MethodResult.Success;
        }

        public static MethodResult ToggleSwitch(object sender, IEventInput input)
        {
            var sInput = (StringInput)input;
            EngineGlobals.DataManager.ToggleSwitch(sInput.Input);
            return MethodResult.Success;
        }

        public static MethodResult TurnOnSwitch(object sender, IEventInput input)
        {
            var sInput = (StringInput)input;
            EngineGlobals.DataManager.TurnOnSwitch(sInput.Input);
            return MethodResult.True;
        }

        public static MethodResult TurnOffSwitch(object sender, IEventInput input)
        {
            var sInput = (StringInput)input;
            EngineGlobals.DataManager.TurnOffSwitch(sInput.Input);
            return MethodResult.False;
        }

        public static MethodResult ModifyVariables(object sender, IEventInput input)
        {
            if (input is IntModInput)
            {
                var temp = (IntModInput)input;
                switch (temp.AdjustmentType)
                {
                    case AdjustType.Addition:
                        temp.VarInput += temp.IntInput;
                        break;
                    case AdjustType.Subtract:
                        temp.VarInput -= temp.IntInput;
                        break;
                    case AdjustType.Multiply:
                        temp.VarInput *= temp.IntInput;
                        break;
                    case AdjustType.Divide:
                        temp.VarInput /= temp.IntInput;
                        break;
                    case AdjustType.Modulus:
                        temp.VarInput %= temp.IntInput;
                        break;
                    case AdjustType.Set:
                        Variable.Set(temp.VarInput, temp.IntInput);
                        break;
                    case AdjustType.SetToGold:
                        Variable.Set(temp.VarInput, EngineGlobals.DataManager.GetVariable("{gold}").AsInt);
                        break;
                    case AdjustType.SetToPlayerX:
                        break;
                    case AdjustType.SetToPlayerY:
                        break;
                }
            }
            else if (input is VariableModInput)
            {
                var temp = (VariableModInput)input;
                switch (temp.AdjustmentType)
                {
                    case AdjustType.Addition:
                        temp.InputL.AsInt += temp.InputR.AsInt;
                        break;
                    case AdjustType.Subtract:
                        temp.InputL.AsInt -= temp.InputR.AsInt;
                        break;
                    case AdjustType.Multiply:
                        temp.InputL.AsInt *= temp.InputR.AsInt;
                        break;
                    case AdjustType.Divide:
                        temp.InputL.AsInt /= temp.InputR.AsInt;
                        break;
                    case AdjustType.Modulus:
                        temp.InputL.AsInt %= temp.InputR.AsInt;
                        break;
                    case AdjustType.Set:
                        temp.InputL.Value = temp.InputR.Value;
                        break;
                }
            }
            return MethodResult.Success;
        }

        #endregion

        #region Conditionals

        /// <summary>
        /// Checks internal variables and switches for their values and alternates method paths on the result
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static MethodResult Condition(object sender, IEventInput input)
        {
            if (!(input is ConditionInput))
            {
                ConsoleWindow.WriteLine("Invalid input passed to Condition method");
            }

            var cInput = (ConditionInput)input;

            switch (cInput.Comparison)  //Check the comparison type
            {
                case CompareType.Var_Var:
                case CompareType.Var_Gold:
                    {
                        var v1 = cInput.InputA as Variable;     //Turn the inputs into variables
                        var v2 = cInput.InputB as Variable;
                        switch (cInput.CheckValue)
                        {

                            case CheckValue.ValueEqual:
                                return v1.Value == v2.Value ? MethodResult.True : MethodResult.False;   //Check if Value A is the same 
                            case CheckValue.ValueGreater:
                                return v1.AsInt > v2.AsInt ? MethodResult.True : MethodResult.False;    //Check if Value A is greater than value B
                            case CheckValue.ValueGreater | CheckValue.ValueEqual:
                                return v1.AsInt >= v2.AsInt ? MethodResult.True : MethodResult.False;   //Check if Value A is greater or equal to value B
                            case CheckValue.ValueLess:
                                return v1.AsInt < v2.AsInt ? MethodResult.True : MethodResult.False;    //Check if Value A is less than value B
                            case CheckValue.ValueLess | CheckValue.ValueEqual:
                                return v1.AsInt <= v2.AsInt ? MethodResult.True : MethodResult.False;   //Check if Value A is less than or equal to value B
                        }
                    }
                    return MethodResult.Fail;       //If none of those were compared then it's a fail flag
                case CompareType.Var_String:
                    {
                        var v1 = cInput.InputA as Variable;                 //Compare the variables as strings
                        var v2 = cInput.InputB as Variable;
                        if (cInput.CheckValue == CheckValue.ValueEqual)
                        {
                            return v1.AsString == v2.AsString ? MethodResult.True : MethodResult.False;
                        }
                    }
                    break;
                case CompareType.Switch_Val:
                    {
                        var s1 = cInput.InputA as Switch;
                        if (cInput.CheckValue == CheckValue.ValueON)
                        {
                            return s1.Value ? MethodResult.True : MethodResult.False;
                        }
                        if (cInput.CheckValue == CheckValue.ValueOFF)
                        {
                            return s1.Value ? MethodResult.False : MethodResult.True;
                        }
                        if (cInput.CheckValue == CheckValue.ValueEqual)
                        {
                            var s2 = cInput.InputB as Switch;
                            return s1.Value == s2.Value ? MethodResult.True : MethodResult.False;
                        }
                    }
                    break;
            }
            return MethodResult.Fail;
        }

        #endregion

        #region Game Related

        public static MethodResult AdjustGold(object sender, IEventInput input)
        {
            var gold = EngineGlobals.DataManager.GetVariable("{gold}");
            IntInput temp = new IntInput();
            if (input is IntInput)
                temp = (IntInput)input;
            else
                EquestriEngine.ErrorMessage = "Invalid input passed into Adjust Gold";

            var newGoldDisplay = new SystemWidgets.GoldDisplay(gold.AsInt, temp.Input);


            GameReference.WidgetDrawer.AddWidget(newGoldDisplay);


            gold.AsInt += temp.Input;
            return MethodResult.Success;
        }

        #endregion

        public static MethodResult AddCutSceneActor(object sender, IEventInput input)
        {
            return MethodResult.Success;
        }

        public static MethodResult ShowMessageBox(object sender, IEventInput input)
        {
            var sInput = (StringInput)input;
            string _lines = sInput.Input;
            var screen = new SystemScreens.MessageBoxScreen(_lines);
            GameReference.State_Manager.AddScreen(screen);
            while (screen.Result != MethodResult.Yes)
            {
                System.Threading.Thread.Sleep(100);
            }
            return MethodResult.Success;
        }

        public static MethodResult WaitForInput(object sender, IEventInput input)
        {
            ControlInput controlInput = (ControlInput)input;
            while (!controlInput.Control.Value)
            {
                System.Threading.Thread.Sleep(10);
            }
            return MethodResult.Success;
        }

        public static MethodResult ShowDialogueChoice(object sender, IEventInput input)
        {
            return MethodResult.No;
        }

        #region Audio

        public static void ChangeMusic(object sender, IEventInput input)
        {
            //var aInput = input as AudioInput;
        }

        #endregion

        #region Methods

        public static Data.Inputs.MethodParamPair GenerateMethodFromArgs(string method, string[] param, string[] paths)
        {
            MethodParamPair output = null;

            ExitPathGroup methodPaths = new ExitPathGroup();
            for (int i = 0; i < paths.Length; i++)
            {
                var temp = paths[i].Split(';');
                if (temp.Length != 2)
                    throw new EngineException("Invalid path size",true);
                int rawResult = int.Parse(temp[i]), nextMethod;
                MethodResult result = (MethodResult)rawResult;
                nextMethod = int.Parse(temp[1]);
                methodPaths[result] = nextMethod;
            }
            switch (method.ToLower())
            {
                case "addgold":
                    {
                        break;
                    }
                case "conditional":
                    {
                        ConditionInput conditionInput = new ConditionInput();
                        break;
                    }
                case "addexp":
                    { 
                        break;
                    }
                case "toggleswitch":
                    {
                        break;
                    }
                case "activateswitch":
                    {
                        break;
                    }
                case "deactivateswitch":
                    {
                        break;
                    }
                case "showmessagebox":
                    {
                        string messageBoxInput = "";
                        for (int i = 0; i < param.Length; i++)
                        {
                            messageBoxInput += param[i] + "\n";
                        }
                        StringInput methodInput = new StringInput() { Input = messageBoxInput };

                        output = new MethodParamPair(ShowMessageBox, methodInput, methodPaths);
                        break;
                    }
                default:
                    throw new EngineException("Method name Not Found", false);
            }
            return output;
        }

        public static Data.Inputs.MethodParamPair GenerateMethodFromString(string input)
        {
            Data.Inputs.MethodParamPair output = null;
            string[] temp = input.Split(';');
            string methodName = temp[0].ToLower();
            switch (methodName)
            {
                case "addgold":
                    {
                        if (temp.Length != 2)
                            throw new EngineException("Incorrect number of parameters passed", false);
                        try
                        {
                            MethodParamResult method = AdjustGold;
                            IntInput parameter = new IntInput();
                            parameter.Input = int.Parse(temp[1]);
                            //output = new MethodParamPair(method, parameter);
                        }
                        catch
                        {
                            EquestriEngine.ErrorMessage = "Error Creating MethodParamPair";
                        }
                        break;
                    }
                case "addexp":
                    {
                        MethodParamResult method = AdjustGold;
                        IntInput parameter = new IntInput();
                        parameter.Input = int.Parse(temp[1]);
                        //output = new MethodParamPair(method, parameter);
                        break;
                    }
                case "toggleswitch":
                    {
                        if (temp.Length != 2)
                            throw new EngineException("Incorrect number of parameters passed", false);
                        MethodParamResult method = ToggleSwitch;
                        StringInput parameter = new StringInput();
                        parameter.Input = temp[1];
                        //output = new MethodParamPair(method, parameter);
                        break;
                    }
                case "activateswitch":
                    {
                        if (temp.Length != 2)
                            throw new EngineException("Incorrect number of parameters passed", false);
                        break;
                    }
                case "deactivateswitch":
                    {
                        if (temp.Length != 2)
                            throw new EngineException("Incorrect number of parameters passed", false);

                        break;
                    }
                default:
                    throw new EngineException("Method name Not Found", false);
            }
            return output;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="file">An already open formatted file</param>
        /// <returns></returns>
        public static MethodParamCollection ReadScript(Utilities.FormattedFile file)
        {
            return null;
        }

        #endregion
    }

    public static class MathHelper
    {
        public const float E = Helper.E;
        public const float Log10E = Helper.Log10E;
        public const float Log2E = Helper.Log2E;
        public const float Pi = Helper.Pi;
        public const float PiOver2 = Helper.PiOver2;
        public const float PiOver4 = Helper.PiOver4;
        public const float TwoPi = Helper.TwoPi;

        public static float Barycentric(float v1, float v2, float v3, float a1, float a2)
        {
            return Helper.Barycentric(v1, v2, v3, a1, a2);
        }

        public static float CatmullRom(float v1, float v2, float v3, float v4, float amount)
        {
            return Helper.CatmullRom(v1, v2, v3, v4, amount);
        }

        public static float Clamp(float val, float min, float max)
        {
            return Helper.Clamp(val, min, max);
        }

        public static float Distance(float v1, float v2)
        {
            return Helper.Distance(v1, v2);
        }

        public static float Lerp(float v1, float v2, float amount)
        {
            return Helper.Lerp(v1, v2, amount);
        }

        public static float Slerp(float v1, float v2, float amount)
        {
            return Helper.SmoothStep(v1, v2, amount);
        }

        public static float ToRadians(float degrees)
        {
            return degrees * (Pi / 180);
        }
    }

    //public class EquestriSkeleBatch : Spine.SkeletonRenderer
    //{
    //    private bool _ready;

    //    public bool Ready
    //    {
    //        get { return _ready; }
    //    }

    //    public EquestriSkeleBatch(Microsoft.Xna.Framework.Graphics.GraphicsDevice device)
    //        : base(device)
    //    {
    //        _ready = true;
    //    }
    //}
}
