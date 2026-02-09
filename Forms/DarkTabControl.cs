using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using KiloFilter.Core;

namespace KiloFilter.Forms
{
    /// <summary>
    /// Custom dark-themed TabControl with enhanced visual styling
    /// </summary>
    public class DarkTabControl : TabControl
    {
        public DarkTabControl()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | 
                         ControlStyles.UserPaint | 
                         ControlStyles.DoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(20, 20, 20)))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }

            Rectangle contentArea = this.DisplayRectangle;
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(45, 45, 45)))
            {
                e.Graphics.FillRectangle(brush, contentArea);
            }

            using (Pen separatorPen = new Pen(Color.FromArgb(70, 70, 70), 1))
            {
                e.Graphics.DrawLine(separatorPen, 
                    contentArea.Left, 
                    contentArea.Top - 1, 
                    contentArea.Right, 
                    contentArea.Top - 1);
            }

            for (int i = 0; i < this.TabCount; i++)
            {
                DrawTab(e.Graphics, this.TabPages[i], i);
            }
        }

        private void DrawTab(Graphics g, TabPage tabPage, int index)
        {
            Rectangle tabBounds = this.GetTabRect(index);
            bool isSelected = (index == this.SelectedIndex);

            Color selectedColor = Color.FromArgb(33, 150, 243);
            Color normalColor = Color.FromArgb(35, 35, 35);
            Color backgroundColor = Color.FromArgb(20, 20, 20);
            
            using (SolidBrush bgBrush = new SolidBrush(backgroundColor))
            {
                g.FillRectangle(bgBrush, tabBounds);
            }
            
            Color tabColor = isSelected ? selectedColor : normalColor;
            using (SolidBrush brush = new SolidBrush(tabColor))
            {
                Rectangle fillRect = new Rectangle(
                    tabBounds.X, 
                    tabBounds.Y + 2, 
                    tabBounds.Width - 1,
                    tabBounds.Height - 2
                );
                g.FillRectangle(brush, fillRect);
            }

            if (isSelected)
            {
                using (Pen pen = new Pen(Color.FromArgb(100, 181, 246), 4))
                {
                    g.DrawLine(pen, 
                        tabBounds.Left + 2, 
                        tabBounds.Top + 2, 
                        tabBounds.Right - 3, 
                        tabBounds.Top + 2);
                }
            }
            else
            {
                using (Pen pen = new Pen(Color.FromArgb(60, 60, 60), 1))
                {
                    g.DrawRectangle(pen, 
                        tabBounds.X, 
                        tabBounds.Y + 2, 
                        tabBounds.Width - 2, 
                        tabBounds.Height - 3);
                }
            }

            string text = tabPage.Text;
            Font font = text.Any(c => c > 0x2000) ? MainForm.emojiFontBold : new Font("Segoe UI", 9F, isSelected ? FontStyle.Bold : FontStyle.Regular);
            
            using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(100, 0, 0, 0)))
            {
                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                Rectangle shadowRect = new Rectangle(tabBounds.X + 1, tabBounds.Y + 1, tabBounds.Width, tabBounds.Height);
                g.DrawString(text, font, shadowBrush, shadowRect, sf);
            }
            
            using (SolidBrush textBrush = new SolidBrush(Color.White))
            {
                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                g.DrawString(text, font, textBrush, tabBounds, sf);
            }
        }
    }
}
