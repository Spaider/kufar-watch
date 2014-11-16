namespace Dmitriev.AdWatcher.UI
{
  partial class FeedListForm
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FeedListForm));
      this.listBox1 = new System.Windows.Forms.ListBox();
      this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.miNewFeeds = new System.Windows.Forms.ToolStripMenuItem();
      this.miFeedList = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.miExit = new System.Windows.Forms.ToolStripMenuItem();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.tsbAddFeed = new System.Windows.Forms.ToolStripButton();
      this.tsbRemoveFeed = new System.Windows.Forms.ToolStripButton();
      this.contextMenuStrip1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // listBox1
      // 
      this.listBox1.DisplayMember = "Caption";
      this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listBox1.FormattingEnabled = true;
      this.listBox1.Location = new System.Drawing.Point(0, 25);
      this.listBox1.Name = "listBox1";
      this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.listBox1.Size = new System.Drawing.Size(397, 228);
      this.listBox1.TabIndex = 2;
      this.listBox1.ValueMember = "Id";
      this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
      // 
      // notifyIcon1
      // 
      this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
      this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
      this.notifyIcon1.Visible = true;
      this.notifyIcon1.BalloonTipClicked += new System.EventHandler(this.notifyIcon1_BalloonTipClicked);
      this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miNewFeeds,
            this.miFeedList,
            this.toolStripMenuItem1,
            this.miExit});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(197, 76);
      // 
      // miNewFeeds
      // 
      this.miNewFeeds.Enabled = false;
      this.miNewFeeds.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
      this.miNewFeeds.Name = "miNewFeeds";
      this.miNewFeeds.Size = new System.Drawing.Size(196, 22);
      this.miNewFeeds.Text = "Новые объявления...";
      this.miNewFeeds.Click += new System.EventHandler(this.miNewFeeds_Click);
      // 
      // miFeedList
      // 
      this.miFeedList.Font = new System.Drawing.Font("Tahoma", 8.25F);
      this.miFeedList.Name = "miFeedList";
      this.miFeedList.Size = new System.Drawing.Size(196, 22);
      this.miFeedList.Text = "Ленты объявлений...";
      this.miFeedList.Click += new System.EventHandler(this.miFeedList_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(193, 6);
      // 
      // miExit
      // 
      this.miExit.Name = "miExit";
      this.miExit.Size = new System.Drawing.Size(196, 22);
      this.miExit.Text = "Выход";
      this.miExit.Click += new System.EventHandler(this.miExit_Click);
      // 
      // timer1
      // 
      this.timer1.Interval = 60000;
      this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddFeed,
            this.tsbRemoveFeed});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(397, 25);
      this.toolStrip1.Stretch = true;
      this.toolStrip1.TabIndex = 3;
      this.toolStrip1.Text = "Ленты";
      // 
      // tsbAddFeed
      // 
      this.tsbAddFeed.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.tsbAddFeed.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddFeed.Image")));
      this.tsbAddFeed.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tsbAddFeed.Name = "tsbAddFeed";
      this.tsbAddFeed.Size = new System.Drawing.Size(73, 22);
      this.tsbAddFeed.Text = "Добавить...";
      this.tsbAddFeed.Click += new System.EventHandler(this.tsbAddFeed_Click);
      // 
      // tsbRemoveFeed
      // 
      this.tsbRemoveFeed.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.tsbRemoveFeed.Image = ((System.Drawing.Image)(resources.GetObject("tsbRemoveFeed.Image")));
      this.tsbRemoveFeed.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tsbRemoveFeed.Name = "tsbRemoveFeed";
      this.tsbRemoveFeed.Size = new System.Drawing.Size(55, 22);
      this.tsbRemoveFeed.Text = "Удалить";
      this.tsbRemoveFeed.Click += new System.EventHandler(this.tsbRemoveFeed_Click);
      // 
      // FeedListForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(397, 253);
      this.Controls.Add(this.listBox1);
      this.Controls.Add(this.toolStrip1);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "FeedListForm";
      this.ShowInTaskbar = false;
      this.Text = "Ленты объявлений";
      this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
      this.Load += new System.EventHandler(this.FeedListForm_Load);
      this.Resize += new System.EventHandler(this.FeedListForm_Resize);
      this.contextMenuStrip1.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ListBox listBox1;
    private System.Windows.Forms.NotifyIcon notifyIcon1;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem miFeedList;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem miExit;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton tsbAddFeed;
    private System.Windows.Forms.ToolStripButton tsbRemoveFeed;
    private System.Windows.Forms.ToolStripMenuItem miNewFeeds;
  }
}