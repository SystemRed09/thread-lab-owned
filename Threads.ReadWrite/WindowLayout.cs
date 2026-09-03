using System.Drawing;
using System.Windows.Forms;

namespace Threads.ReadWrite
{
    // Places the three windows in fixed, predictable positions: Writer top-left,
    // Reader beside it, self-test below. Without this, each form opens wherever
    // Windows feels like putting it, which makes the demonstration hard to follow.
    //
    // Named WindowLayout rather than Layout on purpose: every Form inherits a
    // Control.Layout event, and a class called Layout would be shadowed by it
    // inside the forms that need to call this.
    //
    // TEST SCAFFOLDING. Not part of the design under study.
    public static class WindowLayout
    {
        const int Margin = 30;
        const int CellW = 460;
        const int CellH = 460;

        public static void PlaceWriter(Form f) { Place(f, 0, 0); }
        public static void PlaceReader(Form f) { Place(f, 1, 0); }
        public static void PlaceAudit(Form f) { Place(f, 0, 1); }

        static void Place(Form f, int col, int row)
        {
            Rectangle area = Screen.PrimaryScreen.WorkingArea;

            int x = area.Left + Margin + col * CellW;
            int y = area.Top + Margin + row * CellH;

            // Keep the window on screen even on a small display:
            if (x + f.Width > area.Right) { x = area.Right - f.Width; }
            if (y + f.Height > area.Bottom) { y = area.Bottom - f.Height; }
            if (x < area.Left) { x = area.Left; }
            if (y < area.Top) { y = area.Top; }

            f.StartPosition = FormStartPosition.Manual;
            f.Location = new Point(x, y);
        }
    }
}
