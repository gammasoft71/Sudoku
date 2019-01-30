using System.Collections.Generic;
using System.Windows.Forms;

namespace Sudoku {
  internal class MenuPanel : Panel {
    public MenuPanel() { }

    public Menu.MenuItemCollection Items {
      get { return this.items; }
      set { this.items = value; }
    }

    private Menu.MenuItemCollection items = new Menu.MenuItemCollection(null);
    private List<Label> values = new List<Label>();
  }
}
