using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows.Forms;

namespace DotaPal
{
    public partial class Form1 : Form
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_SYSKEYUP = 0x0105;
        private const int MAX_COOLDOWN = 999;


        private static IntPtr _hookID = IntPtr.Zero;
        private readonly HKAction[] _actions;
        private Action _activeAction = Action.None;
        private Keys _activeModifier = Keys.None;

        private I18N _i18N;

        private readonly LowLevelKeyboardProc _proc;

        private readonly SharedData _sharedData;
        private Overlay overlay;

        public Form1()
        {
            _proc = KeyboardProc;
            _hookID = SetHook(_proc);
            _sharedData = new SharedData();

            _sharedData.Cooldown[Slots.Hero1] = 0;
            _sharedData.Cooldown[Slots.Hero2] = 0;
            _sharedData.Cooldown[Slots.Hero3] = 0;
            _sharedData.Cooldown[Slots.Hero4] = 0;
            _sharedData.Cooldown[Slots.Hero5] = 0;
            _sharedData.Cooldown[Slots.Extra1] = 0;
            _sharedData.Cooldown[Slots.Extra2] = 0;

            InitializeComponent();
            _actions = new[]
            {
                new HKAction(Action.AddCooldown, Slots.Hero1, Keys.LMenu, Keys.F1, labelAddHero1),
                new HKAction(Action.AddCooldown, Slots.Hero2, Keys.LMenu, Keys.F2, labelAddHero2),
                new HKAction(Action.AddCooldown, Slots.Hero3, Keys.LMenu, Keys.F3, labelAddHero3),
                new HKAction(Action.AddCooldown, Slots.Hero4, Keys.LMenu, Keys.F4, labelAddHero4),
                new HKAction(Action.AddCooldown, Slots.Hero5, Keys.LMenu, Keys.F5, labelAddHero5),
                new HKAction(Action.AddCooldown, Slots.Extra1, Keys.LMenu, Keys.F6, labelAddExtra1),
                new HKAction(Action.AddCooldown, Slots.Extra2, Keys.LMenu, Keys.F7, labelAddExtra2),
                new HKAction(Action.ChangeSide, Slots.None, Keys.None, Keys.F8, labelChangeSide),
                new HKAction(Action.ToggleOverlay, Slots.None, Keys.None, Keys.F12, labelToggleOverlay),
                new HKAction(Action.ResetCooldown, Slots.Hero1, Keys.LControlKey, Keys.F1, labelResetHero1),
                new HKAction(Action.ResetCooldown, Slots.Hero2, Keys.LControlKey, Keys.F2, labelResetHero2),
                new HKAction(Action.ResetCooldown, Slots.Hero3, Keys.LControlKey, Keys.F3, labelResetHero3),
                new HKAction(Action.ResetCooldown, Slots.Hero4, Keys.LControlKey, Keys.F4, labelResetHero4),
                new HKAction(Action.ResetCooldown, Slots.Hero5, Keys.LControlKey, Keys.F5, labelResetHero5),
                new HKAction(Action.ResetCooldown, Slots.Extra1, Keys.LControlKey, Keys.F6, labelResetExtra1),
                new HKAction(Action.ResetCooldown, Slots.Extra2, Keys.LControlKey, Keys.F7, labelResetExtra2)
            };
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod,
            uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CultureInfo ci = CultureInfo.InstalledUICulture;
            var lang = Language.En;
            if (ci.TwoLetterISOLanguageName == "ru") lang = Language.Ru;

            _i18N = new I18N(lang);
            overlay = new Overlay(_sharedData);
            overlay.Window.Create();
            ChangeLanguage(lang);
        }

        private void ChangeLanguage(Language lang)
        {
            _i18N.SetLanguage(lang);
            if (_i18N.GetLanguage() == Language.Ru)
            {
                btnLangRu.BackColor = Color.White;
                btnLangEn.BackColor = SystemColors.Control;
            }
            else if (_i18N.GetLanguage() == Language.En)
            {
                btnLangEn.BackColor = Color.White;
                btnLangRu.BackColor = SystemColors.Control;
            }

            groupAdd.Text = _i18N.Translate(Action.AddCooldown.ToString());
            groupReset.Text = _i18N.Translate(Action.ResetCooldown.ToString());
            groupChangeSide.Text = _i18N.Translate(Action.ChangeSide.ToString());
            groupToggle.Text = _i18N.Translate(Action.ToggleOverlay.ToString());
            foreach (var hkAction in _actions)
            {
                if (hkAction.Label == null) continue;

                if (hkAction.Slot != Slots.None)
                {
                    var slotName = hkAction.Slot.ToString();
                    var slotTranslated = _i18N.Translate(slotName.Remove(slotName.Length - 1));
                    var number = slotName.Last();
                    var translatedHKMod = _i18N.Translate(hkAction.Modifier.ToString());
                    var key = hkAction.Key.ToString();
                    var hk = $"{translatedHKMod}+{key}";
                    hkAction.Label.Text = $"{slotTranslated} {number} - {hk}";
                }
                else if (hkAction.Action == Action.ChangeSide)
                {
                    var key = hkAction.Key.ToString();
                    hkAction.Label.Text = key;
                }
                else if (hkAction.Action == Action.ToggleOverlay)
                {
                    var key = hkAction.Key.ToString();
                    hkAction.Label.Text = key;
                }
            }
        }

        private void ProcessAction(HKAction action)
        {
            switch (action.Action)
            {
                case Action.AddCooldown:
                    _sharedData.Cooldown[action.Slot] += 30;
                    _sharedData.Cooldown[action.Slot] = Math.Min(_sharedData.Cooldown[action.Slot], MAX_COOLDOWN);
                    break;
                case Action.ResetCooldown:
                    _sharedData.Cooldown[action.Slot] = 0;
                    break;
                case Action.ChangeSide:
                    _sharedData.IsOnDarkSide = !_sharedData.IsOnDarkSide;
                    break;
                case Action.ToggleOverlay:
                    if (overlay.Window.IsVisible)
                        overlay.Window.Hide();
                    else
                        overlay.Window.Show();
                    break;
            }
        }

        private IntPtr KeyboardProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                var vkCode = Marshal.ReadInt32(lParam);
                if (wParam == (IntPtr) WM_KEYDOWN || wParam == (IntPtr) WM_SYSKEYDOWN)
                {
                    var key = (Keys) vkCode;
                    foreach (var action in _actions)
                    {
                        if (key == action.Modifier)
                        {
                            _activeModifier = key;
                            break;
                        }

                        if (_activeModifier == action.Modifier && key == action.Key) ProcessAction(action);
                    }
                }
                else if (wParam == (IntPtr) WM_KEYUP || wParam == (IntPtr) WM_SYSKEYUP)
                {
                    if (_activeModifier != Keys.None)
                    {
                        var key = (Keys) vkCode;
                        if (new[] {Keys.LControlKey, Keys.LMenu, Keys.LShiftKey}.Contains(key))
                            _activeModifier = Keys.None;
                    }
                }
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            overlay.Dispose();
            UnhookWindowsHookEx(_hookID);
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void cooldownTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            foreach (var keyValuePair in _sharedData.Cooldown)
            {
                var cooldown = _sharedData.Cooldown[keyValuePair.Key];
                if (cooldown > 0) _sharedData.Cooldown[keyValuePair.Key] -= 1;
            }
        }

        private void btnLangEn_Click(object sender, EventArgs e)
        {
            ChangeLanguage(Language.En);
        }

        private void btnLangRu_Click(object sender, EventArgs e)
        {
            ChangeLanguage(Language.Ru);
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);


        private enum KeyModifier
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            WinKey = 8
        }
    }
}