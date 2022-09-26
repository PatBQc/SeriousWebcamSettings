using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using AForge.Imaging.Filters;

namespace LitePlacer {
    internal class DataGridDrag {
        private Rectangle dragBoxFromMouseDown;
        private int rowIndexFromMouseDown;
        private int rowIndexOfItemUnderMouseToDrop;
        private DataGridView dgv;

        public DataGridDrag(DataGridView d) {
            dgv = d;
            d.AllowDrop = true;
            d.MouseMove += MouseMove;
            d.MouseDown += MouseDown;
            d.DragOver += DragOver;
            d.DragDrop += DragDrop;
        }

        private void MouseMove(object sender, MouseEventArgs e) {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left) {
                // If the mouse moves outside the rectangle, start the drag.
                if (dragBoxFromMouseDown != Rectangle.Empty &&
                    !dragBoxFromMouseDown.Contains(e.X, e.Y)) {

                    // Proceed with the drag and drop, passing in the list item.                    
                    DragDropEffects dropEffect = dgv.DoDragDrop(dgv.Rows[rowIndexFromMouseDown], DragDropEffects.Move);
                }
            }
        }

        private void MouseDown(object sender, MouseEventArgs e) {
            // Get the index of the item the mouse is below.
            rowIndexFromMouseDown = dgv.HitTest(e.X, e.Y).RowIndex;
            if (rowIndexFromMouseDown != -1) {
                // Remember the point where the mouse down occurred. 
                // The DragSize indicates the size that the mouse can move 
                // before a drag event should be started.                
                Size dragSize = SystemInformation.DragSize;

                // Create a rectangle using the DragSize, with the mouse position being
                // at the center of the rectangle.
                dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width/2), e.Y - (dragSize.Height/2)),
                    dragSize);
            }
            else
            // Reset the rectangle if the mouse is not over an item in the ListBox.
                dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void DragOver(object sender, DragEventArgs e) { e.Effect = DragDropEffects.Move; }

        private void DragDrop(object sender, DragEventArgs e) {
            // The mouse locations are relative to the screen, so they must be 
            // converted to client coordinates.
            Point clientPoint = dgv.PointToClient(new Point(e.X, e.Y));

            // Get the row index of the item the mouse is below. 
            rowIndexOfItemUnderMouseToDrop = dgv.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            // If the drag operation was a move then remove and insert the row.
            if (e.Effect == DragDropEffects.Move) {
                //       var obj = ((DataGridViewRow)e.Data.GetData(typeof(DataGridViewRow))).DataBoundItem;
                dynamic list = dgv.DataSource;
                var item = list[rowIndexFromMouseDown];
                list.RemoveAt(rowIndexFromMouseDown);
                if (rowIndexOfItemUnderMouseToDrop == -1)
                    list.Add(item);
                else list.Insert(rowIndexOfItemUnderMouseToDrop, item);
            }
        }
    }
}
