namespace Dmitriev.AdWatcher.UI
{
  partial class UnreadAdvsForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnreadAdvsForm));
      this.btnOK = new System.Windows.Forms.Button();
      this.panel1 = new System.Windows.Forms.Panel();
      this.listAdvs = new System.Windows.Forms.ListView();
      this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.btnCancel = new System.Windows.Forms.Button();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(128, 7);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(75, 23);
      this.btnOK.TabIndex = 0;
      this.btnOK.Text = "Понятно!";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.btnCancel);
      this.panel1.Controls.Add(this.btnOK);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(0, 237);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(292, 36);
      this.panel1.TabIndex = 1;
      // 
      // listAdvs
      // 
      this.listAdvs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
      this.listAdvs.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listAdvs.FullRowSelect = true;
      this.listAdvs.Location = new System.Drawing.Point(0, 0);
      this.listAdvs.Name = "listAdvs";
      this.listAdvs.Size = new System.Drawing.Size(292, 237);
      this.listAdvs.TabIndex = 2;
      this.listAdvs.UseCompatibleStateImageBehavior = false;
      this.listAdvs.View = System.Windows.Forms.View.Details;
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "";
      this.columnHeader1.Width = 288;
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(209, 7);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "Позже";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // UnreadAdvsForm
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(292, 273);
      this.Controls.Add(this.listAdvs);
      this.Controls.Add(this.panel1);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "UnreadAdvsForm";
      this.Text = "Новые объявления";
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.ListView listAdvs;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.Button btnCancel;
  }
}