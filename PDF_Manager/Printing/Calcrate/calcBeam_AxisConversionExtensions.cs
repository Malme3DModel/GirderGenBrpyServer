using PdfSharpCore.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace Printing.Calcrate
{
    static class AxisConversionExtensions
    {
        /// <summary>
        /// y軸に平行な直線に関して対称な点に変換
        /// </summary>
        /// <param name="points"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static XPoint[] YAxisSymmetry(this IEnumerable<XPoint> points, double x) => points.Select(p => new XPoint(-p.X + 2 * x, p.Y)).ToArray();

        /// <summary>
        /// y軸に平行な直線に関して対称な点に変換
        /// </summary>
        /// <param name="points"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static XPoint YAxisSymmetry(this XPoint point, double x) => new(-point.X + 2 * x, point.Y);

        /// <summary>
        /// 指定された点に関して点対称な点に変換
        /// </summary>
        /// <param name="points"></param>
        /// <param name="center"></param>
        /// <returns></returns>
        public static XPoint[] PointSymmetry(this IEnumerable<XPoint> points, XPoint center) => points.Select(p => new XPoint(-p.X + 2 * center.X, -p.Y + 2 * center.Y)).ToArray();

        /// <summary>
        /// 指定された点に関して点対称な点に変換
        /// </summary>
        /// <param name="point"></param>
        /// <param name="center"></param>
        /// <returns></returns>
        public static XPoint PointSymmetry(this XPoint point, XPoint center) => new(-point.X + 2 * center.X, -point.Y + 2 * center.Y);
    }
}
