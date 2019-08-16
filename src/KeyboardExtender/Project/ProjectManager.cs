using Avangarde.KeyboardExtender.Externals;
using Avangarde.KeyboardExtender.Models;
using Avangarde.KeyboardExtenderPlugins;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avangarde.KeyboardExtender.Project
{
    public sealed class ProjectManager : IDisposable
    {
        protected List<IBehavior> _bindings { get; set; }


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


        public List<ScreenVO> screens = null;
        private GlobalKeyboardHook _globalKeyboardHook;

        public void InitializeScreens()
        {
            if (screens == null)
            {
                screens = new List<ScreenVO>();
            }
            else
            {
                //TODO clean better?
                screens = new List<ScreenVO>();
            }

            foreach (Screen screen in Screen.AllScreens)
            {
                screens.Add(new ScreenVO(screen));
            }
        }

        public bool RunMainLogic()
        {

            SetupKeyboardHooks();
            InitializeBehaviors();

            return true;

        }

        private void InitializeBehaviors()
        {

            if (this._bindings == null)
            {
                this._bindings = new List<IBehavior>();
            }
            

            Type[] typelist = GetTypesInNamespace(Assembly.GetAssembly(typeof(IBehavior)),
                                      "Avangarde.KeyboardExtenderPlugins.Plugins");

            foreach (Type t in typelist)
            {

                ConstructorInfo ctor = t.GetConstructor(Type.EmptyTypes);
                object instance = ctor.Invoke(new object[] { });
                _bindings.Add(instance as IBehavior);
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

            if((e.KeyboardState == GlobalKeyboardHook.KeyboardState.SysKeyUp || e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyUp)){
                if (this._pressedOnCtrlLeft && this._pressedOnAltLeft)
                {
                    if (e.KeyPressed == Keys.M)
                    {
                        this._bindings[0].ExecuteBehavior();
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

        public void Dispose()
        {
            _globalKeyboardHook?.Dispose();
        }
    }
}
