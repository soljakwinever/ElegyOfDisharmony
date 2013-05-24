using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MessageboxSystem
{
    public class MessageBox
    {
        private string
            _message,
            _name;

        private int charsShown;

        private float charDisplayTime;
        private static float delay_time = 0.1f;

        Scene _sceneReference;

        string expression;

        Dictionary<int, string> _expressionChanges;
        Dictionary<int, float> _delayChanges;
        Dictionary<int, MoveToData> _movements;

        public string Name
        {
            get { return _name; }
        }

        private bool finished;

        public Scene SceneReference
        {
            get { return _sceneReference; }
        }

        public bool Finished
        {
            get { return finished; }
            set
            {
                finished = value;
            }
        }

        public MessageBox(string message, Scene scene)
        {
            _sceneReference = scene;
            charsShown = 0;
            _message = message;
            _name = "";
            expression = "none";

            _expressionChanges = new Dictionary<int, string>();
            _movements = new Dictionary<int, MoveToData>();
            _delayChanges = new Dictionary<int, float>();

            ParseMessagebox(this, message);

            charDisplayTime = _delayChanges.ContainsKey(0) ? _delayChanges[0] : 0.0f;
            finished = false;
        }

        public void Update(float dt)
        {
            charDisplayTime -= dt;

            if (_delayChanges.ContainsKey(charsShown))
                delay_time = _delayChanges[charsShown];
            if (_expressionChanges.ContainsKey(charsShown))
                expression = _expressionChanges[charsShown];
            if (_movements.ContainsKey(charsShown))
                _sceneReference.CharacterMovements[_name.ToLower()] = _movements[charsShown];

            if (!finished && charDisplayTime <= 0.0)
            {
                charsShown++;

                charDisplayTime = delay_time;
            }   
            finished = charsShown == _message.Length;
        }

        /* TODO After Shower:
         * Make code for displaying Name Tag
         * Start on NodeTree manager
         * 
         * */

        public void Draw(SpriteBatch sb, SpriteFont font, Vector2 Position)
        {
            /* -- Obsolete Display Code --
            Console.Clear();
            if (!string.IsNullOrEmpty(_name))
            {
                Console.WriteLine(_name + " says:");
            }
            Console.WriteLine("Expression: {0}", expression);
            Console.WriteLine(_message.Substring(0, charsShown));
             * */

            sb.DrawString(font, _message.Substring(0, charsShown),
                new Vector2(200, 480 - 150) + Vector2.One, Color.Black);
            sb.DrawString(font, _message.Substring(0, charsShown),
                new Vector2(200, 480 - 150), Color.White);
        }

        private static void ParseMessagebox(MessageBox box, string message)
        {
            var _matches = new List<Tuple<int, string>>();
            Regex test = new Regex(@"/ex|(\[[A-Za-z]+,[A-Za-z0-9]+\])" +
                                    @"|/name|\[[A-Za-z]*\]" +
                                    @"|/d|\[[0-9\.]*\]" +
                // /move[(X|Y),Rot,(SX,SY),Flip?,(Smooth?,TimeToTake)]
                                    @"|/move|(\[\([0-9\.\-]+\|[0-9\.\-]+\),[0-9]+,\([0-9\.]+\|[0-9\.]+\),[0-1](?:,[0-1],[0-9\.]+)?\])"
                        );

            MatchCollection matchCollection = test.Matches(message);
            int lengthToSub = 0;
            for (int i = 0; i < matchCollection.Count; i++)
            {
                _matches.Add(new Tuple<int, string>(matchCollection[i].Index, matchCollection[i].Value));
            }
            for (int i = 0; i < _matches.Count; i += 2)
            {
                Tuple<int, string> command = _matches[i];
                Tuple<int, string> param = _matches[i + 1];
                int position = command.Item1;
                switch (command.Item2)
                {

                    case "/ex":
                        {
                            string[] temp = param.Item2.Split(new[] { '[', ',', ']' }, StringSplitOptions.RemoveEmptyEntries);
                            string subject, expression;
                            subject = temp[0];
                            expression = temp[1];

                            box._expressionChanges[position - (lengthToSub)] = expression;
                            break;
                        }
                    case "/name":
                        {
                            var split = param.Item2.Split(new[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                            if (split.Length == 0)
                                break;
                            string temp = split[0];
                            box._name = temp;
                            break;
                        }
                    case "/audio":
                        {
                            break;
                        }
                    case "/d":
                        {
                            var temp = param.Item2.Split(new[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                            float delayVal = 0.1f;
                            if (temp.Length == 1)
                                delayVal = float.Parse(temp[0]);
                            bool addOne = box._delayChanges.ContainsKey(position - lengthToSub);
                            box._delayChanges[addOne ? (position - lengthToSub) + 1 : position - lengthToSub] = delayVal;

                            break;
                        }
                    case "/move":
                        {
                            string[] temp = param.Item2.Split(new[] { '[', ',', ']' }, StringSplitOptions.RemoveEmptyEntries);
                            string[] bracketContents;
                            try
                            {
                                //Split the movement code into pieces

                                bracketContents = temp[0].Split(new[] { '(', '|', ')' }, StringSplitOptions.RemoveEmptyEntries);    //We start with the X and Y positions
                                Vector2 moveTo = new Vector2(
                                    float.Parse(bracketContents[0]),
                                    float.Parse(bracketContents[1]));
                                float rotTo = float.Parse(temp[1]);             //Then the rotation of the object
                                bracketContents = temp[2].Split(new[] { '(', '|', ')' }, StringSplitOptions.RemoveEmptyEntries);
                                Vector2 scaleTo = new Vector2(                  //Then the scale
                                    float.Parse(bracketContents[0]),
                                    float.Parse(bracketContents[1]));
                                bool flipped = temp[3] == "1";
                                bool instant = temp.Length == 4;
                                bool smooth = false;
                                float ttt = 0.0f;
                                if (!instant)
                                {
                                    smooth = temp[4] == "1";
                                    ttt = float.Parse(temp[5]);
                                }
                                box._movements[position - lengthToSub] = new MoveToData(moveTo, scaleTo, MathHelper.ToRadians(rotTo), flipped, instant, ttt, smooth);   //Then we give it to a new MoveData object

                            }
                            catch (Exception ex)
                            {
                                CutSceneGame.ErrorMessage = ex.Message;
                            }
                            break;
                        }
                }
                lengthToSub += command.Item2.Length + param.Item2.Length;
            }

            for (int i = 0; i < _matches.Count; i++)
            {
                box._message = box._message.Replace(_matches[i].Item2, "");
            }

            Console.WriteLine(box._message);
        }
    }
}
