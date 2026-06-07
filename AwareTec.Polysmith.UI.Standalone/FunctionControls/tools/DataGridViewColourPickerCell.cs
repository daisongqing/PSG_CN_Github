using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace AwareTec.Polysmith.UI.FunctionControls.tools
{
    /// <summary>
    /// 颜色选择器
    /// </summary>
    public class DataGridViewColourPickerCell : DataGridViewTextBoxCell
    {

        private DateTime selectedDateTime;
        private static Type valueType = typeof(byte);
        private static Type editType = typeof(DataGridViewColourPickerEditor);
        private StringAlignment verticalAlignment;
        private StringAlignment horizontalAlignment;
        //private FormatInfoTypes format;


        public override Type EditType
        {
            get { return editType; }
        }

        public override Type ValueType
        {
            get { return valueType; }
        }

        public DateTime SelectedDateTime
        {
            get { return selectedDateTime; }
            set { selectedDateTime = value; }
        }

        /// <summary>
        /// Returns the current DataGridView EditingControl as a DataGridViewNumericUpDownEditingControl control
        /// </summary>
        private DataGridViewColourPickerEditor EditingFADatePicker
        {
            get { return DataGridView.EditingControl as DataGridViewColourPickerEditor; }
        }



        private const int RECTCOLOR_LEFT = 0;
        private const int RECTCOLOR_TOP = 0;
        private const int RECTCOLOR_WIDTH = 20;
        private const int RECTTEXT_MARGIN = 0;
        private const int RECTTEXT_LEFT = RECTCOLOR_LEFT + RECTCOLOR_WIDTH + RECTTEXT_MARGIN;
        private static StringFormat sf;
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, DataGridViewPaintParts.All);
            if (DataGridView == null)
                return;

            if (sf == null)
            {
                sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;
            }

            Color BlockColor = Color.White;

            if (value == null || value.GetType() == typeof(DBNull)) return;
            KnownColor kc = (KnownColor)(Convert.ToByte(value));
            BlockColor = Color.FromKnownColor(kc);

            Rectangle rec = new Rectangle(cellBounds.X + 2, cellBounds.Y + 3, cellBounds.Width - 8, cellBounds.Height - 8);

            graphics.FillRectangle(new SolidBrush(BlockColor), rec);
            graphics.DrawRectangle(Pens.Black, rec);
            graphics.DrawString(BlockColor.Name, this.DataGridView.Font, Brushes.Black, rec, sf);
        }
        private static bool PartPainted(DataGridViewPaintParts paintParts, DataGridViewPaintParts paintPart)
        {
            return (paintParts & paintPart) != 0;
        }
        [DefaultValue("Center")]
        public StringAlignment VerticalAlignment
        {
            get { return verticalAlignment; }
            set { verticalAlignment = value; }
        }

        [DefaultValue("Near")]
        public StringAlignment HorizontalAlignment
        {
            get { return horizontalAlignment; }
            set { horizontalAlignment = value; }
        }

        private static bool IsInState(DataGridViewElementStates currentState, DataGridViewElementStates checkState)
        {
            return (currentState & checkState) != 0;
        }

        /// <summary>
        /// Little utility function called by the Paint function to see if a particular part needs to be painted. 
        /// </summary>



        /// <summary>
        /// Determines whether this cell, at the given row index, shows the grid's editing control or not.
        /// The row index needs to be provided as a parameter because this cell may be shared among multiple rows.
        /// </summary>
        private bool OwnsEditor(int rowIndex)
        {
            if (rowIndex == -1 || DataGridView == null)
                return false;

            DataGridViewColourPickerEditor editor = DataGridView.EditingControl as DataGridViewColourPickerEditor;
            return editor != null && rowIndex == editor.EditingControlRowIndex;
        }

        internal void SetValue(int rowIndex, DateTime value)
        {
            //SelectedDateTime = value;
            //if (OwnsEditor(rowIndex))
            //    EditingFADatePicker.SelectedDateTime = value;
        }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            DataGridViewColourPickerEditor editor = DataGridView.EditingControl as DataGridViewColourPickerEditor;

            if (editor != null)
            {
                editor.RightToLeft = DataGridView.RightToLeft;

                string formattedValue = initialFormattedValue.ToString();

                byte o;

                if (byte.TryParse(initialFormattedValue.ToString(), out o))
                {
                    editor.SelectedIndex = o - 1;
                    //editor.Text = "RED";
                }


                //if (string.IsNullOrEmpty(formattedValue))
                //{
                //    editor.SelectedDateTime = DateTime.Now;
                //    editor.mv.MonthViewControl.SetNoneDay();
                //}
                //else
                //{
                //    editor.SelectedDateTime = DateTime.Parse(formattedValue);
                //    editor.mv.MonthViewControl.clock.Dt = editor.SelectedDateTime;
                //}
            }
        }

    }


    public class DataGridViewColourPickerEditor : ColorPickerEx, IDataGridViewEditingControl
    {
        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.SelectedIndexChanged += new EventHandler(DataGridViewFADateTimePickerEditor_SelectedIndexChanged);
        }

        void DataGridViewFADateTimePickerEditor_SelectedIndexChanged(object sender, EventArgs e)
        {
            EditingControlValueChanged = true;
            EditingControlFormattedValue = (byte)8;
            if (EditingControlValueChanged == true)
                EditingControlDataGridView.NotifyCurrentCellDirty(true);
        }
        DataGridView _editingControlDataGridView;
        public DataGridView EditingControlDataGridView
        {
            get
            {
                return _editingControlDataGridView;
            }
            set
            {
                _editingControlDataGridView = value;
            }
        }
        object _editingControlFormattedValue;
        public object EditingControlFormattedValue
        {
            get
            {
                return _editingControlFormattedValue;
            }
            set
            {
                _editingControlFormattedValue = value;
            }
        }
        int _editingControlRowIndex;
        public int EditingControlRowIndex
        {
            get
            {
                return _editingControlRowIndex;
            }
            set
            {
                _editingControlRowIndex = value;
            }
        }

        bool _editingControlValueChanged;
        public bool EditingControlValueChanged
        {
            get
            {
                return _editingControlValueChanged;
            }
            set
            {
                _editingControlValueChanged = value;
            }
        }

        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            return true;
        }

        public Cursor EditingPanelCursor
        {
            get { return Cursors.Default; }
        }

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            if (this.SelectedItem == null)
                return 0;
            return ((byte)((Color)((MyColour)this.SelectedItem).Colour).ToKnownColor()).ToString();
        }

        public void PrepareEditingControlForEdit(bool selectAll)
        {

        }

        public bool RepositionEditingControlOnValueChange
        {
            get { return true; }
        }

    }


    /// <summary>
    /// 颜色选择器单元列
    /// </summary>
    public class DataGridViewColourPickerColumn : DataGridViewColumn
    {

        public DataGridViewColourPickerColumn()
            : base(new DataGridViewColourPickerCell())
        {
        }
        //[Browsable(false)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public override DataGridViewCell CellTemplate
        //{
        //    get { return base.CellTemplate; }
        //    set
        //    {
        //        DataGridViewFADateTimePickerCell dataGridViewFADateTimePickerCell = value as DataGridViewFADateTimePickerCell;
        //        if (value != null && dataGridViewFADateTimePickerCell == null)
        //            throw new InvalidCastException("Value provided for CellTemplate must be of type DataGridViewRadioButtonElements.DataGridViewRadioButtonCell or derive from it.");

        //        base.CellTemplate = value;
        //    }
        //}

        /// <summary>
        /// Small utility function that returns the template cell as a DataGridViewRadioButtonCell.
        /// </summary>
        //private DataGridViewFADateTimePickerCell FADatePickerCellTemplate
        //{
        //    get { return (DataGridViewFADateTimePickerCell)CellTemplate; }
        //}






    }

    public class ColorPickerEx : ComboBox
    {

        private const int RECTCOLOR_LEFT = 4;
        private const int RECTCOLOR_TOP = 3;
        private const int RECTCOLOR_WIDTH = 40;
        private const int RECTTEXT_MARGIN = 3;
        private const int RECTTEXT_LEFT = RECTCOLOR_LEFT + RECTCOLOR_WIDTH + RECTTEXT_MARGIN;

        public ColorPickerEx()
        {
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.DropDownStyle = ComboBoxStyle.DropDownList;

            for (byte i = 1; i < 174; i++)
            {
                MyColour c = new MyColour();
                c.Colour = Color.FromKnownColor((KnownColor)i);
                c.Colourid = i;
                this.Items.Add(c);
            }
            this.DisplayMember = "Name";
            this.ValueMember = "Colourid";
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }


        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.State == DrawItemState.Selected || e.State == DrawItemState.None)
                e.DrawBackground();
            Graphics Grphcs = e.Graphics;
            Color BlockColor = Color.Empty;
            int left = RECTCOLOR_LEFT;
            int vertScrollBarWidth =(base.Items.Count > base.MaxDropDownItems)? SystemInformation.VerticalScrollBarWidth : 0;
            int newWidth;
            int width = base.Width;
            foreach (object s in base.Items)  //Loop through list items and check size of each items.
            {
                if (s != null)
                {
                    newWidth = (int)Grphcs.MeasureString(s.ToString().Trim(), e.Font).Width
                       + vertScrollBarWidth;
                    if (width < newWidth)
                        width = newWidth;   //set the width of the drop down list to the width of the largest item.
                }
            }
            base.DropDownWidth = width;
                if (e.Index == -1)
                    BlockColor = SelectedIndex < 0 ? BackColor : Color.FromName(SelectedText);
                else
                    BlockColor = ((MyColour)base.Items[e.Index]).Colour;
            Grphcs.FillRectangle(new SolidBrush(BlockColor), left, e.Bounds.Top + RECTCOLOR_TOP, RECTCOLOR_WIDTH, ItemHeight - 2 * RECTCOLOR_TOP);
            Grphcs.DrawRectangle(Pens.Black, left, e.Bounds.Top + RECTCOLOR_TOP, RECTCOLOR_WIDTH, ItemHeight - 2 * RECTCOLOR_TOP);
            Grphcs.DrawString(BlockColor.Name, e.Font, new SolidBrush(ForeColor), new Rectangle(RECTTEXT_LEFT, e.Bounds.Top, e.Bounds.Width - RECTTEXT_LEFT, ItemHeight));
            base.OnDrawItem(e);
        }

        protected override void OnDropDownStyleChanged(EventArgs e)
        {
            if (this.DropDownStyle != ComboBoxStyle.DropDownList) this.DropDownStyle = ComboBoxStyle.DropDownList;
        }
    }
    public class MyColour
    {
        byte _colourid;

        public byte Colourid
        {
            get { return _colourid; }
            set { _colourid = value; }
        }

        Color _colour;

        public Color Colour
        {
            get { return _colour; }
            set { _colour = value; }
        }

    }

}