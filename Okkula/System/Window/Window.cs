using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Platform.Windows;
using OpenTK.Graphics.OpenGL4;
using OpenTK;

namespace Okkula {
    public class Window {
        private OpenTK.GameWindow InternalWindow {
            get;
            set;
        }

        private string _title = "";
        public string Title {
            get {
                return _title;
            }
            set {
                _title = value;
                if (this.InternalWindow != null) {
                    this.InternalWindow.Title = value;
                }
            }
        }

        public bool IsOpen {
            get {
                if (this.InternalWindow != null) {
                    return !this.InternalWindow.IsExiting;
                } else {
                    return true;
                }
            }
        }

        private System.Drawing.Color _bgColor = System.Drawing.Color.FromArgb(255, 230, 230, 230);
        public System.Drawing.Color BackgroundColor {
            get {
                return _bgColor;
            }
            set {
                _bgColor = value;
            }
        }

        private int _width = 800;
        public int Width {
            get {
                return _width;
            }
            set {
                _width = 800;
                if (this.InternalWindow != null) {
                    this.InternalWindow.Width = value;
                }
            }
        }

        private int _height = 600;
        public int Height {
            get {
                return _height;
            }
            set {
                _height = 800;
                if (this.InternalWindow != null) {
                    this.InternalWindow.Height = value;
                }
            }
        }

        public Matrix4 Ortho {
            get;
            private set;
        }

        private List<Widget> _children = new List<Widget>();

        public Window() {

        }

        public void Initialize() {
            this.InternalWindow = new OpenTK.GameWindow(this.Width, this.Height, OpenTK.Graphics.GraphicsMode.Default, this.Title, GameWindowFlags.Default, DisplayDevice.Default, 4, 0, OpenTK.Graphics.GraphicsContextFlags.ForwardCompatible);
            this.InternalWindow.MakeCurrent();
            this.InternalWindow.UpdateFrame += Update;
            this.InternalWindow.Resize += InternalWindow_Resize;
            this.InternalWindow.Load += InternalWindow_Load;
            this.InternalWindow.Closing += InternalWindow_Closing;
            this.InternalWindow.Closed += InternalWindow_Closed;
            this.Ortho = Matrix4.CreateOrthographic(this.Width, this.Height, 1.0f, 50.0f);
            this.InternalWindow.Run(30, 30);
        }

        void InternalWindow_Closed(object sender, EventArgs e) {
            this.OnClosed(sender, e);
        }

        void InternalWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            this.OnClosing(sender, e);
        }

        void InternalWindow_Load(object sender, EventArgs e) {
            this.OnLoad(sender, e);
        }

        private void Update(object sender, FrameEventArgs e) {
            GL.Viewport(0, 0, this.Width, this.Height);
            GL.ClearColor(this.BackgroundColor);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            this.OnUpdate(sender, e);
            GL.Flush();
            this.InternalWindow.SwapBuffers();
            this.InternalWindow.ProcessEvents();
        }

        void InternalWindow_Resize(object sender, EventArgs e) {
            OnResize(sender, e);
        }

        public virtual void OnLoad(object sender, EventArgs e) { }

        public virtual void OnResize(object sender, EventArgs e) { }

        public virtual void OnClosing(object sender, EventArgs e) { }

        public virtual void OnClosed(object sender, EventArgs e) { }

        public virtual void OnUpdate(object shader, EventArgs e) { }
    }
}
