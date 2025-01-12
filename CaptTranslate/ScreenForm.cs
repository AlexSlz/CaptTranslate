using System.Drawing;

namespace CaptTranslate
{
    public partial class ScreenForm : Form
    {
        public ScreenForm()
        {
            InitializeComponent();
            this.KeyDown += ScreenForm_KeyDown;
            this.MouseClick += ScreenForm_MouseClick;
            this.DoubleBuffered = true;
            File.Delete(ScreenManager.FileName);
        }

        private void ScreenForm_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                this.Close();
            }
        }

        private void ScreenForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private Point _startPoint;
        private Rectangle _selectedArea;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            _startPoint = e.Location;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!_startPoint.IsEmpty)
            {
                _selectedArea = new Rectangle(Math.Min(_startPoint.X, e.X),
                                             Math.Min(_startPoint.Y, e.Y),
                                             Math.Abs(_startPoint.X - e.X),
                                             Math.Abs(_startPoint.Y - e.Y));
                this.Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.Cursor = Cursors.Default;
            this.Invalidate();
            CaptureScreen();
        }

        private void CaptureScreen()
        {
            this.Opacity = 0;
            ImageData.SelectedArea = _selectedArea;
            ImageData.TextPoint = _startPoint;
            ScreenManager.CaptureScreen();
            this.Close();
            /*
            if (_selectedArea.Width > 0 && _selectedArea.Height > 0)
            {
                this.Opacity = 0;
                Bitmap screenshot = new Bitmap(_selectedArea.Width, _selectedArea.Height);
                using (Graphics g = Graphics.FromImage(screenshot))
                {
                    g.CopyFromScreen(_selectedArea.Left, _selectedArea.Top, 0, 0, _selectedArea.Size);
                }
                Form1.SelectedArea = _selectedArea;
                screenshot.Save("screenshot.png");
                GC.Collect();
                this.Close();
            }
            */
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!_selectedArea.IsEmpty)
            {
                Pen pen = new Pen(Color.RebeccaPurple, 4);
                e.Graphics.DrawRectangle(pen, _selectedArea);
            }
        }
    }
}
