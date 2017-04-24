using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lemon_App.IApps._2048
{
    /// <summary>
    /// _2048Control.xaml 的交互逻辑
    /// </summary>
    public partial class _2048Control : UserControl
    {
        public _2048Control()
        {
            InitializeComponent();
            NewNum();
            NewNum();
        }

        int[,] Block = new int[4, 4]
{
            {0,0,0,0},
            {0,0,0,0},
            {0,0,0,0},
            {0,0,0,0}
};
        private void NewNum()
        {
            Random random = new Random();
            int num = random.Next(0, 9) > 2 ? 2 : 4;

            int nullnum = 0;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (Block[i, j] == 0)
                        nullnum++;
            if (nullnum < 1)
            {
                return;
            }

            int index = random.Next(1, nullnum);
            nullnum = 0;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (Block[i, j] == 0)
                    {
                        nullnum++;
                        if (nullnum != index) continue;
                        Block[i, j] = num;

                        Color backColor;
                        switch (num)
                        {
                            case 2:
                                backColor = Colors.LightPink;
                                break;
                            default:
                                backColor = Colors.LightSalmon;
                                break;
                        }

                        #region 动画效果
                        Label lbl = (Label)grdMain.Children
                            .Cast<UIElement>()
                            .First(e => Grid.GetRow(e) == i && Grid.GetColumn(e) == j);
                        lbl.Content = num.ToString();
                        lbl.Background = new SolidColorBrush(backColor);
                        DoubleAnimation opacity = new DoubleAnimation();
                        opacity.From = 0;
                        opacity.To = 1;
                        Duration duration = new Duration(TimeSpan.FromMilliseconds(500));
                        opacity.Duration = duration;
                        lbl.BeginAnimation(Label.OpacityProperty, opacity);
                        #endregion

                        if (isGameOver())
                        {
                            v.IsOpen = true;
                            l.Text = "你输惹\r\n" + lblScores.Text;
                        }
                    }
        }

        /// <summary>
        /// 没有可移动的方块，游戏结束
        /// </summary>
        /// <returns></returns>
        private bool isGameOver()
        {
            //行没有相同或0
            for (int row = 0; row < 4; row++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (Block[row, i] == 0)
                    {
                        return false;
                    }
                    else
                    {
                        for (int j = i + 1; j < 4; j++)
                        {
                            if (Block[row, i] == Block[row, j])
                            {
                                return false;
                            }
                            else
                            {
                                if (Block[row, j] > 0)
                                    break;
                            }
                        }
                    }
                }
            }
            for (int col = 0; col < 4; col++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (Block[i, col] == 0)
                    {
                        return false;
                    }
                    else
                    {
                        for (int j = i + 1; j < 4; j++)
                        {
                            if (Block[i, col] == Block[j, col])
                            {
                                return false;
                            }
                            else
                            {
                                if (Block[j, col] > 0)
                                    break;
                            }
                        }
                    }
                }
            }

            return true;
        }
        private void ScoresChanged(int scores)
        {
            if (scores > 0)
            {
                lblPlus.Text = String.Format("+{0}", scores);

                DoubleAnimation top = new DoubleAnimation();
                DoubleAnimation opacity = new DoubleAnimation();
                opacity.AutoReverse = true;
                opacity.From = 0;
                opacity.To = 1;
                top.From = 0;
                top.To = -30;
                Duration duration = new Duration(TimeSpan.FromMilliseconds(500));
                top.Duration = duration;
                opacity.Duration = duration;
                tt.BeginAnimation(TranslateTransform.YProperty, top);
                lblPlus.BeginAnimation(Label.OpacityProperty, opacity);

                lblScores.Text = (int.Parse(lblScores.Text) + scores).ToString();
            }
        }
        enum Direction
        {
            Left,
            Right,
            Up,
            Down
        }

        private void creatBlock(Direction deraction)
        {
            foreach (var uiele in grdMain.Children)
            {
                Label lbl = uiele as Label;
                if (lbl == null) continue;
                int row = Grid.GetRow(lbl);
                int col = Grid.GetColumn(lbl);

                int oldnum = (lbl.Content ?? "").ToString() == "" ? 0 : int.Parse((lbl.Content ?? "0").ToString());

                lbl.Content = Block[row, col] == 0 ? "" : Block[row, col].ToString();


                Color backColor;
                switch (Block[row, col])
                {
                    case 2:
                        backColor = Colors.LightPink;
                        break;
                    case 4:
                        backColor = Colors.LightSalmon;
                        break;
                    case 8:
                        backColor = Colors.Tomato;
                        break;
                    case 16:
                        backColor = Colors.Violet;
                        break;
                    case 32:
                        backColor = Colors.HotPink;
                        break;
                    case 64:
                        backColor = Colors.DarkOrchid;
                        break;
                    case 128:
                        backColor = Colors.Magenta;
                        break;
                    case 256:
                        backColor = Colors.MediumVioletRed;
                        break;
                    case 512:
                        backColor = Colors.PaleGreen;
                        break;
                    case 1024:
                        backColor = Colors.Lime;
                        break;
                    case 2048:
                        backColor = Colors.LightSkyBlue;
                        break;
                    case 4096:
                        backColor = Colors.RoyalBlue;
                        break;
                    default:
                        backColor = Colors.LawnGreen;
                        break;
                }
                lbl.Background = Block[row, col] == 0
                    ? new SolidColorBrush(Colors.White)
                    : new SolidColorBrush(backColor);

                if (oldnum != Block[row, col])
                {
                    TranslateTransform t = lbl.RenderTransform as TranslateTransform;

                    DoubleAnimation opacity = new DoubleAnimation();
                    opacity.From = 0;
                    opacity.To = 1;
                    Duration duration = new Duration(TimeSpan.FromMilliseconds(300));
                    opacity.Duration = duration;
                    lbl.BeginAnimation(Label.OpacityProperty, opacity);

                    DoubleAnimation offset = new DoubleAnimation();
                    offset.Duration = duration;

                    switch (deraction)
                    {
                        case Direction.Right:
                            offset.From = -30;
                            offset.To = 0;
                            t.BeginAnimation(TranslateTransform.XProperty, offset);
                            break;
                        case Direction.Left:
                            offset.From = 30;
                            offset.To = 0;
                            t.BeginAnimation(TranslateTransform.XProperty, offset);
                            break;
                        case Direction.Up:
                            offset.From = 30;
                            offset.To = 0;
                            t.BeginAnimation(TranslateTransform.YProperty, offset);
                            break;
                        default:
                            offset.From = -30;
                            offset.To = 0;
                            t.BeginAnimation(TranslateTransform.YProperty, offset);
                            break;
                    }

                }


            }
        }
        private void Up(object sender, MouseButtonEventArgs e)
        {
            bool isMove = false;
            int scores = 0;
            for (int col = 0; col < 4; col++)
                for (int i = 0; i < 4; i++)
                    for (int j = i + 1; j < 4; j++)
                    {
                        if (Block[i, col] == Block[j, col])
                        {
                            isMove = Block[i, col] > 0 ? true : isMove;

                            Block[i, col] *= 2;
                            Block[j, col] = 0;
                            scores += Block[i, col];

                            break;
                        }
                        if (Block[j, col] > 0)
                        {
                            break;
                        }
                    }
            for (int col = 0; col < 4; col++)
                for (int i = 3; i > 0; i--)
                    if (Block[i - 1, col] == 0)
                    {
                        isMove = Block[i, col] > 0 ? true : isMove;

                        Block[i - 1, col] = Block[i, col];
                        Block[i, col] = 0;
                        if (i < 3)
                        {
                            for (int j = i + 1; j < 4; j++)
                            {
                                Block[j - 1, col] = Block[j, col];
                                Block[j, col] = 0;
                            }
                        }
                    }

            ScoresChanged(scores);
            if (isMove == false)
            {
                return;
            }

            creatBlock(Direction.Up);
            NewNum();

        }

        private void Down(object sender, MouseButtonEventArgs e)
        {
            bool isMove = false;
            int scores = 0;

            for (int col = 0; col < 4; col++)
                for (int i = 3; i > -1; i--)
                    for (int j = i - 1; j > -1; j--)
                    {
                        if (Block[i, col] == Block[j, col])
                        {
                            isMove = Block[i, col] > 0 ? true : isMove;

                            Block[i, col] *= 2;
                            Block[j, col] = 0;

                            scores += Block[i, col];
                            break;
                        }
                        if (Block[j, col] > 0)
                        {
                            break;
                        }
                    }
            for (int col = 0; col < 4; col++)
                for (int i = 0; i < 3; i++)
                    if (Block[i + 1, col] == 0)
                    {
                        isMove = Block[i, col] > 0 ? true : isMove;

                        Block[i + 1, col] = Block[i, col];
                        Block[i, col] = 0;

                        if (i > 0)
                        {
                            for (int j = i - 1; j > -1; j--)
                            {
                                Block[j + 1, col] = Block[j, col];
                                Block[j, col] = 0;
                            }
                        }
                    }

            ScoresChanged(scores);

            if (isMove == false)
            {
                return;
            }

            creatBlock(Direction.Down);
            NewNum();

        }

        private void L(object sender, MouseButtonEventArgs e)
        {
            bool isMove = false;
            int scores = 0;
            for (int row = 0; row < 4; row++)
                for (int i = 0; i < 4; i++)
                    for (int j = i + 1; j < 4; j++)
                    {
                        if (Block[row, i] == Block[row, j])
                        {
                            isMove = Block[row, i] > 0 ? true : isMove;

                            Block[row, i] *= 2;
                            Block[row, j] = 0;
                            scores += Block[row, i];
                            break;
                        }
                        if (Block[row, j] > 0)
                        {
                            break;
                        }
                    }

            ScoresChanged(scores);

            for (int row = 0; row < 4; row++)
                for (int i = 3; i > 0; i--)
                    if (Block[row, i - 1] == 0)
                    {
                        isMove = Block[row, i] > 0 ? true : isMove;

                        Block[row, i - 1] = Block[row, i];
                        Block[row, i] = 0;
                        if (i < 3)
                        {
                            for (int j = i + 1; j < 4; j++)
                            {
                                Block[row, j - 1] = Block[row, j];
                                Block[row, j] = 0;
                            }
                        }
                    }

            if (isMove == false)
            {
                return;
            }

            creatBlock(Direction.Left);
            NewNum();

        }

        private void R(object sender, MouseButtonEventArgs e)
        {
            bool isMove = false;
            int scores = 0;
            for (int row = 0; row < 4; row++)
                for (int i = 3; i > -1; i--)
                    for (int j = i - 1; j > -1; j--)
                    {
                        if (Block[row, i] == Block[row, j])
                        {
                            isMove = Block[row, i] > 0 ? true : isMove;

                            Block[row, i] *= 2;
                            Block[row, j] = 0;
                            scores += Block[row, i];
                            break;
                        }
                        if (Block[row, j] > 0)
                        {
                            break;
                        }
                    }
            for (int row = 0; row < 4; row++)
                for (int i = 0; i < 3; i++)
                    if (Block[row, i + 1] == 0)
                    {
                        isMove = Block[row, i] > 0 ? true : isMove;

                        Block[row, i + 1] = Block[row, i];
                        Block[row, i] = 0;

                        if (i > 0)
                        {
                            for (int j = i - 1; j > -1; j--)
                            {
                                Block[row, j + 1] = Block[row, j];
                                Block[row, j] = 0;
                            }
                        }
                    }

            ScoresChanged(scores);

            if (isMove == false)
            {
                return;
            }

            creatBlock(Direction.Right);
            NewNum();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            v.IsOpen = false;
        }

        private void Border_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            RotateTransform rtf = new RotateTransform();
            b.RenderTransform = rtf;
            DoubleAnimation dbAscending = new DoubleAnimation(0, 360, new Duration

            (TimeSpan.FromSeconds(1)));
            dbAscending.RepeatBehavior = RepeatBehavior.Forever;
            var a = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(1));
            a.Completed += delegate { g.Visibility = Visibility.Collapsed; grdMain.Visibility = Visibility.Visible; grdMain.BeginAnimation(OpacityProperty, new DoubleAnimation(0, 1, TimeSpan.FromSeconds(1))); };
            b.BeginAnimation(OpacityProperty, a);
            rtf.BeginAnimation(RotateTransform.AngleProperty, dbAscending);
        }
    }
}
