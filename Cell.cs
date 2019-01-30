        if (value < 0 || value > 9)
          throw new System.ArgumentOutOfRangeException();
        if (this.value != value) {
          this.value = value;
          this.Invalidate();
          this.OnValueChanged(System.EventArgs.Empty);
        }
      }
    }

    public bool ReadOnly {
      get { return !this.Enabled; }
      set { this.Enabled = !value; }
    }

    override protected void OnPaint(System.Windows.Forms.PaintEventArgs e) {
      e.Graphics.FillRectangle(new System.Drawing.SolidBrush(this.ReadOnly ? Color.FromArgb(255, 236, 236, 236) : Color.White), 4, 4, e.ClipRectangle.Width - 8, e.ClipRectangle.Height - 8);
      ControlPaint.DrawBorder3D(e.Graphics, e.ClipRectangle, this.ReadOnly ? Border3DStyle.Etched : Border3DStyle.Bump);

      if (this.Focused)
        e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.SystemColors.Highlight), 6, 6, e.ClipRectangle.Width - 12, e.ClipRectangle.Height - 12);

      if (this.value != 0) {
        System.Drawing.StringFormat format = new StringFormat();
        format.Alignment = System.Drawing.StringAlignment.Center;
        format.LineAlignment = System.Drawing.StringAlignment.Center;
        e.Graphics.DrawString(string.Format("{0}", this.value), new System.Drawing.Font("Courier New", e.ClipRectangle.Height - 14, System.Drawing.FontStyle.Bold), new System.Drawing.SolidBrush(this.ForeColor), e.ClipRectangle, format);
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

    override protected void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e) {
      if (e.KeyChar >= '0' && e.KeyChar <= '9')
        this.Value = e.KeyChar - '0';
      else if (e.KeyChar == (char)System.Windows.Forms.Keys.Back || e.KeyChar == (char)System.Windows.Forms.Keys.Delete)
        this.Value = 0;
      base.OnKeyPress(e);
    }

    virtual public void OnValueChanged(System.EventArgs e) {
      if (this.ValueChanged != null)
        this.ValueChanged(this, e);
    }

    override protected System.Drawing.Size DefaultSize { get { return new System.Drawing.Size(98, 98); } }

    public System.EventHandler ValueChanged = null;

    private int value = 0;
  }
}
