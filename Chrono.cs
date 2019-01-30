using System.Diagnostics;
using System.Windows.Forms;

namespace Sudoku {
  internal class Chrono : Label {
    public Chrono() {
      this.timer.Interval = 31;
      this.Text = "00:00:00";
      this.timer.Tick += delegate (object sender, System.EventArgs e) {
        this.Text = this.showMilliseconds ? $"{this.chrono.Elapsed.Hours:D2}:{this.chrono.Elapsed.Minutes:D2}:{this.chrono.Elapsed.Seconds:D2}.{this.chrono.Elapsed.Milliseconds:D3}" : $"{this.chrono.Elapsed.Hours:D2}:{this.chrono.Elapsed.Minutes:D2}:{this.chrono.Elapsed.Seconds:D2}";
      };
    }

    public bool ShowMilliseconds {
      get { return this.showMilliseconds; }
      set {
        this.showMilliseconds = value;
        if (!this.IsRunning)
          this.Reset();
      }
    }

    public bool IsRunning { get; private set; } = false;

    public void Start() {
      this.IsRunning = true;
      this.chrono.Start();
      this.timer.Enabled = true;
    }

    public void Pause() { this.timer.Enabled = false; }

    public void Resume() { this.timer.Enabled = true; }

    public void Stop() {
      this.IsRunning = false;
      this.chrono.Stop();
      this.timer.Enabled = false;
    }

    public void Reset() {
      this.Stop();
      this.chrono.Reset();
      this.Text = this.showMilliseconds ? "00:00:00.000" : "00:00:00";
    }

    private Stopwatch chrono = new Stopwatch();
    private Timer timer = new Timer();
    private bool showMilliseconds = false;
  }
}
