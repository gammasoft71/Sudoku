using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sudoku {
  internal class Cell : Control {
    public Cell() {
      this.ReadOnly = false;
    }
    public Cell(Cell cell) {
      this.value = cell.value;
      this.ReadOnly = cell.ReadOnly;
    }

    public int Value {
      get { return this.value; }
      set {
        if (value < 0 || value > 9)
          throw new ArgumentOutOfRangeException();
        if (this.value != value) {
          this.value = value;
          this.Invalidate();
          this.OnValueChanged(EventArgs.Empty);
        }
      }
    }

    public bool ReadOnly {
      get { return !this.Enabled; }
      set { this.Enabled = !value; }
    }

    override protected void OnPaint(PaintEventArgs e) {
      e.Graphics.FillRectangle(new SolidBrush(this.ReadOnly ? Color.FromArgb(255, 236, 236, 236) : Color.White), 4, 4, e.ClipRectangle.Width - 8, e.ClipRectangle.Height - 8);
      ControlPaint.DrawBorder3D(e.Graphics, e.ClipRectangle, this.ReadOnly ? Border3DStyle.Etched : Border3DStyle.Bump);

      if (this.Focused)
        e.Graphics.FillRectangle(new SolidBrush(SystemColors.Highlight), 6, 6, e.ClipRectangle.Width - 12, e.ClipRectangle.Height - 12);

      if (this.value != 0) {
        StringFormat format = new StringFormat();
        format.Alignment = StringAlignment.Center;
        format.LineAlignment = StringAlignment.Center;
        e.Graphics.DrawString($"{this.value}", new Font("Courier New", e.ClipRectangle.Height - 14, FontStyle.Bold), new SolidBrush(this.ForeColor), e.ClipRectangle, format);
      }
      base.OnPaint(e);
    }

    protected override void OnClick(EventArgs e) {
      base.OnClick(e);
      this.Focus();
    }

    protected override void OnGotFocus(EventArgs e) {
      base.OnGotFocus(e);
      this.Invalidate();
    }

    protected override void OnLostFocus(EventArgs e) {
      base.OnLostFocus(e);
      this.Invalidate();
    }

    override protected void OnKeyPress(KeyPressEventArgs e) {
      if (e.KeyChar >= '0' && e.KeyChar <= '9')
        this.Value = e.KeyChar - '0';
      else if (e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Delete)
        this.Value = 0;
      base.OnKeyPress(e);
    }

    virtual public void OnValueChanged(EventArgs e) {
      if (this.ValueChanged != null)
        this.ValueChanged(this, e);
    }

    override protected Size DefaultSize { get { return new Size(98, 98); } }

    public EventHandler ValueChanged = null;

    private int value = 0;
  }
}
