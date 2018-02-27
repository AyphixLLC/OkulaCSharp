using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using System.Drawing;

namespace Okkula {
    public class Sprite {
        public Vector2 Position { 
            get; 
            set;
        }
        public Vector2 Scale { 
            get; 
            set; 
        }

        public RectangleF TexRect {
            get;
            set;
        }

        public Texture Texture {
            get;
            set;
        }

        public Matrix4 ModelMatrix {
            get;
            set;
        }

        public Matrix4 ModelViewProjectionMatrix {
            get;
            set;
        }

        private float maxDist {
            get;
            set;
        }

        public SizeF Size {
            get {
                return new SizeF(Scale.X, Scale.Y);
            }
            set {
                Scale = new Vector2(value.Width, value.Height);
                maxDist = (float)Math.Sqrt(this.Scale.X * this.Scale.X + this.Scale.Y * this.Scale.Y);
            }
        }

        private Window Window {
            get;
            set;
        }

        public void CalculateModelMatrix() {
            Vector3 translation = new Vector3();

            translation = new Vector3(Position.X - Window.Width / 2 - 0, Position.Y - Window.Height - 0, 0.0f);
            ModelMatrix = Matrix4.CreateScale(Scale.X, Scale.Y, 1.0f) * Matrix4.CreateRotationZ(0) * Matrix4.CreateTranslation(translation);
        }

        public Sprite(Texture texture, int width, int height) {
            this.Texture = texture;
            this.Size = new Size(width, height);
        }

        public static Vector2[] GetVerticies() {
            return new Vector2[] {
                new Vector2(-0.5f, -0.5f),
                new Vector2(-0.5f, 0.5f),
                new Vector2(0.5f, 0.5f),
                new Vector2(0.5f, -0.5f)
            };
        }

        public Vector2[] GetTexCoords() {
            return new Vector2[] {
                new Vector2(TexRect.Left,TexRect.Bottom),
                new Vector2(TexRect.Left, TexRect.Top),
                new Vector2(TexRect.Right, TexRect.Top),
                new Vector2(TexRect.Right, TexRect.Bottom)
            };
        }

        public int[] GetIndicies(int offset = 0) {
            int[] indices = new int[] { 0, 1, 2, 0, 2, 3 };

            if (offset != 0) {
                for (int i = 0; i < indices.Length; i++) {
                    indices[i] += offset;
                }
            }

            return indices;
        }

        public bool IsVisible {
            get {
                return Position.X + LongestSide > 0 && Position.X - LongestSide < 0 + Window.Width && Position.Y + LongestSide > 0 && Position.Y - LongestSide < 0 + Window.Height;
            }
        }

        public float HalfWidth { 
            get { 
                return Scale.X / 2.0f; 
            } 
        }

        public float HalfHeight { 
            get { 
                return Scale.Y / 2.0f; 
            } 
        }

        public float LongestSide {
            get {
                return Math.Max(Size.Width, Size.Height);
            }
        }

        public Vector2 TopLeft {
            get {
                return new Vector2((float)((-HalfWidth) * Math.Cos(0) - (-HalfHeight) * Math.Sin(0)), (float)((-HalfWidth) * Math.Sin(0) + (-HalfHeight) * Math.Cos(0))) + Position;
            }
        }

        public Vector2 TopRight {
            get {
                return new Vector2((float)((HalfWidth) * Math.Cos(0) - (-HalfHeight) * Math.Sin(0)), (float)((HalfWidth) * Math.Sin(0) + (-HalfHeight) * Math.Cos(0))) + Position;
            }
        }

        public Vector2 BottomLeft {
            get {
                return new Vector2((float)((-HalfWidth) * Math.Cos(0) - (HalfHeight) * Math.Sin(0)), (float)((-HalfWidth) * Math.Sin(0) + (HalfHeight) * Math.Cos(0))) + Position;
            }
        }

        public Vector2 BottomRight {
            get {
                return new Vector2((float)((HalfWidth) * Math.Cos(0) - (HalfHeight) * Math.Sin(0)), (float)((HalfWidth) * Math.Sin(0) + (HalfHeight) * Math.Cos(0))) + Position;
            }
        }

        public bool IsInside(Vector2 point) {
            Vector2 AP = point - TopLeft;
            Vector2 AB = TopRight - TopLeft;
            Vector2 AD = BottomLeft - TopLeft;

            return (0 < Vector2.Dot(AP, AB) && Vector2.Dot(AP, AB) < Vector2.Dot(AB, AB) && 0 < Vector2.Dot(AP, AD) && Vector2.Dot(AP, AD) < Vector2.Dot(AD, AD));
        }
    }
}
