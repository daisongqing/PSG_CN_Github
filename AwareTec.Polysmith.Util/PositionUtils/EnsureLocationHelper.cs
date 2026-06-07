using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AwareTec.Polysmith.Util
{
    public class EnsureLocationHelper
    {
        public static Point CalculateSituableLocation(Form form)
        {
            RectPosition formPosition = new RectPosition(new Rectangle(form.Location.X, form.Location.Y, form.Width, form.Height));
            RectPosition screenPosition = new RectPosition(Screen.PrimaryScreen.WorkingArea);

            BeyondClientSituation situation = GetBeyondClientSituation(formPosition, screenPosition);

            if (situation == BeyondClientSituation.None)
                return form.Location;

            switch (situation)
            {
                case BeyondClientSituation.LeftTop:
                    return screenPosition.LeftTopPoint;
                    break;
                case BeyondClientSituation.Left:
                    return new Point(screenPosition.LeftTopPoint.X,
                                    formPosition.LeftTopPoint.Y);
                    break;
                case BeyondClientSituation.LeftBottom:
                    Point point = (new RectPosition(PointType.LeftBottom,
                                                    screenPosition.LeftBottomPoint,
                                                    formPosition.Width,
                                                    formPosition.Height)).LeftTopPoint;

                    return new Point(point.X, point.Y) ;
                    break;
                case BeyondClientSituation.Bottom:
                    return new Point(formPosition.LeftTopPoint.X,
                                    (int)(screenPosition.BottomVal - formPosition.Height));
                    break;
                case BeyondClientSituation.RightBottom:
                    Point p = (new RectPosition(PointType.RightBottom,
                                                screenPosition.RightBottomPoint,
                                                formPosition.Width,
                                                formPosition.Height)).LeftTopPoint;

                    return new Point(p.X, p.Y);
                    break;
                case BeyondClientSituation.Right:
                    return new Point((int)(screenPosition.RightVal - formPosition.Width),
                                              formPosition.LeftTopPoint.Y);
                    break;
                case BeyondClientSituation.RightTop:
                    return (new RectPosition(PointType.RightTop,
                                                    screenPosition.RightTopPoint,
                                                    formPosition.Width,
                                                    formPosition.Height)).LeftTopPoint;
                    break;
                case BeyondClientSituation.Top:
                    return new Point(formPosition.LeftTopPoint.X,
                                              (int)(screenPosition.TopVal + formPosition.Height));
                    break;
                default:
                    return form.Location;
                    break;
            }
        }

        public class RectPosition
        {
            private Point _leftTopPoint;
            private Point _leftBottomPoint;
            private Point _rightTopPoint;
            private Point _rightBottomPoint;
            private Point _centerPoint;
            private float _topVal;
            private float _bottomVal;
            private float _leftVal;
            private float _rightVal;
            private int _width;
            private int _height;

            public Point LeftTopPoint => _leftTopPoint;
            public Point LeftBottomPoint => _leftBottomPoint;
            public Point RightTopPoint => _rightTopPoint;
            public Point RightBottomPoint => _rightBottomPoint;
            public Point CenterPoint => _centerPoint;
            public float TopVal => _topVal;
            public float BottomVal => _bottomVal;
            public float LeftVal => _leftVal;
            public float RightVal => _rightVal;
            public int Width => _width;
            public int Height => _height;

            private void Init(PointType pointType,
                              Point point,
                              int width,
                              int height)
            {
                switch (pointType)
                {
                    case PointType.LeftTop:
                        _leftTopPoint = point;
                        break;
                    case PointType.LeftBottom:
                        _leftTopPoint = new Point(point.X, point.Y - height);
                        break;
                    case PointType.RightTop:
                        _leftTopPoint = new Point(point.X - width, point.Y);
                        break;
                    case PointType.RightBottom:
                        _leftTopPoint = new Point(point.X - width, point.Y - height);
                        break;
                }

                _leftBottomPoint = new Point(_leftTopPoint.X, _leftTopPoint.Y + height);
                _rightTopPoint = new Point(_leftTopPoint.X + width, _leftTopPoint.Y);
                _rightBottomPoint = new Point(_rightTopPoint.X, _leftBottomPoint.Y);
                _centerPoint = new Point(_leftTopPoint.X + width / 2,
                                         _leftTopPoint.Y + height / 2);

                _topVal = _leftTopPoint.Y;
                _bottomVal = _leftBottomPoint.Y;
                _leftVal = _leftTopPoint.X;
                _rightVal = _rightTopPoint.X;
                _width = width;
                _height = height;
            }

            public RectPosition(PointType pointType,
                                Point point,
                                int width,
                                int height)
            {
                Init(pointType, point, width, height);
            }

            public RectPosition(Rectangle rect)
            {
                Init(PointType.LeftTop, rect.Location, rect.Width, rect.Height);
            }
        }

        public enum BeyondClientSituation
        {
            LeftTop,
            Top,
            RightTop,
            Right,
            RightBottom,
            Bottom,
            LeftBottom,
            Left,
            None
        }

        public enum PointType
        {
            LeftTop,
            LeftBottom,
            RightTop,
            RightBottom
        }

        public static BeyondClientSituation GetBeyondClientSituation(RectPosition rectPosition,
                                                                     RectPosition screenPosition)
        {
            List<BeyondClientSituation> beyonds = new List<BeyondClientSituation>();

            //情况1：窗体左上角被遮挡
            if ((!Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.LeftTopPoint)) &&
               (!Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.LeftBottomPoint)) &&
               (!Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.RightTopPoint)) &&
               (Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.RightBottomPoint)))
                beyonds.Add(BeyondClientSituation.LeftTop);
            //情况2：窗体上角被遮挡
            if ((!Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.LeftTopPoint)) &&
               (!Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.RightTopPoint)) &&
               (Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.LeftBottomPoint)) &&
               (Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.RightBottomPoint)))
                beyonds.Add(BeyondClientSituation.Top);
            //情况3：窗体右上角被遮挡
            if ((!Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.LeftTopPoint)) &&
               (!Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.RightTopPoint)) &&
               (!Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.RightBottomPoint)) &&
               (Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.LeftBottomPoint)))
                beyonds.Add(BeyondClientSituation.RightTop);
            //情况4：窗体左角被遮挡
            if ((!Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.RightTopPoint)) &&
               (!Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.RightBottomPoint)) &&
               (Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.LeftTopPoint)) &&
               (Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.LeftBottomPoint)))
                beyonds.Add(BeyondClientSituation.Right);
            //情况5：窗体右下角被遮挡
            if ((!Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.RightTopPoint)) &&
               (!Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.RightBottomPoint)) &&
               (!Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.LeftBottomPoint)) &&
               (Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.LeftTopPoint)))
                beyonds.Add(BeyondClientSituation.RightBottom);
            //情况6：窗体下角被遮挡
            if ((!Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.LeftBottomPoint)) &&
               (!Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.RightBottomPoint)) &&
               (Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.LeftTopPoint)) &&
               (Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.RightTopPoint)))
                beyonds.Add(BeyondClientSituation.Bottom);
            //情况7：窗体左下角被遮挡
            if ((!Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.LeftTopPoint)) &&
               (!Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.LeftBottomPoint)) &&
               (!Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.RightBottomPoint)) &&
               (Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.RightTopPoint)))
                beyonds.Add(BeyondClientSituation.LeftBottom);
            //情况8：窗体左角被遮挡
            if ((!Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.LeftTopPoint)) &&
               (!Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.LeftBottomPoint)) &&
               (Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.RightTopPoint)) &&
               (Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.RightBottomPoint)))
                beyonds.Add(BeyondClientSituation.Left);
            //情况9: 窗体全被遮挡
            if ((!Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.LeftTopPoint)) &&
               (!Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.LeftBottomPoint)) &&
               (!Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.RightTopPoint)) &&
               (!Screen.PrimaryScreen.WorkingArea.Contains(rectPosition.RightBottomPoint)))
            {
                //判断处于哪个方位:
                if (rectPosition.LeftVal < screenPosition.LeftVal &&
                   rectPosition.TopVal < screenPosition.TopVal)
                    return BeyondClientSituation.LeftTop;

                if (rectPosition.LeftVal < screenPosition.LeftVal &&
                   rectPosition.TopVal >= screenPosition.TopVal &&
                   rectPosition.BottomVal <= screenPosition.BottomVal)
                    return BeyondClientSituation.Left;

                if (rectPosition.LeftVal < screenPosition.LeftVal &&
                   rectPosition.BottomVal > screenPosition.BottomVal)
                    return BeyondClientSituation.LeftBottom;

                if (rectPosition.LeftVal >= screenPosition.LeftVal &&
                    rectPosition.RightVal <= screenPosition.RightVal &&
                    rectPosition.BottomVal > screenPosition.BottomVal)
                    return BeyondClientSituation.Bottom;

                if (rectPosition.RightVal > screenPosition.RightVal &&
                   rectPosition.BottomVal > screenPosition.BottomVal)
                    return BeyondClientSituation.RightBottom;

                if (rectPosition.RightVal > screenPosition.RightVal &&
                   rectPosition.TopVal >= screenPosition.TopVal &&
                   rectPosition.BottomVal <= screenPosition.BottomVal)
                    return BeyondClientSituation.Right;

                if (rectPosition.RightVal > screenPosition.RightVal &&
                    rectPosition.TopVal < screenPosition.TopVal)
                    return BeyondClientSituation.RightTop;

                if (rectPosition.LeftVal >= screenPosition.LeftVal &&
                    rectPosition.RightVal <= screenPosition.RightVal &&
                    rectPosition.TopVal < screenPosition.TopVal)
                    return BeyondClientSituation.Top;
            }

            if (beyonds.Count == 1) return beyonds[0];
            if (beyonds.Count == 0) return BeyondClientSituation.None;
            throw new Exception("超出限定情况");
        }

    }
}
