using PdfSharpCore.Drawing;
using System;

namespace Printing.Comon
{
    internal class Shape
    {
        /// <summary>
        /// 直線を描く
        /// </summary>
        /// <param name="mc"></param>
        /// <param name="_pt0">始点座標</param>
        /// <param name="_pt1">終点座標</param>
        /// <param name="_PenWidth"></param>
        static public void DrawLine(PdfDocument mc, XPoint _pt1, XPoint _pt2, double _PenWidth = double.NaN)
        {
            if (!double.IsNaN(_PenWidth))
                mc.xpen.Width = _PenWidth;
            mc.gfx.DrawLine(mc.xpen, _pt1, _pt2);
        }

        static public void DrawLine(PdfDocument mc, XPoint _pt1, XPoint _pt2, double _PenWidth, XColor col)
        {
            mc.xpen.Color = col;
            DrawLine(mc, _pt1, _pt2, _PenWidth);
        }

        /// <summary>
        /// 破線を描く
        /// </summary>
        /// <param name="mc"></param>
        /// <param name="_pt1"></param>
        /// <param name="_pt2"></param>
        /// <param name="_PenWidth"></param>
        /// <param name="_Interval">破線の距離</param>
        static public void DrawDashLine(PdfDocument mc, XPoint _pt1, XPoint _pt2, double _PenWidth, double _Interval)
        {
            var LenX = _pt2.X - _pt1.X;
            var LenY = _pt2.Y - _pt1.Y;
            var Length = (double)Math.Sqrt(Math.Pow(LenX, 2) + Math.Pow(LenY, 2));
            int num1 = (int)Math.Round(Length / _Interval, 0);
            int num2 = num1 % 2 == 1 ? num1 + 1 : num1;
            int num3 = num2 / 2;
            var IntX = LenX / num2;
            var IntY = LenY / num2;

            XPoint p1 = _pt1;
            XPoint p2 = new XPoint();
            for (int i = 0; i < num3; i++)
            {
                p2.X = p1.X + IntX;
                p2.Y = p1.Y + IntY;
                mc.gfx.DrawLine(mc.xpen, p1, p2);
                p1.X = p2.X + IntX;
                p1.Y = p2.Y + IntY;
            }
        }

        /// <summary>
        /// 円を描く
        /// </summary>
        /// <param name="mc"></param>
        /// <param name="_pt0"></param>
        /// <param name="_size"></param>
        /// <param name="_PenWidth"></param>
        static public void Drawcircle(PdfDocument mc, XPoint _pt0, XSize _size)
        {
            mc.gfx.DrawEllipse(mc.xpen, _pt0.X, _pt0.Y, _size.Width, _size.Height);
        }

        const double arrowH = 3;
        const double arrowW = 1;

        /// <summary>
        /// 連続した直線を描く
        /// </summary>
        /// <param name="mc"></param>
        /// <param name="_points"></param>
        /// <param name="_PenWidth"></param>
        /// <param name="dashPattern">破線パターン</param>
        /// <param name="startCap">始点形状</param>
        /// <param name="endCap">終点形状</param>
        static public void DrawLines(PdfDocument mc, XPoint[] _points, double _PenWidth = double.NaN,
            double[] dashPattern = null,
            LineCap startCap = LineCap.NoAnchor, LineCap endCap = LineCap.NoAnchor)
        {
            if (_points.Length < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(_points));
            }

            if (!double.IsNaN(_PenWidth))
                mc.xpen.Width = _PenWidth;

            var bkup = mc.xpen.DashPattern;
            mc.xpen.DashPattern = dashPattern ?? Array.Empty<double>();
            try
            {
                mc.gfx.DrawLines(mc.xpen, _points);
            }
            finally
            {
                mc.xpen.DashPattern = bkup;
            }
            if (startCap == LineCap.ArrowAnchor)
            {
                var _pt0 = _points[0];
                var _pt1 = _points[1];

                var vector = _pt0 - _pt1;
                var theta = Math.Atan2(vector.Y, vector.X);
                var mat = new XMatrix();
                mat.RotateAtAppend(theta * 180 / Math.PI, _pt0);
                mc.gfx.DrawLine(mc.xpen, _pt0, mat.Transform(new XPoint(_pt0.X - arrowH, _pt0.Y - arrowW)));
                mc.gfx.DrawLine(mc.xpen, _pt0, mat.Transform(new XPoint(_pt0.X - arrowH, _pt0.Y + arrowW)));
            }
            if (endCap == LineCap.ArrowAnchor)
            {
                var _pt0 = _points[_points.Length - 2];
                var _pt1 = _points[_points.Length - 1];

                var vector = _pt1 - _pt0;
                var theta = Math.Atan2(vector.Y, vector.X);
                var mat = new XMatrix();
                mat.RotateAtAppend(theta * 180 / Math.PI, _pt1);
                mc.gfx.DrawLine(mc.xpen, _pt1, mat.Transform(new XPoint(_pt1.X - arrowH, _pt1.Y - arrowW)));
                mc.gfx.DrawLine(mc.xpen, _pt1, mat.Transform(new XPoint(_pt1.X - arrowH, _pt1.Y + arrowW)));
            }
        }

        /// <summary>
        /// 多角形を描く(始点と終点の間にも直線を描画)
        /// </summary>
        /// <param name="mc"></param>
        /// <param name="_points"></param>
        /// <param name="_PenWidth"></param>
        static public void DrawPolygon(PdfDocument mc, XPoint[] _points, double _PenWidth = double.NaN)
        {
            if (!double.IsNaN(_PenWidth))
                mc.xpen.Width = _PenWidth;
            mc.gfx.DrawPolygon(mc.xpen, _points);
        }

        /// <summary>
        /// 円弧を描く(現状は真円の円弧のみに対応)
        /// </summary>
        /// <param name="mc"></param>
        /// <param name="_pt0"></param>
        /// <param name="_pt1"></param>
        /// <param name="startAngle">描画開始角度(X軸の正方向から時計回りを正とする。単位は度)</param>
        /// <param name="sweepAngel">円弧の描画角度(単位は度)</param>
        /// <param name="startCap">始点形状</param>
        /// <param name="endCap">終点形状</param>
        static public void DrawArc(PdfDocument mc, XPoint _pt0, XPoint _pt1, double startAngle, double sweepAngel,
            LineCap startCap = LineCap.NoAnchor, LineCap endCap = LineCap.NoAnchor)
        {
            var rect = new XRect(_pt0, _pt1);

            // 幅と高さが小数点以下5桁まで一致すれば一先ず正方形とみなす
            if (Math.Round(rect.Width, 5, MidpointRounding.AwayFromZero) != Math.Round(rect.Height, 5, MidpointRounding.AwayFromZero))
                throw new Exception("sorry, perfect circle only.");

            mc.gfx.DrawArc(mc.xpen, rect, startAngle, sweepAngel);

            // 以下の処理は真円専用
            var radius = rect.Width / 2;
            var center = rect.Center;
            if (startCap == LineCap.ArrowAnchor)
            {
                var angle = startAngle;
                var p = center + radius * new XVector(Math.Cos(angle / 180 * Math.PI), Math.Sin(angle / 180 * Math.PI));
                var mat = new XMatrix();
                mat.RotateAtAppend(angle - 90, p);
                mc.gfx.DrawLine(mc.xpen, p, mat.Transform(new XPoint(p.X - arrowH, p.Y - arrowW)));
                mc.gfx.DrawLine(mc.xpen, p, mat.Transform(new XPoint(p.X - arrowH, p.Y + arrowW)));
            }
            if (endCap == LineCap.ArrowAnchor)
            {
                var angle = startAngle + sweepAngel;
                var p = center + radius * new XVector(Math.Cos(angle / 180 * Math.PI), Math.Sin(angle / 180 * Math.PI));
                var mat = new XMatrix();
                mat.RotateAtAppend(angle + 90, p);
                mc.gfx.DrawLine(mc.xpen, p, mat.Transform(new XPoint(p.X - arrowH, p.Y - arrowW)));
                mc.gfx.DrawLine(mc.xpen, p, mat.Transform(new XPoint(p.X - arrowH, p.Y + arrowW)));
            }
        }
    }
}
