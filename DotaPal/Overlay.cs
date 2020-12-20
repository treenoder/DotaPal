using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using GameOverlay.Drawing;
using GameOverlay.Windows;

namespace DotaPal
{
    public class Overlay : IDisposable
    {
        private const int GWL_STYLE = -16;
        private const int WS_BORDER = 0x00800000; // thin border
        private const int WS_THICKFRAME = 0x00040000; // sizing (thick) border

        private readonly Dictionary<string, SolidBrush> _brushes;
        private readonly Dictionary<string, Font> _fonts;
        private readonly Dictionary<string, Image> _images;
        private Vector2 _clientResolution = new(1920f, 1080f);
        private readonly Vector2 _designResolution = new(1920f, 1080f);
        private readonly SharedData _sharedData;
        private float _sizeCoefficient = 1f;

        private bool _windowHaveBorders;


        private RECT _windowRect;

        private readonly float _windowResizeTimer = 30f;
        private float _windowResizeTimerCurrent;

        public Overlay(SharedData sharedData)
        {
            _sharedData = sharedData;
            _brushes = new Dictionary<string, SolidBrush>();
            _fonts = new Dictionary<string, Font>();
            _images = new Dictionary<string, Image>();

            var gfx = new Graphics
            {
                PerPrimitiveAntiAliasing = true,
                TextAntiAliasing = true
            };

            Window = new GraphicsWindow(0, 0, 800, 600, gfx)
            {
                FPS = 30,
                IsTopmost = false,
                IsVisible = true
            };

            Window.DestroyGraphics += _window_DestroyGraphics;
            Window.DrawGraphics += _window_DrawGraphics;
            Window.SetupGraphics += _window_SetupGraphics;
        }


        public GraphicsWindow Window { get; }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(HandleRef hWnd, out RECT lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        private void _window_SetupGraphics(object sender, SetupGraphicsEventArgs e)
        {
            var gfx = e.Graphics;

            if (e.RecreateResources)
            {
                foreach (var pair in _brushes) pair.Value.Dispose();
                foreach (var pair in _images) pair.Value.Dispose();
            }

            _brushes["black"] = gfx.CreateSolidBrush(0, 0, 0);
            _brushes["white"] = gfx.CreateSolidBrush(255, 255, 255);
            _brushes["red"] = gfx.CreateSolidBrush(255, 0, 0);
            _brushes["green"] = gfx.CreateSolidBrush(0, 255, 0);
            _brushes["blue"] = gfx.CreateSolidBrush(0, 0, 255);
            _brushes["background"] = gfx.CreateSolidBrush(0x33, 0x36, 0x3F);
            _brushes["playerCooldownText"] = gfx.CreateSolidBrush(255, 255, 255);
            _brushes["playerCooldownBG"] = gfx.CreateSolidBrush(28, 32, 35, 100);
            _brushes["transparent"] = gfx.CreateSolidBrush(0x0, 0x0, 0x0, 0x0);
            _brushes["grid"] = gfx.CreateSolidBrush(255, 255, 255, 0.2f);
            _brushes["random"] = gfx.CreateSolidBrush(0, 0, 0);

            if (e.RecreateResources) return;

            _fonts["arial"] = gfx.CreateFont("Arial", 12);
            _fonts["consolas"] = gfx.CreateFont("Consolas", 14);
        }

        private void _window_DestroyGraphics(object sender, DestroyGraphicsEventArgs e)
        {
            foreach (var pair in _brushes) pair.Value.Dispose();
            foreach (var pair in _fonts) pair.Value.Dispose();
            foreach (var pair in _images) pair.Value.Dispose();
        }

        private void _window_DrawGraphics(object sender, DrawGraphicsEventArgs e)
        {
            _windowResizeTimerCurrent--;
            if (_windowResizeTimerCurrent <= 0)
            {
                _windowResizeTimerCurrent = _windowResizeTimer;
                ResizeWindow();
            }

            var gfx = e.Graphics;

            gfx.ClearScene(_brushes["transparent"]);

            DrawCooldownLabels(gfx);
        }

        private void DrawCooldownLabels(Graphics gfx)
        {
            var vBorderOffset = 45;
            var offset = new Vector2(1080, 80);
            if (_sharedData.IsOnDarkSide) offset.X = 570;
            if (_windowHaveBorders) offset.Y += vBorderOffset;

            string text = "";
            var position = new Vector2();
            var cooldown = 0;
            for (var i = 0; i < 5; i++)
            {
                cooldown = _sharedData.Cooldown[(Slots) i];
                if (cooldown <= 0) continue;
                text = cooldown.ToString();
                position.X = offset.X * _sizeCoefficient + 62 * _sizeCoefficient * i;
                position.Y = offset.Y * _sizeCoefficient;

                gfx.DrawTextWithBackground(
                    _fonts["consolas"],
                    _brushes["playerCooldownText"],
                    _brushes["playerCooldownBG"],
                    position.X, position.Y,
                    text
                );
            }

            cooldown = _sharedData.Cooldown[Slots.Extra1];
            if (cooldown > 0)
            {
                offset = new Vector2(410, 1000);
                if (_windowHaveBorders) offset.Y += vBorderOffset;
                text = _sharedData.Cooldown[Slots.Extra1].ToString();
                position.X = offset.X * _sizeCoefficient;
                position.Y = offset.Y * _sizeCoefficient;

                gfx.DrawTextWithBackground(
                    _fonts["consolas"],
                    _brushes["playerCooldownText"],
                    _brushes["playerCooldownBG"],
                    position.X, position.Y,
                    text
                );
            }

            cooldown = _sharedData.Cooldown[Slots.Extra2];
            if (cooldown > 0)
            {
                offset = new Vector2(1510, 1000);
                if (_windowHaveBorders) offset.Y += vBorderOffset;

                text = _sharedData.Cooldown[Slots.Extra2].ToString();
                position.X = offset.X * _sizeCoefficient;
                position.Y = offset.Y * _sizeCoefficient;

                gfx.DrawTextWithBackground(
                    _fonts["consolas"],
                    _brushes["playerCooldownText"],
                    _brushes["playerCooldownBG"],
                    position.X, position.Y,
                    text
                );
            }
        }

        private bool NativeWindowHasBorder(IntPtr hWnd)
        {
            return (GetWindowLong(hWnd, GWL_STYLE) & (WS_BORDER | WS_THICKFRAME)) != 0;
        }

        private void ResizeWindow()
        {
            var handle = GetForegroundWindow();

            GetWindowRect(new HandleRef(null, handle), out _windowRect);
            Window.FitTo(handle);
            Window.PlaceAbove(handle);

            _windowHaveBorders = NativeWindowHasBorder(handle);

            _clientResolution.X = _windowRect.Right - _windowRect.Left;
            _clientResolution.Y = -_windowRect.Bottom - _windowRect.Top;
            _sizeCoefficient = _clientResolution.X / _designResolution.X;
        }

        public void Run()
        {
            Window.Create();
            Window.Join();
        }

        ~Overlay()
        {
            Dispose(false);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left; // x position of upper-left corner
            public int Top; // y position of upper-left corner
            public int Right; // x position of lower-right corner
            public int Bottom; // y position of lower-right corner
        }

        #region IDisposable Support

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                Window.Dispose();

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}