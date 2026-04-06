using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CaptTranslate;

    public partial class SelectForm : Form
    {
        private Point _startPoint;
        private Rectangle _selectedArea;
        public SelectForm()
        {
            InitializeComponent();

            this.KeyPreview = true;
            
            Rectangle allScreensBounds = Rectangle.Empty;

            foreach (var screen in Screen.AllScreens)
            {
                allScreensBounds = Rectangle.Union(allScreensBounds, screen.Bounds);
            }

            this.StartPosition = FormStartPosition.Manual;
            this.Location = allScreensBounds.Location;
            this.Size = allScreensBounds.Size;
            
            DoubleBuffered = true;

            if (File.Exists(ScreenManager.FileName))
                File.Delete(ScreenManager.FileName);

            Cursor = Cursors.Cross;

            ImageData.IsSuccess = false;

        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button == MouseButtons.Right) this.Close();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                _startPoint = e.Location;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !_startPoint.IsEmpty)
            {
                _selectedArea = new Rectangle(
                    Math.Min(_startPoint.X, e.X),
                    Math.Min(_startPoint.Y, e.Y),
                    Math.Abs(_startPoint.X - e.X),
                    Math.Abs(_startPoint.Y - e.Y));

                this.Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (_startPoint.IsEmpty && (_selectedArea.Width <= 0 && _selectedArea.Height <= 0))
                return;

            this.Cursor = Cursors.Default;
            
            ImageData.IsSuccess = true;
            ImageData.SelectedArea = _selectedArea;
            ImageData.TextPoint = this.PointToScreen(_startPoint);
            
            this.Close();
        }
        

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_selectedArea.IsEmpty) return;
            using var pen = new Pen(Color.RebeccaPurple, 4);
            e.Graphics.DrawRectangle(pen, _selectedArea);
        }
    }