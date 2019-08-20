using Avangarde.KeyboardExtender.Externals;
using Avangarde.KeyboardExtenderPlugins;
using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avangarde.KeyboardExtender.Project
{
    public sealed class ProjectManager : IDisposable
    {
        protected List<IBehavior> _availableBindings { get; set; }
        protected List<IBehavior> _activeBindings { get; set; }



        #region SingletonConfiguration
        private static ProjectManager _instance = new ProjectManager();

        static ProjectManager()
        {
        }

        private ProjectManager()
        {
        }


        public static ProjectManager Instance
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        private bool _pressedOnAltLeft = false;
        private bool _pressedOnCtrlLeft = false;
        private bool _pressedOnShiftLeft = false;
        private bool _pressedOnSuperLeft = false;
        private bool _pressedOnAltRight = false;
        private bool _pressedOnSuperRight = false;
        private bool _pressedOnCtrlRight = false;
        private bool _pressedOnShiftRight = false;


        private GlobalKeyboardHook _globalKeyboardHook;
        private byte[] _modifierArray;

        

        public bool RunMainLogic()
        {
            InitializeBehaviors();

            LoadConfiguration();
            SetupKeyboardHooks();

            return true;

        }

        private void LoadConfiguration()
        {
            if (!File.Exists(Environment.CurrentDirectory + "\\KeyboardExtender.cfg"))
            {
                CreateDefaultConfigFile();
            }

            FileIniDataParser finiParser = new IniParser.FileIniDataParser();
            IniData envelope = finiParser.ReadFile(Environment.CurrentDirectory + "\\KeyboardExtender.cfg");

            string modmask = envelope["General"]["ModMask"];
            this._modifierArray = CalculateModifierArrayFromString(modmask);
            List<KeyData> keys = envelope["Keys"].ToList();

            foreach (KeyData key in keys)
            {
                string keyRaw = key.KeyName;
                string value = key.Value;

                foreach (var bind in this._availableBindings)
                {
                    if (bind.Identifier == value)
                    {
                        bind.TriggerKey = (Keys)Enum.Parse(typeof(Keys), keyRaw);
                        bind.ModifierArray = this._modifierArray;
                        this._activeBindings.Add(bind);
                    }
                }
            }


        }

        private void CreateDefaultConfigFile()
        {
            string[] contents = { "[General]", "ModMask = <" + Keys.LControlKey.ToString() + "><" + Keys.Alt.ToString() + ">", "[Keys]"};
            System.IO.File.WriteAllLines(Environment.CurrentDirectory + "\\KeyboardExtender.cfg", contents);
        }

        private byte[] CalculateModifierArrayFromString(string modmask)
        {
            var pattern = @"\<(.*?)\>";
            //var query = "H1-receptor antagonist [HSA:3269] [PATH:hsa04080(3269)]";
            var matches = Regex.Matches(modmask, pattern);

            List<string> masks = new List<string>();

            foreach (Match m in matches)
            {
                Console.WriteLine(m.Groups[1]);
                masks.Add(m.Groups[1].ToString());

            }

            byte[] result = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };

            foreach (string each in masks)
            {
                ////Alt, AltGr, CtrlLeft, CtrlRight, ShiftLeft, ShiftRight, SuperLeft, SuperRight
                if ((Keys)Enum.Parse(typeof(Keys), each) == Keys.Alt)
                {
                    result[0] = 1;
                }

                if ((Keys)Enum.Parse(typeof(Keys), each) == Keys.LControlKey)
                {
                    result[2] = 1;
                }

                if ((Keys)Enum.Parse(typeof(Keys), each) == Keys.RControlKey)
                {
                    result[3] = 1;
                }

                if ((Keys)Enum.Parse(typeof(Keys), each) == Keys.LShiftKey)
                {
                    result[4] = 1;
                }

                if ((Keys)Enum.Parse(typeof(Keys), each) == Keys.RShiftKey)
                {
                    result[5] = 1;
                }

                if ((Keys)Enum.Parse(typeof(Keys), each) == Keys.LWin)
                {
                    result[6] = 1;
                }

                if ((Keys)Enum.Parse(typeof(Keys), each) == Keys.RWin)
                {
                    result[7] = 1;
                }

            }

            return result;
        }

        private void InitializeBehaviors()
        {

            if (this._availableBindings == null)
            {
                this._availableBindings = new List<IBehavior>();
            }

            if (this._activeBindings == null)
            {
                this._activeBindings = new List<IBehavior>();
            }


            Type[] typelist = GetTypesInNamespace(Assembly.GetAssembly(typeof(IBehavior)),
                                      "Avangarde.KeyboardExtenderPlugins.Plugins");

            foreach (Type t in typelist)
            {

                ConstructorInfo ctor = t.GetConstructor(Type.EmptyTypes);
                object instance = ctor.Invoke(new object[] { });
                _availableBindings.Add(instance as IBehavior);
            }

        }

        private Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return assembly.GetTypes().Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal)).ToArray();
        }

        private void SetupKeyboardHooks()
        {
            _globalKeyboardHook = new GlobalKeyboardHook();
            _globalKeyboardHook.KeyboardPressed += _globalKeyboardHook_KeyboardPressed;
        }

        private void _globalKeyboardHook_KeyboardPressed(object sender, GlobalKeyboardHookEventArgs e)
        {


            //Handle modifiers
            if ((e.KeyboardState == GlobalKeyboardHook.KeyboardState.SysKeyDown || e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown) && (Enum.IsDefined(typeof(KeyboardVirtualCodes), e.KeyboardData.VirtualCode)))
            {
                KeyboardVirtualCodes keyValueParsed = (KeyboardVirtualCodes)e.KeyboardData.VirtualCode;

                switch (keyValueParsed)
                {
                    case KeyboardVirtualCodes.Alt:
                        this._pressedOnAltLeft = true;
                        break;
                    case KeyboardVirtualCodes.AltGr:
                        this._pressedOnAltRight = true;
                        break;
                    case KeyboardVirtualCodes.CtrlLeft:
                        this._pressedOnCtrlLeft = true;
                        break;
                    case KeyboardVirtualCodes.CtrlRight:
                        this._pressedOnCtrlRight = true;
                        break;
                    case KeyboardVirtualCodes.ShiftLeft:
                        this._pressedOnShiftLeft = true;
                        break;
                    case KeyboardVirtualCodes.ShiftRight:
                        this._pressedOnShiftRight = true;
                        break;
                    case KeyboardVirtualCodes.SuperLeft:
                        this._pressedOnSuperLeft = true;
                        break;
                    case KeyboardVirtualCodes.SuperRight:
                        this._pressedOnSuperRight = true;
                        break;
                    default:
                        break;
                }


                Debug.WriteLine("Modifier ON: " + keyValueParsed.ToString() + " : " + e.KeyPressed.ToString());

                //e.Handled = true;
                return;
            }

            //Handle modifiers
            if ((e.KeyboardState == GlobalKeyboardHook.KeyboardState.SysKeyUp || e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyUp) && (Enum.IsDefined(typeof(KeyboardVirtualCodes), e.KeyboardData.VirtualCode)))
            {
                KeyboardVirtualCodes keyValueParsed = (KeyboardVirtualCodes)e.KeyboardData.VirtualCode;

                switch (keyValueParsed)
                {
                    case KeyboardVirtualCodes.Alt:
                        this._pressedOnAltLeft = false;
                        break;
                    case KeyboardVirtualCodes.AltGr:
                        this._pressedOnAltRight = false;
                        break;
                    case KeyboardVirtualCodes.CtrlLeft:
                        this._pressedOnCtrlLeft = false;
                        break;
                    case KeyboardVirtualCodes.CtrlRight:
                        this._pressedOnCtrlRight = false;
                        break;
                    case KeyboardVirtualCodes.ShiftLeft:
                        this._pressedOnShiftLeft = false;
                        break;
                    case KeyboardVirtualCodes.ShiftRight:
                        this._pressedOnShiftRight = false;
                        break;
                    case KeyboardVirtualCodes.SuperLeft:
                        this._pressedOnSuperLeft = false;
                        break;
                    case KeyboardVirtualCodes.SuperRight:
                        this._pressedOnSuperRight = false;
                        break;
                    default:
                        break;
                }

                Debug.WriteLine("Modifier OFF: " + keyValueParsed.ToString() + " : " + e.KeyPressed.ToString());

                //e.Handled = true;
                return;
            }

            Debug.WriteLine(e.KeyboardState.ToString() + " : " + e.KeyboardData.VirtualCode + " : " + e.KeyPressed.ToString());

            if ((e.KeyboardState == GlobalKeyboardHook.KeyboardState.SysKeyUp || e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyUp))
            {

                byte[] hello = CalculateCurrentModifierArray();

                if (this._modifierArray.SequenceEqual(hello))
                {

                    IBehavior detected = null;

                    foreach (IBehavior type in this._activeBindings)
                    {
                        Keys trigger = type.TriggerKey;
                        if (e.KeyPressed == trigger)
                        {
                            detected = type;
                            break;
                        }
                    }
                    if (detected != null)
                    {
                        foreach (IBehavior type in this._activeBindings)
                        {
                            if (type != detected)
                            {
                                type.ResetModifier();
                            }
                        }

                        detected.ExecuteBehavior();
                        e.Handled = true;
                        return;
                    }



                }
            }


            /*
            if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyUp)

                //Debug.WriteLine(e.KeyboardData.VirtualCode);

                if (e.KeyboardData.VirtualCode != GlobalKeyboardHook.VkSnapshot)
                    return;

            // seems, not needed in the life.
            //if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.SysKeyDown &&
            //    e.KeyboardData.Flags == GlobalKeyboardHook.LlkhfAltdown)
            //{
            //    MessageBox.Show("Alt + Print Screen");
            //    e.Handled = true;
            //}
            //else
            */

        }

        private byte[] CalculateCurrentModifierArray()
        {
            byte[] result = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };

            ////Alt, AltGr, CtrlLeft, CtrlRight, ShiftLeft, ShiftRight, SuperLeft, SuperRight
            if (this._pressedOnAltLeft)
            {
                result[0] = 1;
            }

            if (this._pressedOnAltRight)
            {
                result[1] = 1;
            }

            if (this._pressedOnCtrlLeft)
            {
                result[2] = 1;
            }

            if (this._pressedOnCtrlRight)
            {
                result[3] = 1;
            }

            if (this._pressedOnShiftLeft)
            {
                result[4] = 1;
            }

            if (this._pressedOnShiftRight)
            {
                result[5] = 1;
            }

            if (this._pressedOnSuperLeft)
            {
                result[6] = 1;
            }

            if (this._pressedOnSuperRight)
            {
                result[7] = 1;
            }

            return result;
        }

        public void Dispose()
        {
            _globalKeyboardHook?.Dispose();
        }
    }
}
