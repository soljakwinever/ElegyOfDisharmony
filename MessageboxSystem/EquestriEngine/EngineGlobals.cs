using EquestriEngine.Data;
using EquestriEngine.Data.Inputs.Interfaces;
using EquestriEngine.Data.Inputs;
//using EquestriEngine.Objects.Nodes;
using EquestriEngine.Systems;
using Helper = Microsoft.Xna.Framework.MathHelper;

namespace EquestriEngine
{
    //public delegate void TriggerEvent(Objects.Nodes.Node trigger, NodeFlags triggerFlags);
    public delegate void GenericEvent(object sender, IEventInput input);

    public static class EngineGlobals
    {
        static Data.Collections.ActionList Current_ActionList;
        public static EquestriEngine GameReference;

        public static void Wait(object sender,IEventInput input)
        {
            IntInput IInput = (IntInput)input;
            System.Threading.Thread.Sleep(IInput.Input);
        }

        public static void AssignActionList(Data.Collections.ActionList list)
        {
            Current_ActionList = list;
            Current_ActionList.OnFinish += ListErase;
            Current_ActionList.StartFromBeginning();
        }

        public static void ListErase(object sender, IEventInput input)
        {
            Current_ActionList = null;
        }

        private static void TryContinueList()
        {
            if (Current_ActionList != null)
                Current_ActionList.ExecuteCurrent();
        }

        #region Switches And Variables

        public static void ToggleSwitch(object sender, IEventInput input)
        {
            var sInput = (StringInput)input;
            Systems.DataManager.ToggleSwitch(sInput.Input);
        }

        public static void TurnOnSwitch(object sender, IEventInput input)
        {
            var sInput = (StringInput)input;
            Systems.DataManager.TurnOnSwitch(sInput.Input);
        }

        public static void TurnOffSwitch(object sender, IEventInput input)
        {
            var sInput = (StringInput)input;
            Systems.DataManager.TurnOffSwitch(sInput.Input);
        }

        public static void ModifyVariables(object sender, IEventInput input)
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
                        Variable.Set(temp.VarInput, Systems.DataManager.GetVariable("{gold}").AsInt);
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
        }

        #endregion

        #region Conditionals

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static void Condition(object sender, IEventInput input)
        {
            if (!(input is ConditionInput))
            {
                ConsoleWindow.WriteLine("Invalid input passed to Condition method");
            }

            var cInput = (ConditionInput)input;
            var v1 = cInput.InputA as Variable;
            var v2 = cInput.InputB as Variable;
            bool path1 = false;
            bool path2;
            switch (cInput.Comparison)
            {
                case CompareType.Var_Var:
                case CompareType.Var_Gold:
                    if (cInput.CheckValue == CheckValue.ValueEqual)
                    {
                        if (v1.Value == v2.Value)
                            path1 = true;
                    }
                    else if (cInput.CheckValue == CheckValue.ValueGreater)
                    {
                        if (v1.AsInt > v2.AsInt)
                            path1 = true;
                    }
                    else if (cInput.CheckValue == CheckValue.ValueGreaterEqual)
                    {
                        if (v1.AsInt >= v2.AsInt)
                            path1 = true;
                    }
                    else if (cInput.CheckValue == CheckValue.ValueLess)
                    {
                        if (v1.AsInt < v2.AsInt)
                            path1 = true;
                    }
                    else if (cInput.CheckValue == CheckValue.ValueLessEqual)
                    {
                        if (v1.AsInt <= v2.AsInt)
                            path1 = true;
                    }
                    break;
                case CompareType.Var_String:
                    if (cInput.CheckValue == CheckValue.ValueEqual)
                    {
                        if (v1.Value == v2.Value)
                            path1 = true;
                    }
                    break;
                case CompareType.Switch_Val:
                    break;
            }
            path2 = !path1 && cInput.HasElse;
            if (path1)      //Conditional complete route
            {
                //cInput.Path1.OldList = Current_ActionList;
                Current_ActionList.ExecuteList(cInput.Path1);
            }
            else if (path2) //Else route
            {
                Current_ActionList.ExecuteList(cInput.Path2);
            }
        }

        #endregion

        #region Game Related

        public static void AdjustGold(object sender, IEventInput input)
        {
            var gold = DataManager.GetVariable("{gold}");
            IntInput temp = new IntInput();
            if (input is IntInput)
                temp = (IntInput)input;
            else
                EquestriEngine.ErrorMessage = "Invalid input passed into Adjust Gold";

            if (sender is Achievement /*|| sender is Objects.GameObjects.NPC*/)
            {
                var newGoldDisplay = new SystemWidgets.GoldDisplay(gold.AsInt, temp.Input);
                GameReference.WidgetDrawer.AddWidget(newGoldDisplay);
            }

            gold.AsInt += temp.Input;
            TryContinueList();
        }

        /// <summary>
        /// Starts a battle sequence
        /// </summary>
        /// <param name="sender">The object that sent the request </param>
        /// <param name="input">A BattleInput, this will hold information on enemies in battle, 
        /// and spoils, as well as info about the type of battle</param>
        public static void InitiateBattle(object sender, IEventInput input)
        {
            //var bScreen = new SystemScreens.BattleScreen("level.level");
        }

        #endregion

        public static void AddCutSceneActor(object sender, IEventInput input)
        {

        }

        public static void ShowMessageBox(object sender, IEventInput input)
        {
            var sInput = (StringInput)input;
            string _lines = sInput.Input;
            var screen = new SystemScreens.MessageBoxScreen(_lines, Current_ActionList);
            GameReference.State_Manager.AddScreen(screen);
        }

        public static bool ShowDialogueChoice(object sender, IEventInput input)
        {
            return false;
        }

        #region Audio

        public static void ChangeMusic(object sender, IEventInput input)
        {
            //var aInput = input as AudioInput;
        }

        #endregion

        #region Methods

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
                            throw new Data.Exceptions.EngineException("Incorrect number of parameters passed", false);
                        try
                        {
                            GenericEvent method = AdjustGold;
                            IntInput parameter = new IntInput();
                            parameter.Input = int.Parse(temp[1]);
                            output = new MethodParamPair(method, parameter);
                        }
                        catch
                        {
                            EquestriEngine.ErrorMessage = "Error Creating MethodParamPair";
                        }
                        break;
                    }
                case "addexp":
                    {
                        GenericEvent method = AdjustGold;
                        IntInput parameter = new IntInput();
                        parameter.Input = int.Parse(temp[1]);
                        output = new MethodParamPair(method, parameter);
                        break;
                    }
                case "toggleswitch":
                    {
                        if (temp.Length != 2)
                            throw new Data.Exceptions.EngineException("Incorrect number of parameters passed", false);
                        GenericEvent method = ToggleSwitch;
                        StringInput parameter = new StringInput();
                        parameter.Input = temp[1];
                        output = new MethodParamPair(method, parameter);
                        break;
                    }
                case "activateswitch":
                    {
                        if (temp.Length != 2)
                            throw new Data.Exceptions.EngineException("Incorrect number of parameters passed", false);
                        break;
                    }
                case "deactivateswitch":
                    {
                        if (temp.Length != 2)
                            throw new Data.Exceptions.EngineException("Incorrect number of parameters passed", false);

                        break;
                    }
                default:
                    throw new Data.Exceptions.EngineException("Method name Not Found", false);
            }
            return output;
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

        public static float Slerp(float v1, float v2, float amount)
        {
            return Helper.SmoothStep(v1, v2, amount);
        }

        public static float ToRadians(float degrees)
        {
            return degrees * (Pi / 180);
        }
    }

    public class Equestribatch : Microsoft.Xna.Framework.Graphics.SpriteBatch
    {
        private bool _ready;

        public bool Ready
        {
            get { return _ready; }
        }

        public Equestribatch(Microsoft.Xna.Framework.Graphics.GraphicsDevice device)
            : base(device)
        {
            _ready = true;
        }
    }

    public class EquestriSkeleBatch : Spine.SkeletonRenderer
    {
        private bool _ready;

        public bool Ready
        {
            get { return _ready; }
        }

        public EquestriSkeleBatch(Microsoft.Xna.Framework.Graphics.GraphicsDevice device)
            : base(device)
        {
            _ready = true;
        }
    }
}
